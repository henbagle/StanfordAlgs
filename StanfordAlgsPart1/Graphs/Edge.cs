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
            return (Item1 == i || Item2 == i);
        }

        public bool Has(Node<T> i, Node<T> i2)
        {
            if (Directed)
            {
                return (Item1 == i && Item2 == i2);
            }
            else
            {
                return ((Item1 == i && Item2 == i2) || (Item1 == i2 && Item2 == i));
            }
        }

        public Node<T> Not(Node<T> i)
        {
            if (!Has(i)) throw new ArgumentException("Node is not in edge");
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