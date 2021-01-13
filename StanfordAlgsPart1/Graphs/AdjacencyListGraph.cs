using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanfordAlgs.Graphs
{
    public class AdjacencyListGraph<T> : GraphBase<T>
    {
        // Nodes are stored as a list because we occationally need to access them directly
        public List<Node<T>> V { get; }

        public HashSet<Edge<T>> E { get; }

        public AdjacencyListGraph(bool directed = false) : base(directed)
        {
            V = new List<Node<T>>();
            E = new HashSet<Edge<T>>();
        }

        public override int NodeCount { get { return V.Count; } } // Problem: Count includes deleted nodes
        public int EdgeCount { get { return E.Count; } }

        public override void AddEdge(int v1, int v2, int weight = 1)
        {
            if (v1 >= V.Count || v2 >= V.Count || v1 < 0 || v2 < 0)
            {
                throw new ArgumentOutOfRangeException("Vertices are out of bounds");
            }

            if (weight != 1) throw new NotImplementedException("Non-one weights not currently implemented");

            // Don't allow us to create self-loop edges
            if (v1 != v2)
            {
                Edge<T> newE = new Edge<T>(GetNode(v1), GetNode(v2), Directed);
                E.Add(newE);

                // Add the new edge to at least one node
                GetNode(v1).AddEdge(newE);
                if (this.Directed == false)
                {
                    GetNode(v2).AddEdge(newE);
                }
            }
        }

        // Remove an edge between two vertice indexes
        public void RemoveEdge(int v1, int v2)
        {
            if (v1 >= V.Count || v2 >= V.Count || v1 < 0 || v2 < 0)
            {
                throw new ArgumentOutOfRangeException("Vertices are out of bounds");
            }

            Node<T> n1 = GetNode(v1);
            Node<T> n2 = GetNode(v2);

            if (n1.IsConnectedTo(n2))
            {
                RemoveEdge(n1.e.Where((Edge<T> e) => { return e.Has(n2); }).First());
            }
            // Handle having the order wrong in an undirected graph
            else if (!Directed && n2.IsConnectedTo(n1))
            {
                RemoveEdge(n2.e.Where((Edge<T> e) => { return e.Has(n1); }).First());
            }
            else
            {
                throw new ArgumentException("No edge exists between those nodes");
            }
        }

        // Overload of RemoveEdge that works directly by reference
        public void RemoveEdge(Edge<T> edge)
        {
            if (!E.Contains(edge)) throw new ArgumentException("Edge does not exist");

            E.Remove(edge);

            edge.Item1.RemoveEdge(edge);
            if (Directed == false)
            {
                edge.Item2.RemoveEdge(edge);
            }
        }

        public int AddNode(T val)
        {
            Node<T> newNode = new Node<T>(val, V.Count);
            V.Add(newNode);
            return newNode.nodeID;
        }

        private int AddNode()
        {
            // Add valueless node. Used when deep cloning.
            Node<T> newNode = new Node<T>(V.Count);
            V.Add(newNode);
            return newNode.nodeID;
        }

        public Node<T> GetNode(int id)
        {
            if (id < 0 || id >= V.Count) throw new ArgumentOutOfRangeException("Non-existant Node ID");
            if (V.ElementAt(id) == null) throw new Exception("Node has been deleted");
            return V.ElementAt(id);
        }

        public void RemoveNode(int id)
        {
            if (id < 0 || id >= V.Count) throw new ArgumentOutOfRangeException("Non-existant Node ID");
            if (V.ElementAt(id) == null) throw new Exception("Node has already been deleted");

            // Find every edge that accesses this node. This could be refined on an undirected graph.
            List<Edge<T>> edgesForRemoval = new List<Edge<T>>();

            foreach (Edge<T> edge in E)
            {
                if (edge.Item1 == GetNode(id) || edge.Item2 == GetNode(id)) { edgesForRemoval.Add(edge); }
            }

            // Remove that edge
            foreach (Edge<T> edge in edgesForRemoval)
            {
                RemoveEdge(edge);
            }

            // Remove this node. We keep the position in the list so as to not move other nodes around.
            V[id] = null;
        }

        public void Contract(Edge<T> toContract)
        {
            if (Directed) throw new NotSupportedException("Contracting edges is not supported on directed graphs");

            // Take all edges pointing to Item2 and redirect them to Item1

            List<Edge<T>> edgesToDelete = new List<Edge<T>>();
            foreach (Edge<T> edge in toContract.Item2.e)
            {
                // Delete any edges that will become self-loops
                if (edge.Has(toContract.Item1, toContract.Item2))
                {
                    edgesToDelete.Add(edge);
                }
                else
                {
                    // Redirect all other edges on Item2
                    if (edge.Item1 == toContract.Item2) edge.Item1 = toContract.Item1;
                    else if (edge.Item2 == toContract.Item2) edge.Item2 = toContract.Item1;
                    toContract.Item1.e.Add(edge);
                }
            }

            // Remove problematic selfloops
            foreach (Edge<T> edge in edgesToDelete)
            {
                RemoveEdge(edge);
            }

            // Remove Item2
            toContract.Item2.e = new HashSet<Edge<T>>(); // This is just for safety
            V[toContract.Item2.nodeID] = null;
        }

        // Deep clone of graphs
        public AdjacencyListGraph<T> Clone()
        {
            AdjacencyListGraph<T> newGraph = new AdjacencyListGraph<T>(directed: this.Directed);

            // Copy every value to a new node in the new graph
            foreach (Node<T> node in V)
            {
                // Handle deleted nodes (can't add null to a list, want to keep all nodeIDs the same)
                if (node == null)
                {
                    int del = newGraph.AddNode();
                    newGraph.RemoveNode(del);
                }
                else
                {
                    newGraph.AddNode(node.Val);
                }
            }

            // Re-create every edge
            foreach (Edge<T> edge in E)
            {
                newGraph.AddEdge(edge.Item1.nodeID, edge.Item2.nodeID);
            }

            return newGraph;
        }
    }
}