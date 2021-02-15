using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json.Serialization;

namespace FluentSpotifyApi.Expressions.Fields
{
    internal static class ExpressionHelpers
    {
        public static IEnumerable<string> GetFieldsPath(ParameterExpression parameterExpression, Expression expression)
        {
            return GetFieldsPathBackward(parameterExpression, expression).Reverse();
        }

        private static IEnumerable<string> GetFieldsPathBackward(ParameterExpression parameterExpression, Expression expression)
        {
            MemberExpression memberExpression;
            while ((memberExpression = GetMemberExpression(expression)) != null)
            {
                var fieldName = memberExpression.Member.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? memberExpression.Member.Name;

                yield return fieldName;

                expression = memberExpression.Expression;
            }

            if (expression != parameterExpression)
            {
                throw new ArgumentException("The expression must be a member expression referencing the input parameter.", nameof(expression));
            }
        }

        private static MemberExpression GetMemberExpression(Expression expression)
        {
            if (expression is BinaryExpression binaryExpression && binaryExpression.NodeType == ExpressionType.ArrayIndex)
            {
                expression = binaryExpression.Left;
            }
            else if (expression is UnaryExpression unaryExpression && unaryExpression.NodeType == ExpressionType.Convert)
            {
                expression = unaryExpression.Operand;
            }

            return expression as MemberExpression;
        }
    }
}
