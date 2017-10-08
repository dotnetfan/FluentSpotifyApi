using System.Linq.Expressions;

namespace FluentSpotifyApi.Expressions.Query
{
    internal static class ExpressionHelpers
    {
        public static bool IsContainsCall(Expression expression)
        {
            return expression is MethodCallExpression methodCall && methodCall.Method.Name == "Contains" && methodCall.Method.DeclaringType == typeof(string);
        }

        public static bool IsRange(Expression expression)
        {
            return
                expression is BinaryExpression binaryExpression &&
                (binaryExpression.NodeType == ExpressionType.GreaterThanOrEqual || binaryExpression.NodeType == ExpressionType.LessThanOrEqual);
        }
    }
}
