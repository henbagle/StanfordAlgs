using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanfordAlgs
{
    // PART 1 WEEK 4
    // Random Contraction algorithm for finding the minimum cut
    internal class MinimumCut
    {
    }

    public class Graph
    {
        public List<Node> nodes = new List<Node>();
        public List<Edge> edges = new List<Edge>();

        public int addNode(int value)
        {
            nodes.Add(new Node(value));
            return nodes.Count - 1;
        }

        public void createEdge(int from, int to)
        {
            Edge newE = new Edge(nodes[from], nodes[to]);
            nodes[from].e.Add(newE);
            nodes[to].e.Add(newE);
            edges.Add(newE);
        }
    }

    public class Node
    {
        public int val { get; set; }
        public List<Edge> e = new List<Edge>();

        public Node(int v = 0)
        {
            val = v;
        }
    }

    public struct Edge
    {
        public (Node, Node) nodes { get; }

        public Edge(Node from, Node to)
        {
            nodes = (from, to);
        }
    }
}