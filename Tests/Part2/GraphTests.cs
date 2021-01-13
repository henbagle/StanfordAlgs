using Microsoft.VisualStudio.TestTools.UnitTesting;
using StanfordAlgs.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Part2Tests
{
    [TestClass]
    public class GraphTests
    {
        [TestMethod]
        public void GraphsCanAddNodesAndEdges()
        {
            AdjacencyListGraph<int> g = new AdjacencyListGraph<int>();
            g.AddNode(1);
            g.AddNode(5);
            g.AddNode(7);
            int nodeId = g.AddNode(10);
            Assert.AreEqual(7, g.GetNode(2).Val);
            Assert.AreEqual(1, g.GetNode(0).Val);
            Assert.AreEqual(4, g.NodeCount);

            g.AddEdge(0, 1);
            g.AddEdge(1, 2);
            g.AddEdge(2, 3);
            g.AddEdge(3, 0);

            Assert.AreEqual(4, g.EdgeCount);
            Assert.AreEqual(10, g.GetNode(nodeId).Val);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => { g.GetNode(-1); });
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => { g.GetNode(4); });
        }

        [TestMethod]
        public void NodesKnowAboutUndirectedEdges()
        {
            AdjacencyListGraph<int> g = new AdjacencyListGraph<int>();
            g.AddNode(1);
            g.AddNode(5);
            g.AddNode(7);
            g.AddNode(10);
            g.AddEdge(0, 1);
            g.AddEdge(1, 2);
            g.AddEdge(2, 3);
            g.AddEdge(3, 0);

            Assert.AreEqual(2, g.GetNode(0).AdjacentNodeCount());
            Assert.AreEqual(1, g.GetNode(0).ParallelNodeCount(g.GetNode(1)));

            g.AddEdge(0, 1);
            Assert.AreEqual(2, g.GetNode(0).ParallelNodeCount(g.GetNode(1)));
        }

        [TestMethod]
        public void ParallelEdgesWorkOnUndirectedGraph()
        {
            AdjacencyListGraph<int> g = new AdjacencyListGraph<int>();
            g.AddNode(0);
            g.AddNode(1);

            g.AddEdge(0, 1);

            Assert.AreEqual(1, g.GetNode(0).ParallelNodeCount(g.GetNode(1)));
            Assert.AreEqual(1, g.GetNode(1).ParallelNodeCount(g.GetNode(0)));
            Assert.AreEqual(1, g.EdgeCount);

            g.AddEdge(1, 0);

            Assert.AreEqual(2, g.GetNode(0).ParallelNodeCount(g.GetNode(1)));
            Assert.AreEqual(2, g.GetNode(1).ParallelNodeCount(g.GetNode(0)));
            Assert.AreEqual(2, g.GetNode(1).AdjacentNodeCount()); // Fix this behavior
            Assert.AreEqual(2, g.EdgeCount);

            g.RemoveEdge(0, 1);

            Assert.AreEqual(1, g.GetNode(0).ParallelNodeCount(g.GetNode(1)));
            Assert.AreEqual(1, g.GetNode(1).ParallelNodeCount(g.GetNode(0)));
            Assert.AreEqual(1, g.EdgeCount);

            g.RemoveEdge(0, 1);

            Assert.AreEqual(0, g.GetNode(0).ParallelNodeCount(g.GetNode(1)));
            Assert.AreEqual(0, g.GetNode(1).ParallelNodeCount(g.GetNode(0)));
            Assert.AreEqual(0, g.EdgeCount);
        }

        [TestMethod]
        public void ParallelEdgesWorkOnDirectedGraphs()
        {
            AdjacencyListGraph<int> g = new AdjacencyListGraph<int>(directed: true);
            g.AddNode(0);
            g.AddNode(1);

            g.AddEdge(0, 1);

            Assert.AreEqual(1, g.GetNode(0).ParallelNodeCount(g.GetNode(1)));
            Assert.AreEqual(0, g.GetNode(1).ParallelNodeCount(g.GetNode(0)));
            Assert.AreEqual(1, g.EdgeCount);

            g.AddEdge(1, 0);

            Assert.AreEqual(1, g.GetNode(0).ParallelNodeCount(g.GetNode(1)));
            Assert.AreEqual(1, g.GetNode(1).ParallelNodeCount(g.GetNode(0)));
            Assert.AreEqual(2, g.EdgeCount);

            g.AddEdge(1, 0);

            Assert.AreEqual(1, g.GetNode(0).ParallelNodeCount(g.GetNode(1)));
            Assert.AreEqual(2, g.GetNode(1).ParallelNodeCount(g.GetNode(0)));
            Assert.AreEqual(3, g.EdgeCount);

            g.RemoveEdge(0, 1);

            Assert.AreEqual(0, g.GetNode(0).ParallelNodeCount(g.GetNode(1)));
            Assert.AreEqual(2, g.GetNode(1).ParallelNodeCount(g.GetNode(0)));
            Assert.AreEqual(2, g.EdgeCount);

            Assert.ThrowsException<ArgumentException>(() => { g.RemoveEdge(0, 1); });
        }

        [TestMethod]
        public void NodesCanBeDeleted()
        {
            AdjacencyListGraph<int> g = new AdjacencyListGraph<int>();
            g.AddNode(0);
            g.AddNode(1);
            g.AddNode(2);
            g.AddNode(3);
            g.AddNode(4);

            g.RemoveNode(1);
            Assert.AreEqual(2, g.GetNode(2).Val);
            Assert.ThrowsException<Exception>(() => { g.GetNode(1); });
            Assert.ThrowsException<Exception>(() => { g.AddEdge(0, 1); });

            g.AddEdge(2, 3);
            g.AddEdge(3, 4);
            g.RemoveNode(2);
            Assert.AreEqual(1, g.GetNode(3).AdjacentNodeCount());
        }

        [TestMethod]
        public void CanClone()
        {
            AdjacencyListGraph<int> g = new AdjacencyListGraph<int>();
            g.AddNode(0);
            g.AddNode(5);
            g.AddNode(10);
            g.AddEdge(0, 1);
            g.AddEdge(0, 2);
            g.RemoveNode(1);

            AdjacencyListGraph<int> h = g.Clone();
            Assert.AreEqual(10, h.GetNode(2).Val);
            Assert.AreEqual(1, h.GetNode(2).AdjacentNodeCount());
            Assert.ThrowsException<Exception>(() => { h.GetNode(1); });
            Assert.AreNotEqual(g.GetNode(2), h.GetNode(2));
            Assert.AreEqual(g.GetNode(2).Val, h.GetNode(2).Val);
        }
    }
}