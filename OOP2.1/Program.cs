using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tree.Unit;
using Tree.Exeption;

namespace OOP2._1
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree<string> tree = new Tree<string>(new Node<string>("f", new List<Node<string>>
            {new Node<string>("b", new List<Node<string>>{
            new Node<string>("a"), new Node<string>("d",new List<Node<string>> {
            new Node<string>("c"),new Node<string>("e")})})
            ,new Node<string>("g",new List<Node<string>>{new Node<string>("i",new List<Node<string>>{
            new Node<string>("h")})})}));

            ActionDelegateForNode<string> action = node => Console.Write(node.value + " ");

            tree.ForEach(WalkType.InOrder, action);
            Console.WriteLine();
            tree.ForEach(WalkType.PreOrder, action);
            Console.WriteLine();
            tree.ForEach(WalkType.PostOrder, action);
            Console.WriteLine();
            tree.ForEach(WalkType.BreadthFirst, action);
            Console.WriteLine();
                        
            try
            {
                Node<string> node = null;
                tree.Root.Remove(node);
            }
            catch (TreeException e)
            {
                Console.WriteLine(e.Message);
            }            
            
            Console.ReadKey();
        }
    }
}
