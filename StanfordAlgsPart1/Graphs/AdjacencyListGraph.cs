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

        public override int NodeCount { get { return V.Count; } } // Problem: Count includes deleted nodes
        public int EdgeCount { get { return E.Count; } }

        public AdjacencyListGraph(bool directed = false) : base(directed)
        {
            V = new List<Node<T>>();
            E = new HashSet<Edge<T>>();
        }

        public override void AddEdgeBetweenIndices(int v1, int v2, int weight = 1)
        {
            if (v1 < 0 || v2 < 0)
            {
                throw new ArgumentException("Vertices are out of bounds");
            }
            AddEdge(GetNode(v1), GetNode(v2), weight);
        }

        public Edge<T> AddEdge(Node<T> n1, Node<T> n2, int weight = 1)
        {
            if (weight != 1) throw new NotImplementedException("Non-one weights not currently implemented");
            if (n1 == n2) throw new ArgumentException("Not allowed to create self loops"); // Except when you are

            Edge<T> newE = new Edge<T>(n1, n2, Directed);
            n1.AddEdge(newE);
            n2.AddEdge(newE);
            E.Add(newE);

            return newE;
        }

        // Remove an edge between two vertice indexes
        public void RemoveEdgeBetween(int v1, int v2)
        {
            if (v1 < 0 || v2 < 0)
            {
                throw new ArgumentException("Vertices are out of bounds");
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
            if (!E.Contains(edge)) throw new ArgumentException("Edge does not exist in this graph");

            E.Remove(edge);

            edge.Item1.RemoveEdge(edge);
            edge.Item2.RemoveEdge(edge);
        }

        public int AddNode(T val)
        {
            Node<T> newNode = new Node<T>(val, V.Count);
            V.Add(newNode);
            return newNode.nodeID;
        }

        public Node<T> GetNode(int id)
        {
            if (id < 0) throw new ArgumentException("Non-existant Node ID");
            Node<T> node = V.Find((Node<T> n) => (n.nodeID == id)); // We have to do this because you can delete nodes :(
            if (node == null) throw new ArgumentException("Node does not exist.");
            else return node;
        }

        public void RemoveNodeAtIndice(int id)
        {
            if (id < 0) throw new ArgumentException("Non-existant Node ID");

            // Find every edge that accesses this node. This could be refined on an undirected graph.
            Node<T> nodeToDelete = GetNode(id);
            RemoveNode(nodeToDelete);
        }

        public void RemoveNode(Node<T> n)
        {
            foreach (Edge<T> edge in n.e)
            {
                RemoveEdge(edge);
            }
            V.Remove(n);
        }

        public List<Node<T>> BFSWhere(Predicate<Node<T>> criteria, Node<T> start, bool findOne = false)
        {
            List<Node<T>> output = new List<Node<T>>();
            Queue<Node<T>> queue = new Queue<Node<T>>();
            HashSet<Node<T>> visited = new HashSet<Node<T>>();

            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                Node<T> node = queue.Dequeue();

                if (criteria(node))
                {
                    output.Add(node);
                    if (findOne) return output;
                }

                foreach (Edge<T> e in node.e)
                {
                    Node<T> edgeTarget = e.Not(node);
                    if (!visited.Contains(edgeTarget))
                    {
                        queue.Enqueue(edgeTarget);
                        visited.Add(edgeTarget);
                    }
                }
            }

            return output;
        }

        public List<Node<T>> DFSWhere(Predicate<Node<T>> criteria, Node<T> start, bool findOne = false)
        {
            List<Node<T>> output = new List<Node<T>>();
            HashSet<Node<T>> visited = new HashSet<Node<T>>();

            start.DFS(criteria, ref visited, ref output);

            if (findOne && output.Count > 0)
            {
                Node<T> outNode = output[0];
                output = new List<Node<T>>();
                output.Add(outNode);
            }
            return output;
        }

        public List<(Node<T>, int)> TopologicalSort()
        {
            // Graph MUST be: Acyclic, all connected together
            // Heyyyy this doesn't work!
            if (!Directed) throw new NotSupportedException("Cannot topologically sort an undirected graph");

            List<(Node<T>, int)> output = new List<(Node<T>, int)>();
            HashSet<Node<T>> visited = new HashSet<Node<T>>();
            int count = V.Count - 1;

            foreach (Node<T> n in V)
            {
                if (!visited.Contains(n))
                {
                    n.TopologicalSortDFS(ref count, ref visited, ref output);
                }
            }

            return output;
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
            RemoveNode(toContract.Item2);
        }

        // Deep clone of graphs
        // Will NOT preserve nodeIDs if any nodes have been deleted
        public AdjacencyListGraph<T> Clone()
        {
            AdjacencyListGraph<T> newGraph = new AdjacencyListGraph<T>(directed: this.Directed);
            Dictionary<int, int> mapToNew = new Dictionary<int, int>();

            // Copy every value to a new node in the new graph
            foreach (Node<T> node in V)
            {
                int newID = newGraph.AddNode(node.Val);
                mapToNew.Add(node.nodeID, newID);
            }

            // Re-create every edge
            foreach (Edge<T> edge in E)
            {
                Node<T> i1 = newGraph.V[mapToNew[edge.Item1.nodeID]];
                Node<T> i2 = newGraph.V[mapToNew[edge.Item2.nodeID]];
                newGraph.AddEdge(i1, i2);
            }

            return newGraph;
        }
    }
}