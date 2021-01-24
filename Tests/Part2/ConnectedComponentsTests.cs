using Microsoft.VisualStudio.TestTools.UnitTesting;
using StanfordAlgs.Graphs;
using StanfordAlgs.Part2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Part2Tests
{
    [TestClass]
    public class ConnectedComponentsTests
    {
        private AdjacencyListGraph<int> g = new AdjacencyListGraph<int>();
        private AdjacencyListGraph<int> d = new AdjacencyListGraph<int>(directed: true);
        private AdjacencyListGraph<int> SCCs = new AdjacencyListGraph<int>(directed: true);

        [TestInitialize]
        public void SetupGraph()
        {
            for (int i = 0; i <= 10; i++)
            {
                g.AddNode(i);
                d.AddNode(i);
                SCCs.AddNode(i);
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

            d.AddEdgeBetweenIndices(0, 1);
            d.AddEdgeBetweenIndices(0, 2);
            d.AddEdgeBetweenIndices(0, 3);
            d.AddEdgeBetweenIndices(1, 2);
            d.AddEdgeBetweenIndices(3, 4);
            d.AddEdgeBetweenIndices(4, 5);
            d.AddEdgeBetweenIndices(4, 7);
            d.AddEdgeBetweenIndices(6, 7);
            d.AddEdgeBetweenIndices(8, 9);

            SCCs.AddEdgeBetweenIndices(0, 1);
            SCCs.AddEdgeBetweenIndices(1, 2);
            SCCs.AddEdgeBetweenIndices(2, 0);
            SCCs.AddEdgeBetweenIndices(1, 3);

            SCCs.AddEdgeBetweenIndices(3, 4);
            SCCs.AddEdgeBetweenIndices(4, 5);
            SCCs.AddEdgeBetweenIndices(5, 3);
            SCCs.AddEdgeBetweenIndices(3, 6);

            SCCs.AddEdgeBetweenIndices(6, 7);
            SCCs.AddEdgeBetweenIndices(7, 8);
            SCCs.AddEdgeBetweenIndices(8, 6);

            SCCs.AddEdgeBetweenIndices(9, 10);
            SCCs.AddEdgeBetweenIndices(10, 9);
        }

        [TestMethod]
        public void FindsConnectedComponentsWithBFS()
        {
            List<List<Node<int>>> components = ConnectedComponents.GetConnectedComponentsBFS(g);
            List<Node<int>> oneComponent = g.DFSWhere((Node<int> n) => (true), g.V[0]);

            // Checks there are the right number of components
            Assert.AreEqual(3, components.Count);

            // Verifys the first component is all connected
            CollectionAssert.AreEquivalent(oneComponent, components[0]);

            // Verify a lone component works
            Assert.AreEqual(g.V[10], components[2][0]);
        }

        [TestMethod]
        public void FindsStronglyConnectedComponentsWithDFS()
        {
            List<List<Node<int>>> components = ConnectedComponents.StronglyConnectedComponents(SCCs);
        }
    }
}