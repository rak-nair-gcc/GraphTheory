using System;
using System.Collections.Generic;
using GraphTheory.Algorithms;
using System.Linq;

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

            var edgesForKruskal = new List<Common.EdgeWithWeight>()
            {
                new Common.EdgeWithWeight{FromNode = 1,ToNode    = 2, Weight = 3},
                new Common.EdgeWithWeight{FromNode = 1,ToNode    = 4, Weight = 1},
                new Common.EdgeWithWeight{FromNode = 2,ToNode    = 3, Weight = 1},
                new Common.EdgeWithWeight{FromNode = 2,ToNode    = 4, Weight = 3},
                new Common.EdgeWithWeight{FromNode = 3,ToNode    = 4, Weight = 1},
                new Common.EdgeWithWeight{FromNode = 3,ToNode    = 5, Weight = 5},
                new Common.EdgeWithWeight{FromNode = 3,ToNode    = 6, Weight = 4},
                new Common.EdgeWithWeight{FromNode = 4,ToNode    = 5, Weight = 6},
                new Common.EdgeWithWeight{FromNode = 5,ToNode    = 6, Weight = 2},

            };

            var kruskalAlgo = new KruskalAlgo(edgesForKruskal);
            var minSpanTreeEdges = kruskalAlgo.GetMinSpanningTree();
            var edgeSum = minSpanTreeEdges.Sum(x => x.Weight);

        }
    }
}
