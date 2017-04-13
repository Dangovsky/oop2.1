using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tree.Exeption;

namespace Tree.Unit
{
    public enum WalkType
    {
        PreOrder, InOrder, PostOrder, BreadthFirst
    }
    
    public delegate void ActionDelegateForNode<T>(Node<T> node);
    public delegate void ActionDelegateForValue<T>(T value);

    public class Tree<T> : Node<T>
    {
        private Node<T> root;

        public Node<T> Root { get { return root; } }

        public Tree(Node<T> root)
        {
            if (root == null)
            {
                throw new TreeException("Root is null");
            }
            root.tree = this;
            root.parent = null;
            this.root = root;
        }
        
        public void ForEach(WalkType type, ActionDelegateForNode<T> actionDelegate) 
        {
            Stack<Node<T>> stack = new Stack<Node<T>>();
            switch (type)
            {
                case WalkType.PreOrder:
                    if (root == null)
                    {
                        throw new TreeException("Root is null");
                    }
                    root.UnVisit();                  
                    stack.Push(root);
                    while (stack.Count != 0)
                    {
                        Node<T> node = stack.Pop();
                        if (node == null)
                        {
                            continue;
                        }
                        actionDelegate(node);                        
                        for (int i = node.ChildCount - 1; i >= 0; i--)
                        {
                            stack.Push(node.Leafs[i]);
                        }
                    }
                    break;
                case WalkType.InOrder:
                    if (root == null)
                    {
                        throw new TreeException("Root is null");
                    }
                    root.UnVisit();            
                    stack.Push(root);
                    while (stack.Count != 0)
                    {
                        Node<T> node = stack.Pop();
                        if (node == null)
                        {
                            continue;
                        }
                        if (node.Leafs.Count == 0 | node.visit)
                        {
                            actionDelegate(node);
                            continue;
                        }
                        for (int i = node.Leafs.Count - 1; i > node.ChildCount / 2 -1 ; i--) 
                        {
                            stack.Push(node.Leafs[i]);
                        }
                        stack.Push(node);
                        for (int i = node.ChildCount / 2 - 1; i >= 0; i--) 
                        {
                            stack.Push(node.Leafs[i]);
                        }
                        node.visit = true;
                    }
                    break;

                case WalkType.PostOrder:
                    if (root == null)
                    {
                        throw new TreeException("Root is null");
                    }
                    root.UnVisit();
                    stack.Push(root);
                    while (stack.Count != 0) 
                    {
                        Node<T> node = stack.Pop();
                        if (node == null)
                        {
                            continue;
                        }                        
                        if (node.Leafs.Count == 0 | node.visit)
                        {
                            actionDelegate(node);
                            continue;
                        }
                        stack.Push(node);
                        for (int i = node.ChildCount-1; i >= 0; i--)
                        {
                            stack.Push(node.Leafs[i]);
                        }
                        node.visit = true;
                    }
                    break;
                case WalkType.BreadthFirst:
                    if (root == null)
                    {
                        throw new TreeException("Root is null");
                    }
                    Queue<Node<T>> queue1 = new Queue<Node<T>>();
                    queue1.Enqueue(root);
                    while (queue1.Count != 0)
                    {
                        Node<T> node = queue1.Dequeue();
                        if (node == null)
                        {
                            continue;
                        }
                        actionDelegate(node);
                        foreach (Node<T> nodeCycle in node.Leafs)
                        {
                            queue1.Enqueue(nodeCycle);
                        }                        
                    }
                    break;
                default:
                    break;
            }
        }

        public void ForEach(WalkType type, ActionDelegateForValue<T> actionDelegate)
        {
            Stack<Node<T>> stack = new Stack<Node<T>>();
            switch (type)
            {
                case WalkType.PreOrder:
                    if (root == null)
                    {
                        throw new TreeException("Root is null");
                    }
                    stack.Push(root);
                    while (stack.Count != 0)
                    {
                        Node<T> node = stack.Pop();
                        if (node == null)
                        {
                            continue;
                        }
                        actionDelegate(node.value);
                        foreach (Node<T> nodeCycle in node.Leafs)
                        {
                            stack.Push(nodeCycle);
                        }
                    }
                    break;
                case WalkType.InOrder:
                    if (root == null)
                    {
                        throw new TreeException("Root is null");
                    }
                    root.UnVisit();
                    stack.Push(root);
                    while (stack.Count != 0)
                    {
                        Node<T> node = stack.Pop();
                        if (node == null)
                        {
                            continue;
                        }
                        if (node.Leafs.Count == 0 | node.visit)
                        {
                            actionDelegate(node.value);
                            continue;
                        }
                        for (int i = 0; i <= node.Leafs.Count / 2; i++)
                        {
                            stack.Push(node.Leafs[i]);
                        }
                        stack.Push(node);
                        for (int i = node.Leafs.Count / 2 + 1; i < node.Leafs.Count; i++)
                        {
                            stack.Push(node.Leafs[i]);
                        }
                        node.visit = true;
                    }
                    break;
                case WalkType.PostOrder:
                    if (root == null)
                    {
                        throw new TreeException("Root is null");
                    }
                    root.UnVisit();
                    stack.Push(root);
                    while (stack.Count != 0)
                    {
                        Node<T> node = stack.Pop();
                        if (node == null)
                        {
                            continue;
                        }
                        if (node.Leafs.Count == 0 | node.visit)
                        {
                            actionDelegate(node.value);
                            continue;
                        }
                        for (int i = 0; i <= node.Leafs.Count / 2; i++)
                        {
                            stack.Push(node.Leafs[i]);
                        }
                        for (int i = node.Leafs.Count / 2 + 1; i < node.Leafs.Count; i++)
                        {
                            stack.Push(node.Leafs[i]);
                        }
                        stack.Push(node);
                        node.visit = true;
                    }
                    break;
                case WalkType.BreadthFirst:
                    if (root == null)
                    {
                        throw new TreeException("Root is null");
                    }
                    Queue<Node<T>> queue1 = new Queue<Node<T>>();
                    queue1.Enqueue(root);
                    while (queue1.Count != 0)
                    {
                        Node<T> node = queue1.Dequeue();
                        if (node == null)
                        {
                            continue;
                        }
                        actionDelegate(node.value);
                        foreach (Node<T> nodeCycle in node.Leafs)
                        {
                            queue1.Enqueue(nodeCycle);
                        }
                    }
                    break;
                default:
                    break;
            }

        }
    }
}
