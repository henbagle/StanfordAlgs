using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanfordAlgs.Graphs
{
    public class Edge<T> : IComparable<Edge<T>>
    {
        public Node<T> Item1 { get; set; }
        public Node<T> Item2 { get; set; }
        private bool Directed;

        public int CompareTo(Edge<T> to)
        {
            if (Item1 == to.Item1 && Item2 == to.Item2) return 0;
            else return 1;
        }

        public bool Has(Node<T> i)
        {
            // Returns whether or not you can use this edge to get to the specified node
            if (Directed)
            {
                return (Item2 == i);
            }
            else
            {
                return (Item1 == i || Item2 == i);
            }
        }

        public bool HasIgnoreDirection(Node<T> i)
        {
            // Returns true if this edge goes to or from the specified node
            return (Item1 == i || Item2 == i);
        }

        public bool Has(Node<T> i, Node<T> i2)
        {
            // Returns if the node goes from the first node to the second node
            if (Directed)
            {
                return (Item1 == i && Item2 == i2);
            }
            else
            {
                return ((Item1 == i && Item2 == i2) || (Item1 == i2 && Item2 == i));
            }
        }

        public bool CanTraverseFrom(Node<T> i)
        {
            if (!Directed) return Has(i);
            else return (i == Item1);
        }

        public Node<T> Not(Node<T> i)
        {
            // Returns the opposite node from the node you input, regardless of directionality
            if (!HasIgnoreDirection(i)) throw new ArgumentException("Node is not in edge");
            if (Item1 != i) return i;
            else return Item2;
        }

        public Edge(Node<T> I1, Node<T> I2, bool directed = false)
        {
            Item1 = I1;
            Item2 = I2;
            Directed = directed;
        }
    }
}