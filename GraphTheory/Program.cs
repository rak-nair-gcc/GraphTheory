using GraphTheory.Algorithms;

namespace GraphTheory
{
    class Program
    {
        static void Main(string[] args)
        {
            var djSet = new DisjointSet();
            var edges = new int[8][]
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

            foreach (var edge in edges)
            {
                var result = djSet.AddEdge(edge[0],edge[1]);
            }
        }
    }
}
