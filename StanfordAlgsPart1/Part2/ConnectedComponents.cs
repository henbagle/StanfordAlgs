using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StanfordAlgs.Graphs;

namespace StanfordAlgs.Part2
{
    public class ConnectedComponents
    {
        public static List<List<Node<T>>> GetConnectedComponentsBFS<T>(AdjacencyListGraph<T> g)
        {
            if (g.Directed) throw new NotSupportedException("Not supported on a directed graph with BFS implementation.");
            List<List<Node<T>>> components = new List<List<Node<T>>>();
            HashSet<Node<T>> visited = new HashSet<Node<T>>();

            // Loop over each node, in case each one is their own connected component
            foreach (Node<T> start in g.V)
            {
                // If we haven't seen it yet, do a BFS adding every node to a list
                if (!visited.Contains(start))
                {
                    List<Node<T>> currentComponent = new List<Node<T>>();
                    Queue<Node<T>> queue = new Queue<Node<T>>();

                    // Do BFS
                    queue.Enqueue(start);
                    visited.Add(start);
                    while (queue.Count > 0)
                    {
                        Node<T> node = queue.Dequeue();
                        currentComponent.Add(node);

                        foreach (Edge<T> e in node.e)
                        {
                            if (e.CanTraverseFrom(node))
                            {
                                Node<T> edgeTarget = e.Not(node);
                                if (!visited.Contains(edgeTarget))
                                {
                                    queue.Enqueue(edgeTarget);
                                    visited.Add(edgeTarget);
                                }
                            }
                        }
                    }

                    // That list is a connected component
                    components.Add(currentComponent);
                }
            }

            return components;
        }

        public static List<List<Node<T>>> StronglyConnectedComponents<T>(AdjacencyListGraph<T> g)
        {
            if (!g.Directed) throw new ArgumentException("Graph must be directed");

            HashSet<Node<T>> visited = new HashSet<Node<T>>(); // List of nodes we've visited
            List<List<Node<T>>> output = new List<List<Node<T>>>();
            List<Node<T>> leaders = new List<Node<T>>(); // List of nodes in reverse order of components.

            // Do first pass - do DFSs backwards going from heads to tails
            foreach (Node<T> n in g.V)
            {
                if (!visited.Contains(n))
                {
                    SCCFirstPassDFS<T>(n, ref visited, ref leaders);
                }
            }

            // Second pass - work backwards from ranking, doing normal DFSs to find components.
            // Will be ordered in such a way that you will peel back SCCs one layer at a time.
            visited = new HashSet<Node<T>>();
            for (int i = leaders.Count - 1; i >= 0; i--)
            {
                if (!visited.Contains(leaders[i]))
                {
                    output.Add(SCCSecondPassDFS(leaders[i], ref visited));
                }
            }

            Console.WriteLine(output.Sum((List<Node<T>> l) => (l.Count)));

            return output;
        }

        private static void SCCFirstPassDFS<T>(Node<T> start, ref HashSet<Node<T>> visited, ref List<Node<T>> output)
        {
            // I wish this didn't exist.
            Stack<Node<T>> st = new Stack<Node<T>>();
            List<Node<T>> thisTree = new List<Node<T>>();
            st.Push(start);
            visited.Add(start);

            while (st.Count > 0)
            {
                Node<T> thisNode = st.Pop();
                thisTree.Add(thisNode);
                foreach (Edge<T> e in thisNode.incomingEdges)
                {
                    Node<T> targetNode = e.Item1;
                    if (!visited.Contains(targetNode))
                    {
                        st.Push(targetNode);
                        visited.Add(targetNode);
                    }
                }
            }

            // Add to output list after all calls have finished.
            // "Deeper" nodes (Higher up because we're working in a reversed graph) get added before lower ones.
            // By then navigating through the list with lower nodes first (in reverse), you'll get each SCC one at a time

            // If we did this recursively, we wouldn't need to maintain a seperate list that we then reverse.
            // With recursion, the (reversed) graph A -> B -> C would add CBA to the list. Since we're doing it with a stack,
            // we get ABC which we need to reverse before adding to the rest of the output list.
            thisTree.Reverse();
            output.AddRange(thisTree);
        }

        private static List<Node<T>> SCCSecondPassDFS<T>(Node<T> start, ref HashSet<Node<T>> visited)
        {
            // Build up a connected component
            List<Node<T>> thisComponent = new List<Node<T>>();
            Stack<Node<T>> st = new Stack<Node<T>>();
            st.Push(start);
            visited.Add(start);

            while (st.Count > 0)
            {
                Node<T> thisNode = st.Pop();
                thisComponent.Add(thisNode);
                foreach (Edge<T> e in thisNode.e)
                {
                    Node<T> targetNode = e.Item2;
                    if (!visited.Contains(targetNode))
                    {
                        st.Push(targetNode);
                        visited.Add(targetNode);
                    }
                }
            }

            return thisComponent;
        }
    }
}