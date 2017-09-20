using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Trie
    {
        private Node Root { get; }

        public Trie()
        {
            Root = new Node(null);
        }

        #region public

        public bool Add(string element)
        {
            Node node = GetNode(element, true);
            if (node.IsTerminal)
            {
                return false;
            }
            node.IsTerminal = true;
            Update(node);
            return true;
        }

        public bool Contains(string element)
        {
            return GetNode(element, false)?.IsTerminal ?? false;
        }

        public bool Remove(string element)
        {
            if (!Contains(element))
            {
                return false;
            }
            Remove(GetNode(element, false));
            return true;
        }

        public int Size()
        {
            return Root.ElementsCount;
        }

        public int HowManyStartsWithPrefix(string prefix)
        {
            Node node = GetNode(prefix, false);
            return node?.ElementsCount ?? 0;
        }

        #endregion

        #region private

        private Node GetNode(string element, bool createIfNotExist)
        {
            Node node = Root;
            foreach (char c in element)
            {
                if (!node.Children.ContainsKey(c))
                {
                    if (!createIfNotExist)
                    {
                        return null;
                    }
                    node.Children.Add(c, new Node(node, c));
                }
                node = node.Children[c];
            }
            return node;
        }

        private void Update(Node node)
        {
            while (node != null)
            {
                node.Update();
                node = node.Parent;
            }
        }

        private void Remove(Node node)
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }
            node.IsTerminal = false;
            Update(node);
            while (!IsRoot(node) && node.IsLeaf && !node.IsTerminal)
            {
                var p = node.Parent;
                p.Children.Remove(node.Symbol);
                node = p;
            }
        }

        private bool IsRoot(Node node)
        {
            return node.Parent == null;
        }

        #endregion

        private class Node
        {
            public Dictionary<char, Node> Children { get; } = new Dictionary<char, Node>();
            public bool IsTerminal { get; set; }

            public char Symbol { get; }

            public bool IsLeaf => Children.Count == 0;

            public int ElementsCount { get; set; } = 0;

            public Node Parent { get; }

            public Node(Node parent, char c = '\0', bool isTerminal = false)
            {
                IsTerminal = isTerminal;
                Symbol = c;
                Parent = parent;
            }

            public void Update()
            {
                ElementsCount = Children.Sum(p => p.Value.ElementsCount) + Convert.ToInt32(IsTerminal);
            }
        }
    }
}