using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace FluentSpotifyApi.Expressions.Fields
{
    internal class FieldsBuilder<TInput> : IFieldsBuilder<TInput>
    {
        [SuppressMessage("Microsoft.StyleCop.CSharp.SpacingRules", "SA1009:ClosingParenthesisMustBeSpacedCorrectly", Justification = "C# 7 Tuples")]
        private readonly IList<(LambdaExpression Expression, bool IsExclude)> expressions = new List<(LambdaExpression Expression, bool IsExclude)>();

        public IFieldsBuilder<TInput> Include<TResult>(Expression<Func<TInput, TResult>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            this.expressions.Add((expression, false));

            return this;
        }

        public IFieldsBuilder<TInput> Exclude<TResult>(Expression<Func<TInput, TResult>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            this.expressions.Add((expression, true));

            return this;
        }

        public string GetFields()
        {
            var tree = new FieldsTree();
            foreach (var item in this.expressions)
            {
                tree.Add(ExpressionHelpers.GetFieldsPath(item.Expression.Parameters.First(), item.Expression.Body), item.IsExclude);
            }

            return tree.GetFields();
        }
    }
}
