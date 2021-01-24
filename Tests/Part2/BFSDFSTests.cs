using Microsoft.VisualStudio.TestTools.UnitTesting;
using StanfordAlgs.Graphs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Part2Tests
{
    [TestClass]
    public class BFSDFSTests
    {
        private AdjacencyListGraph<int> g = new AdjacencyListGraph<int>();
        private AdjacencyListGraph<int> d = new AdjacencyListGraph<int>(directed: true);

        [TestInitialize]
        public void SetupGraph()
        {
            for (int i = 0; i <= 10; i++)
            {
                g.AddNode(i);
            }

            g.AddEdgeBetweenIndices(0, 1);
            g.AddEdgeBetweenIndices(0, 2);
            g.AddEdgeBetweenIndices(0, 3);
            g.AddEdgeBetweenIndices(1, 2);
            g.AddEdgeBetweenIndices(3, 4);
            g.AddEdgeBetweenIndices(4, 5);
            g.AddEdgeBetweenIndices(4, 6);
            g.AddEdgeBetweenIndices(4, 7);
            g.AddEdgeBetweenIndices(6, 7);
            g.AddEdgeBetweenIndices(5, 6);
            g.AddEdgeBetweenIndices(8, 9);

            for (int i = 0; i <= 10; i++)
            {
                d.AddNode(i);
            }

            d.AddEdgeBetweenIndices(0, 1);
            d.AddEdgeBetweenIndices(0, 2);
            d.AddEdgeBetweenIndices(0, 3);
            d.AddEdgeBetweenIndices(1, 2);
            d.AddEdgeBetweenIndices(3, 4);
            d.AddEdgeBetweenIndices(4, 5);
            d.AddEdgeBetweenIndices(4, 7);
            d.AddEdgeBetweenIndices(6, 7);
            d.AddEdgeBetweenIndices(8, 9);
        }

        [TestMethod]
        public void BFSWorksUnirectedGraph()
        {
            Node<int> start = g.V[0];
            List<Node<int>> output = g.BFSWhere((Node<int> n) => (n.Val >= 0), start);
            Assert.AreEqual(8, output.Count);
            Assert.AreEqual(8, output.Distinct().Count());
            Assert.IsTrue(output.Contains(start));
            Assert.IsTrue(output.Contains(g.GetNode(7)));
            Assert.IsFalse(output.Contains(g.GetNode(8)));
        }

        [TestMethod]
        public void BFSWorksDirectedGraph()
        {
            Node<int> start = d.V[0];
            List<Node<int>> output = d.BFSWhere((Node<int> n) => (n.Val >= 0), start);
            Assert.AreEqual(7, output.Count);
            Assert.AreEqual(7, output.Distinct().Count());
            Assert.IsTrue(output.Contains(start));
            Assert.IsTrue(output.Contains(d.GetNode(7)));
            Assert.IsFalse(output.Contains(d.GetNode(8)));
            Assert.IsFalse(output.Contains(d.GetNode(6)));
        }

        [TestMethod]
        public void BFSFindOneWorks()
        {
            Node<int> start = g.V[0];
            List<Node<int>> output = d.BFSWhere((Node<int> n) => (n.Val >= 3), start, findOne: true);
            Assert.AreEqual(1, output.Count);
            Assert.IsTrue(output.Contains(g.GetNode(3)));
        }

        [TestMethod]
        public void DFSWorksUndirectedGraph()
        {
            Node<int> start = g.V[0];
            List<Node<int>> output = g.DFSWhere((Node<int> n) => (n.Val >= 0), start);
            Assert.AreEqual(8, output.Count);
            Assert.AreEqual(8, output.Distinct().Count());
            Assert.IsTrue(output.Contains(start));
            Assert.IsTrue(output.Contains(g.GetNode(7)));
            Assert.IsFalse(output.Contains(g.GetNode(8)));
        }

        [TestMethod]
        public void DFSWorksDirectedGraph()
        {
            Node<int> start = d.V[0];
            List<Node<int>> output = d.DFSWhere((Node<int> n) => (n.Val >= 0), start);
            Assert.AreEqual(7, output.Count);
            Assert.AreEqual(7, output.Distinct().Count());
            Assert.IsTrue(output.Contains(start));
            Assert.IsTrue(output.Contains(d.GetNode(7)));
            Assert.IsFalse(output.Contains(d.GetNode(8)));
            Assert.IsFalse(output.Contains(d.GetNode(6)));
        }

        [TestMethod]
        public void DFSFindOneWorks()
        {
            Node<int> start = g.V[0];
            List<Node<int>> output = d.DFSWhere((Node<int> n) => (n.Val >= 3), start, findOne: true);
            Assert.AreEqual(1, output.Count);
            Assert.IsTrue(output.Contains(g.GetNode(3)));
        }
    }
}