using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanfordAlgs
{
    public abstract class GraphBase<T>
    {
        public readonly bool Directed;

        public GraphBase(bool directed = false)
        {
            this.Directed = directed;
        }

        public abstract void AddEdgeBetweenIndices(int v1, int v2, int weight = 1);

        public abstract int NodeCount { get; }
    }
}