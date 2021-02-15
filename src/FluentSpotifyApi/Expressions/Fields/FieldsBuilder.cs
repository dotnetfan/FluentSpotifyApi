using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentSpotifyApi.Core.Utils;

namespace FluentSpotifyApi.Expressions.Fields
{
    internal class FieldsBuilder<TInput> : IFieldsBuilder<TInput>
    {
        private readonly IList<(LambdaExpression Expression, bool IsExclude)> expressions = new List<(LambdaExpression Expression, bool IsExclude)>();

        public IFieldsBuilder<TInput> Include<TResult>(Expression<Func<TInput, TResult>> expression)
        {
            SpotifyArgumentAssertUtils.ThrowIfNull(expression, nameof(expression));

            this.expressions.Add((expression, false));

            return this;
        }

        public IFieldsBuilder<TInput> Exclude<TResult>(Expression<Func<TInput, TResult>> expression)
        {
            SpotifyArgumentAssertUtils.ThrowIfNull(expression, nameof(expression));

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
