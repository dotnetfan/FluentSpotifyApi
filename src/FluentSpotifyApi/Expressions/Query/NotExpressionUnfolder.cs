using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentSpotifyApi.Expressions.Query
{
    internal static class NotExpressionUnfolder
    {
        public static IEnumerable<object> Unfold(IEnumerable<object> flattenedExpression)
        {
            foreach (var item in flattenedExpression)
            {
                if (item is UnaryExpression unaryExpression && unaryExpression.NodeType == ExpressionType.Not)
                {
                    yield return ExpressionType.Not;

                    var operand = unaryExpression.Operand;
                    if (ExpressionHelpers.IsContainsCall(operand))
                    {
                        yield return operand;
                    }
                    else if (operand is BinaryExpression binaryExpression && ExpressionHelpers.IsRange(binaryExpression.Left))
                    {
                        yield return binaryExpression.Left;
                        yield return binaryExpression.NodeType;
                        yield return binaryExpression.Right;
                    }
                    else
                    {
                        throw new ArgumentException($"Unsupported NOT expression operand of type '{operand.GetType()}' has been found.", nameof(operand));
                    }
                }
                else if (item is BinaryExpression binaryExpression && binaryExpression.NodeType == ExpressionType.NotEqual)
                {
                    yield return ExpressionType.Not;
                    yield return Expression.Equal(binaryExpression.Left, binaryExpression.Right);
                }
                else
                {
                    yield return item;
                }
            }
        }
    }
}
