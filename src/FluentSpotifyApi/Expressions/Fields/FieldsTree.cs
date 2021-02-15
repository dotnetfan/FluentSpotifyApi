using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSpotifyApi.Expressions.Fields
{
    internal class FieldsTree
    {
        private readonly Node root;

        public FieldsTree()
        {
            this.root = new Node(string.Empty);
        }

        public void Add(IEnumerable<string> fieldsPath, bool isExclude)
        {
            var node = this.root;

            var list = fieldsPath.ToList();
            for (var i = 0; i < list.Count; i++)
            {
                var fieldName = list[i];

                var isCurrentNodeExclude = (i == list.Count - 1) && isExclude;
                if (node.IsExclude != isCurrentNodeExclude)
                {
                    node.IsExclude = isCurrentNodeExclude;
                    node.Children = null;
                }

                var child = (node.Children ?? Enumerable.Empty<Node>()).FirstOrDefault(item => item.FieldName == fieldName);
                if (child == null)
                {
                    child = new Node(fieldName);
                    (node.Children = node.Children ?? new List<Node>()).Add(child);
                }

                node = child;
            }
        }

        public string GetFields()
        {
            var sb = new StringBuilder();
            foreach (var item in this.GetFieldsInternal(this.root))
            {
                sb.Append(item);
            }

            if (sb.Length == 0)
            {
                return null;
            }

            return sb.ToString();
        }

        private IEnumerable<string> GetFieldsInternal(Node node)
        {
            if (!(node.Children ?? Enumerable.Empty<Node>()).Any())
            {
                yield break;
            }

            yield return "(";

            if (node.IsExclude)
            {
                yield return "!";
            }

            var isFirst = true;
            foreach (var child in node.Children)
            {
                if (!isFirst)
                {
                    yield return ",";
                }

                yield return child.FieldName;

                foreach (var item in this.GetFieldsInternal(child))
                {
                    yield return item;
                }

                isFirst = false;
            }

            yield return ")";
        }

        private class Node
        {
            public Node(string fieldName)
            {
                this.FieldName = fieldName;
            }

            public string FieldName { get; }

            public bool IsExclude { get; set; }

            public IList<Node> Children { get; set; }
        }
    }
}
