using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanfordAlgs.Graphs
{
    public class Node<T>
    {
        public T Val { get; }
        public int nodeID { get; }
        public HashSet<Edge<T>> e = new HashSet<Edge<T>>();
        public HashSet<Edge<T>> incomingEdges = new HashSet<Edge<T>>();

        public Node(T value, int id)
        {
            Val = value;
            nodeID = id;
        }

        public Node(int id)
        {
            nodeID = id;
        }

        public void AddEdge(Edge<T> v)
        {
            if (v.CanTraverseFrom(this))
            {
                e.Add(v);
            }
            else
            {
                incomingEdges.Add(v);
            }
        }

        public bool IsConnectedTo(Node<T> v)
        {
            return e.Any((Edge<T> edge) => { return edge.Has(v); });
        }

        public int ParallelNodeCount(Node<T> v)
        {
            return e.Where((Edge<T> edge) => { return edge.Has(v); }).Count();
        }

        public void RemoveEdge(Edge<T> v)
        {
            e.Remove(v);
            incomingEdges.Remove(v);
        }

        public int AdjacentNodeCount()
        {
            // Goal: This should output the number of connected nodes disregarding parallel edges. It currently doesn't.

            // This doesn't work.
            //var query = e.Select<Edge<T>, Node<T>>((edge, index) => (edge.Not(this)));
            //return query.Distinct().Count();

            return e.Count;
        }

        internal void DFS(Predicate<Node<T>> criteria, ref HashSet<Node<T>> visited, ref List<Node<T>> output)
        {
            visited.Add(this);
            if (criteria(this))
            {
                output.Add(this);
            }

            foreach (Edge<T> edge in e)
            {
                Node<T> edgeTarget = edge.Not(this);
                if (!visited.Contains(edgeTarget))
                {
                    edgeTarget.DFS(criteria, ref visited, ref output);
                }
            }
        }

        internal void TopologicalSortDFS(ref int count, ref HashSet<Node<T>> visited, ref List<(Node<T>, int)> output)
        {
            visited.Add(this);

            foreach (Edge<T> edge in e)
            {
                Node<T> edgeTarget = edge.Not(this);
                if (!visited.Contains(edgeTarget))
                {
                    edgeTarget.TopologicalSortDFS(ref count, ref visited, ref output);
                }
                output.Add((this, count));
                count--;
            }
        }
    }
}