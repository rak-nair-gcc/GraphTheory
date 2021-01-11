using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphTheory.Common;

namespace GraphTheory.Algorithms
{
    //Sort edges by weight
    //Create a Disjoint Set
    //Unify all edges
    //Returns Min Spanning Tree
    //ElogE + E 
    public class KruskalAlgo
    {
        List<SharedClasses.IEdgeWithWeight> _edges;
        DisjointSet _set;

        public KruskalAlgo(List<SharedClasses.IEdgeWithWeight> edges)
        {
            _edges = edges;
            _set = new DisjointSet();
        }

        public List<SharedClasses.IEdgeWithWeight> GetMinSpanningTree()
        {
            var result = new List<SharedClasses.IEdgeWithWeight>();

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
            _edges.Sort(Comparer<SharedClasses.IEdgeWithWeight>.Create((x, y) => { return x.Weight.CompareTo(y.Weight); }));
        }

    }
}
