using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraphTheory.Common;

namespace GraphTheory.Algorithms
{
    //Sort edges by weight
    //Create a Disjoint Set
    //Unify all edges
    //Returns Min Spanning Tree
    //ElogE + E 
    public class KruskalAlgo
    {
        List<EdgeWithWeight> _edges;
        DisjointSet _set;

        public KruskalAlgo(List<EdgeWithWeight> edges)
        {
            _edges = edges;
            _set = new DisjointSet();
        }

        public List<EdgeWithWeight> GetMinSpanningTree()
        {
            var result = new List<EdgeWithWeight>();

            SortEdgeList();

            foreach (var edge in _edges)
            {
                if(_set.AddEdge(edge.FromNode,edge.ToNode))
                    result.Add(edge);
            }

            return result;
        }

        void SortEdgeList()
        {
            _edges.Sort(Comparer<EdgeWithWeight>.Create((x, y) => { return x.Weight.CompareTo(y.Weight); }));
        }

    }
}
