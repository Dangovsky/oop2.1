using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tree.Exeption;

namespace Tree.Unit
{
    public delegate int CompairDelegate<T>(Node<T> node1, Node<T> node2);

    public class Node<T>
    {
        public T value;
        private List<Node<T>> leafs;
        internal Node<T> parent;
        internal Tree<T> tree;
        internal bool visit;
    
        public bool isEmpty
        {
            get
            {
                if (leafs.Count == 0 && value == null)
                {
                    return true;
                }
                return false;
            }
        }
        public int ChildCount { get { return leafs.Count; } }
        public List<Node<T>> Leafs { get { return leafs; } }
        public Node<T> Parent { get { return parent; } }
        public Tree<T> Tree { get { return tree; } }

        public Node()
        {
            leafs = new List<Node<T>>();
            visit = false;
        }

        public Node(T value)
        {
            this.value = value;
            leafs = new List<Node<T>>();
            visit = false;
        }

        public Node(List<Node<T>> leafs)
        {
            if (leafs.Count == 0)
            {
                throw new TreeException("List is empty");
            }
            this.leafs = leafs;
            visit = false;
            foreach (Node<T> node in leafs)
            {
                node.parent = this;
                node.tree = this.tree;
                node.visit = false;
            }
        }

        public Node(Node<T> leaf)
        {
            if (leaf == null)
            {
                throw new TreeException("Leaf is null");
            }
            visit = false;
            leafs = new List<Node<T>>();
            leaf.parent = this;
            leaf.tree = this.tree;
            leaf.visit = false;
            leafs.Add(leaf);
        }

        public Node(T value, List<Node<T>> leafs)
        {
            if (leafs.Count == 0)
            {
                throw new TreeException("List is empty");
            }
            if (leafs[0].value.GetType() != value.GetType())
            {
                throw new TreeException("Different data types");
            }
            this.value = value;
            this.leafs = leafs;
            visit = false;
            foreach (Node<T> node in leafs)
            {
                node.parent = this;
                node.tree = this.tree;
                node.visit = false;
            }
        }

        public Node(T value, Node<T> leaf)
        {
            if (leaf == null)
            {
                throw new TreeException("Leaf is null");
            }
            if (value.GetType() != leaf.value.GetType())
            {
                throw new TreeException("Different data types");
            }
            this.value = value;
            this.leafs = new List<Node<T>>();
            visit = false;
            leaf.parent = this;
            leaf.tree = this.tree;
            leaf.visit = false;
            this.leafs.Add(leaf);
        }

        public void Add(Node<T> node)
        {
            if (node == null)
            {
                throw new TreeException("Node is null");
            }
            if (node.value.GetType() != value.GetType())
            {
                throw new TreeException("Different data types");
            }
            node.parent = this;
            node.tree = this.tree;
            node.visit = false;
        }

        public void Add(List<Node<T>> nodes)
        {
            if (nodes.Count == 0)
            {
                throw new TreeException("List is empty");
            }
            if (nodes[0].value.GetType() != value.GetType())
            {
                throw new TreeException("Different data types");
            }
            foreach (Node<T> node in nodes)
            {
                Add(node);
            }
        }

        public void Add(T value)
        {
            if (value.GetType() != this.value.GetType())
            {
                throw new TreeException("Different data types");
            }
            Node<T> node = new Node<T>(value);
            leafs.Add(node);
        }

        public int Remove(Node<T> node)
        {
            if (node == null)
            {
                throw new TreeException("Node is null");
            }
            if (node.value.GetType() != value.GetType())
            {
                throw new TreeException("Different data types");
            }
            int count = 0;
            for (int i = 0; i < leafs.Count; i++)
            {
                if (leafs[i].Equals(node))
                {
                    leafs.Remove(node);
                    count++;
                }
            }
            return count;
        }

        public int Remove(List<Node<T>> nodes)
        {
            if (nodes.Count == 0)
            {
                throw new TreeException("List is empty");
            }
            if (nodes[0].value.GetType() != value.GetType())
            {
                throw new TreeException("Different data types");
            }
            int count = 0;
            foreach (Node<T> node in leafs)
            {
                count += Remove(node);
            }
            return count;
        }

        public int Remove(T value)
        {
            if (value.GetType() != this.value.GetType())
            {
                throw new TreeException("Different data types");
            }
            int count = 0;
            for (int i = 0; i < leafs.Count; i++)
            {
                if (leafs[i].value.Equals(value))
                {
                    leafs.Remove(leafs[i]);
                    count++;
                }
            }
            return count;
        }

        public void Clear()
        {
            value = default(T);
            leafs = new List<Node<T>>();
            parent = null;
            tree = null;
        }

        public void SortNodes(CompairDelegate<T> compairDelegate)
        {
            for (int i = 0; i < leafs.Count; i++)
            {
                for (int j = 0; j < leafs.Count - i - 1; j++)
                {
                    if (compairDelegate(leafs[j], leafs[j + 1]) > 0)
                    {
                        Node<T> temp = leafs[j];
                        leafs[j] = leafs[j + 1];
                        leafs[j + 1] = temp;
                    }
                }
            }
        }

        internal void UnVisit()
        {
            visit = false;
            foreach (Node<T> node in leafs)
                node.UnVisit();
        }
    }
}
