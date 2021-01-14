using System;
using System.Collections.Generic;
using GraphTheory.Algorithms;
using System.Linq;
using static GraphTheory.Common.SharedClasses;

namespace GraphTheory
{
    class Program
    {
        static void Main(string[] args)
        {
            var djSet = new DisjointSet();
            var edgesForDisjoinSet = new int[8][]
            {
                new int[] {1,2},
                new int[] {2,3},
                new int[] {4,5},
                new int[] {6,7},
                new int[] {5,6},
                new int[] {3,7},
                new int[] {3,4},
                new int[] {1,6}
            };

            //foreach (var edge in edgesForDisjoinSet)
            //{
            //    var result = djSet.AddEdge(edge[0],edge[1]);
            //}

            var edgesForKruskal = new List<IEdgeWithWeight>()
            {
                new EdgeWithWeight{FromNode = 1,ToNode    = 2, Weight = 3},
                new EdgeWithWeight{FromNode = 1,ToNode    = 4, Weight = 1},
                new EdgeWithWeight{FromNode = 2,ToNode    = 3, Weight = 1},
                new EdgeWithWeight{FromNode = 2,ToNode    = 4, Weight = 3},
                new EdgeWithWeight{FromNode = 3,ToNode    = 4, Weight = 1},
                new EdgeWithWeight{FromNode = 3,ToNode    = 5, Weight = 5},
                new EdgeWithWeight{FromNode = 3,ToNode    = 6, Weight = 4},
                new EdgeWithWeight{FromNode = 4,ToNode    = 5, Weight = 6},
                new EdgeWithWeight{FromNode = 5,ToNode    = 6, Weight = 2},

            };

            //var kruskalAlgo = new KruskalAlgo(edgesForKruskal);
            //var minSpanTreeEdges = kruskalAlgo.GetMinSpanningTree();
            //var edgeSum = minSpanTreeEdges.Sum(x => x.Weight);

            var edgesForTopSort = new List<IEdge>()
            {
                new Edge {FromNode = 5, ToNode = 0},
                new Edge {FromNode = 5, ToNode = 2},
                new Edge {FromNode = 0, ToNode = 2},
                new Edge {FromNode = 0, ToNode = 3},
                new Edge {FromNode = 3, ToNode = 1},
                new Edge {FromNode = 4, ToNode = 1},
                new Edge {FromNode = 4, ToNode = 2},
            };

            //var topSort = new TopologicalSort(edgesForTopSort);
            //var resultFromTopSort = topSort.GetToplogicalSort();

            var edgesForDijkstra = new List<IEdgeWithWeight>
            {
                new EdgeWithWeight{FromNode = 0, ToNode = 1, Weight = 1},
                new EdgeWithWeight{FromNode = 1, ToNode = 0, Weight = 1},
                new EdgeWithWeight{FromNode = 0, ToNode = 2, Weight = 4},
                new EdgeWithWeight{FromNode = 2, ToNode = 0, Weight = 4},
                new EdgeWithWeight{FromNode = 1, ToNode = 2, Weight = 4},
                new EdgeWithWeight{FromNode = 2, ToNode = 1, Weight = 4},
                new EdgeWithWeight{FromNode = 1, ToNode = 3, Weight = 2},
                new EdgeWithWeight{FromNode = 3, ToNode = 1, Weight = 2},
                new EdgeWithWeight{FromNode = 1, ToNode = 4, Weight = 7},
                new EdgeWithWeight{FromNode = 4, ToNode = 1, Weight = 7},
                new EdgeWithWeight{FromNode = 2, ToNode = 3, Weight = 3},
                new EdgeWithWeight{FromNode = 3, ToNode = 2, Weight = 3},
                new EdgeWithWeight{FromNode = 2, ToNode = 4, Weight = 5},
                new EdgeWithWeight{FromNode = 4, ToNode = 2, Weight = 5},
                new EdgeWithWeight{FromNode = 3, ToNode = 4, Weight = 4},
                new EdgeWithWeight{FromNode = 4, ToNode = 3, Weight = 4},
                new EdgeWithWeight{FromNode = 3, ToNode = 5, Weight = 6},
                new EdgeWithWeight{FromNode = 5, ToNode = 3, Weight = 6},
                new EdgeWithWeight{FromNode = 4, ToNode = 5, Weight = 7},
                new EdgeWithWeight{FromNode = 5, ToNode = 4, Weight = 7}
            };

            var dijkstra = new Dijkstra(edgesForDijkstra,0);
            //var resultFromDijkstra = dijkstra.GetMinPath();

            var bellmanFord = new BellmanFord(edgesForDijkstra,0);
            //var resultFromBellmanFord = bellmanFord.GetMinPath();


            var edgesForTarjansSCC = new List<IEdge>
            {
                new Edge {FromNode = 0, ToNode = 1},
                new Edge {FromNode = 0, ToNode = 2},
                new Edge {FromNode = 1, ToNode = 3},
                new Edge {FromNode = 3, ToNode = 4},
                new Edge {FromNode = 4, ToNode = 5},
                new Edge {FromNode = 5, ToNode = 2},
                new Edge {FromNode = 2, ToNode = 1},
                //new Edge {FromNode = 5, ToNode = 0},
            };
            var tarjans = new TarjansAlgo(edgesForTarjansSCC);
            //var scc = tarjans.GetStronglyConnectedComponents();


            var edgesForTarjansBridges = new List<IEdge>
            {
                new Edge {FromNode = 0, ToNode = 1},
                new Edge {FromNode = 1, ToNode = 0},
                new Edge {FromNode = 0, ToNode = 2},
                new Edge {FromNode = 2, ToNode = 0},
                new Edge {FromNode = 1, ToNode = 2},
                new Edge {FromNode = 2, ToNode = 1},
                new Edge {FromNode = 2, ToNode = 3},
                new Edge {FromNode = 3, ToNode = 2},
                new Edge {FromNode = 3, ToNode = 4},
                new Edge {FromNode = 4, ToNode = 3},
                new Edge {FromNode = 4, ToNode = 5},
                new Edge {FromNode = 5, ToNode = 4},
                new Edge {FromNode = 5, ToNode = 6},
                new Edge {FromNode = 6, ToNode = 5},
                new Edge {FromNode = 4, ToNode = 6},
                new Edge {FromNode = 6, ToNode = 4},
                new Edge {FromNode = 5, ToNode = 7},
                new Edge {FromNode = 7, ToNode = 5},
                //new Edge {FromNode = 5, ToNode = 0},
            };
            tarjans = new TarjansAlgo(edgesForTarjansBridges);
            var bridges = tarjans.GetBridgesAndArticulationPoints();
        }
    }
}
