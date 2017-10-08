using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentSpotifyApi.Expressions.Query
{
    internal static class LogicalExpressionFlattener
    {
        public static IEnumerable<object> Flatten(Expression expression)
        {
            if (expression is BinaryExpression binaryExpression && (binaryExpression.NodeType == ExpressionType.AndAlso || binaryExpression.NodeType == ExpressionType.OrElse))
            {
                foreach (var item in Flatten(binaryExpression.Left))
                {
                    yield return item;
                }

                yield return binaryExpression.NodeType;

                foreach (var item in Flatten(binaryExpression.Right))
                {
                    yield return item;
                }
            }
            else
            {
                yield return expression;
            }
        }
    }
}
