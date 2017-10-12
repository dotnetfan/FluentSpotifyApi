using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using FluentSpotifyApi.Core.Internal.Extensions;

namespace FluentSpotifyApi.Expressions.Query
{
    /// <summary>
    /// The query provider.
    /// </summary>
    public static class QueryProvider
    {
        /// <summary>
        /// Gets query from the specified predicate expression.
        /// </summary>
        /// <typeparam name="TInput">The type of the input.</typeparam>
        /// <param name="predicate">The predicate expression.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when <paramref name="predicate"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="predicate"/> is not valid.
        /// </exception>
        public static string Get<TInput>(Expression<Func<TInput, bool>> predicate) => Get(predicate, new QueryOptions());

        /// <summary>
        /// Gets query from the specified predicate expression.
        /// </summary>
        /// <typeparam name="TInput">The type of the input.</typeparam>
        /// <param name="predicate">The predicate expression.</param>
        /// <param name="queryOptions">The query options.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when <paramref name="predicate"/> or <paramref name="queryOptions"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="predicate"/> is not valid.
        /// </exception>
        public static string Get<TInput>(Expression<Func<TInput, bool>> predicate, QueryOptions queryOptions)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (queryOptions == null)
            {
                throw new ArgumentNullException(nameof(queryOptions));
            }

            return new QueryProviderHelper<TInput>(predicate, queryOptions).Get();
        }

        private class QueryProviderHelper<TInput>
        {
            private const string OrOperator = "OR";

            private const string NotOperator = "NOT";

            private static readonly string[] Operators = new[] { OrOperator, NotOperator };

            private static readonly Regex SpaceRegex = new Regex(@"\s+", RegexOptions.Compiled);

            private readonly ParameterExpression parameterExpression;

            private readonly Expression body;

            private readonly QueryOptions queryOptions;

            public QueryProviderHelper(Expression<Func<TInput, bool>> predicate, QueryOptions queryOptions)
            {
                this.parameterExpression = predicate.Parameters.First();
                this.body = predicate.Body;
                this.queryOptions = queryOptions;
            }

            public string Get()
            {
                var flattenedExpression = LogicalExpressionFlattener.Flatten(this.body);
                flattenedExpression = NotExpressionUnfolder.Unfold(flattenedExpression);

                var sb = new StringBuilder();

                var list = flattenedExpression.ToList();
                for (var i = 0; i < list.Count; i++)
                {
                    var item = list[i];

                    if (item is Expression expression)
                    {
                        string term;

                        if (expression is BinaryExpression binaryExpression && binaryExpression.NodeType == ExpressionType.Equal)
                        {
                            term = this.ProcessEqualExpression(binaryExpression);
                        }
                        else if (ExpressionHelpers.IsRange(expression))
                        {
                            if (i + 2 >= list.Count || !(list[i] is BinaryExpression left) || !(list[i + 1] is ExpressionType expressionType) || !(list[i + 2] is BinaryExpression right))
                            {
                                throw new ArgumentException($"Range must be the following sequence: BinaryExpression, ExpressionType, BinaryExpression.");
                            }

                            term = this.ProcessRangeExpression(left, expressionType, right);

                            i += 2;
                        }
                        else if (ExpressionHelpers.IsContainsCall(expression))
                        {
                            term = this.ProcessContainsCallExpression((MethodCallExpression)expression, i > 0 ? list[i - 1] : null);
                        }
                        else
                        {
                            throw new ArgumentException($"Unsupported expression of type '{expression.GetType()}' has been found.");
                        }

                        sb.Append(term);
                    }
                    else if (item is ExpressionType expressionType)
                    {
                        switch (expressionType)
                        {
                            case ExpressionType.AndAlso:
                                sb.Append(" ");
                                break;
                            case ExpressionType.OrElse:
                                sb.Append($" {OrOperator} ");
                                break;
                            case ExpressionType.Not:
                                sb.Append($"{NotOperator} ");
                                break;
                            default:
                                throw new ArgumentException($"Unsupported expression type '{expressionType}' has been found.");
                        }
                    }
                    else
                    {
                        throw new ArgumentException($"Unsupported item of type '{item?.GetType()}' has been found.");
                    }
                }

                return sb.ToString();
            }

            private string ProcessEqualExpression(BinaryExpression expression)
            {
                expression = this.UnfoldEnumConversionEqualExpression(expression);

                Expression valueExpression;
                if (this.TryGetQueryField(expression.Left, out string queryField))
                {
                    valueExpression = expression.Right;
                }
                else if (this.TryGetQueryField(expression.Right, out queryField))
                {
                    valueExpression = expression.Left;
                }
                else
                {
                    throw new ArgumentException("One of the equal expression operands must be a query field.");
                }

                var value = this.GetValue(valueExpression);

                string stringValue;
                if (value is Enum enumValue)
                {
                    stringValue = enumValue.GetDescription();
                }
                else
                {
                    stringValue = value.ToInvariantString();
                }

                if (value == null || value is string)
                {
                    if (this.queryOptions.RemoveSpecialCharacters)
                    {
                        stringValue = stringValue.Replace("\"", string.Empty);
                    }

                    stringValue = $"\"{stringValue}\"";
                }

                return this.GetTerm(queryField, stringValue);
            }

            private BinaryExpression UnfoldEnumConversionEqualExpression(BinaryExpression binaryExpression)
            {
                Expression valueExpression;

                if (this.TryUnfoldInputEnumConversionExpression(binaryExpression.Left, out MemberExpression innerExpression))
                {
                    valueExpression = binaryExpression.Right;
                }
                else if (this.TryUnfoldInputEnumConversionExpression(binaryExpression.Right, out innerExpression))
                {
                    valueExpression = binaryExpression.Left;
                }
                else
                {
                    return binaryExpression;
                }

                return Expression.Equal(innerExpression, Expression.Convert(valueExpression, innerExpression.Type));
            }

            private bool TryUnfoldInputEnumConversionExpression(Expression expression, out MemberExpression inner)
            {
                inner = null;

                if (expression is UnaryExpression unaryExpression &&
                    unaryExpression.NodeType == ExpressionType.Convert &&
                    unaryExpression.Operand.Type.GetTypeInfo().IsEnum &&
                    this.TryGetAsInputMember(unaryExpression.Operand, out MemberExpression member))
                {
                    inner = member;
                    return true;
                }

                return false;
            }

            private string ProcessRangeExpression(BinaryExpression left, ExpressionType expressionType, BinaryExpression right)
            {
                if (expressionType != ExpressionType.AndAlso)
                {
                    throw new ArgumentException("Only range expressions with 'AndAlso' operator are supported.");
                }

                var leftBound = this.ProcessBoundExpression(left);
                var rightBound = this.ProcessBoundExpression(right);

                if (leftBound.QueryField != rightBound.QueryField)
                {
                    throw new ArgumentException("Both range bound expressions must reference the same query field.");
                }

                if (leftBound.IsLower == rightBound.IsLower)
                {
                    throw new ArgumentException("Range bound expressions must define a range.");
                }

                var range = string.Join("-", new[] { leftBound, rightBound }.OrderBy(item => item.IsLower).Select(item => item.Value.ToInvariantString()));

                return this.GetTerm(leftBound.QueryField, range);
            }

            private (bool IsLower, string QueryField, int Value) ProcessBoundExpression(BinaryExpression expression)
            {
                bool isLower;

                if (expression.NodeType == ExpressionType.LessThanOrEqual)
                {
                    isLower = true;
                }
                else if (expression.NodeType == ExpressionType.GreaterThanOrEqual)
                {
                    isLower = false;
                }
                else
                {
                    throw new ArgumentException("Only bound expressions with 'GreaterThanOrEqual' or 'LessThanOrEqual' operators are supported.");
                }

                Expression valueExpression;
                if (this.TryGetQueryField(expression.Left, out string queryField))
                {
                    valueExpression = expression.Right;
                }
                else if (this.TryGetQueryField(expression.Right, out queryField))
                {
                    valueExpression = expression.Left;
                    isLower = !isLower;
                }
                else
                {
                    throw new ArgumentException("One of the bound expression operands must be a query field.");
                }

                var value = (int)this.GetValue(valueExpression);

                return (isLower, queryField, value);
            }

            private string ProcessContainsCallExpression(MethodCallExpression expression, object lastItem)
            {
                if (!this.TryGetQueryField(expression.Object, out string queryField))
                {
                    throw new ArgumentException("The Contains call object expression must be a query field.");
                }

                var stringValue = this.GetValue(expression.Arguments.First()).ToInvariantString();

                if (this.queryOptions.RemoveSpecialCharacters)
                {
                    stringValue = stringValue.Replace("\"", string.Empty).Replace(":", string.Empty);
                }

                if (this.queryOptions.NormalizePartialMatch)
                {
                    var sb = new StringBuilder();

                    var isFirst = true;
                    foreach (var word in SpaceRegex.Split(stringValue).Where(item => !string.IsNullOrEmpty(item)))
                    {
                        if (!isFirst)
                        {
                            sb.Append(" ");

                            if (lastItem is ExpressionType expressionType && expressionType == ExpressionType.Not)
                            {
                                sb.Append($"{NotOperator} ");
                            }
                        }
                        
                        sb.Append(this.GetTerm(queryField, Operators.Contains(word, StringComparer.OrdinalIgnoreCase) ? word.ToLower() : word));

                        isFirst = false;
                    }

                    return sb.Length == 0 ? this.GetTerm(queryField, string.Empty) : sb.ToString();
                }
                else
                {
                    return this.GetTerm(queryField, stringValue);
                }
            }

            private string GetTerm(string queryField, string value)
            {
                return !string.IsNullOrEmpty(queryField) ? $"{queryField}:{value}" : value;
            }

            private bool TryGetQueryField(Expression expression, out string queryField)
            {
                queryField = null;

                if (this.TryGetAsInputMember(expression, out MemberExpression member))
                {
                    queryField = member.Member.GetCustomAttribute<DescriptionAttribute>()?.Description;
                    return true;
                }

                return false;
            }

            private bool TryGetAsInputMember(Expression expression, out MemberExpression member)
            {
                member = null;

                if (expression is MemberExpression memberExpression && memberExpression.Expression == this.parameterExpression)
                {
                    member = memberExpression;
                    return true;
                }

                return false;
            }

            private object GetValue(Expression expression)
            {
                if (expression is ConstantExpression constant)
                {
                    return constant.Value;
                }
                else
                {
                    var objectExpression = Expression.Convert(expression, typeof(object));
                    var getterLambda = Expression.Lambda<Func<object>>(objectExpression);
                    var getter = getterLambda.Compile();

                    return getter();
                }
            }
        }
    }
}
