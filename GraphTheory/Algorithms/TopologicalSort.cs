using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using GraphTheory.Common;

namespace GraphTheory.Algorithms
{
    //For an edge u->v, in topological sort, u appears before v.
    //u is the pre-requisite for v.
    public class TopologicalSort
    {
        List<SharedClasses.IEdge> _edges;
        Dictionary<int, List<int>> _adj;
        HashSet<int> _seen;

        public TopologicalSort(List<SharedClasses.IEdge> edges)
        {
            _edges = edges;
            _adj = Helper.BuildAdjList(edges);
        }

        //void BuildAdjList()
        //{
        //    _adj = new Dictionary<int, List<int>>();
        //    foreach (var edge in _edges)
        //    {
        //        if(!_adj.ContainsKey(edge.FromNode))
        //            _adj.Add(edge.FromNode,new List<int>());
        //        if (!_adj.ContainsKey(edge.ToNode))
        //            _adj.Add(edge.ToNode, new List<int>());

        //        _adj[edge.FromNode].Add(edge.ToNode);
        //    }
        //}
        //O(V+E)
        public List<int> GetToplogicalSort()
        {
            var stack = new Stack<int>();
            _seen = new HashSet<int>();
            foreach (var key in _adj.Keys)
            {
                if (_seen.Contains(key))
                    continue;
                var currentList = new List<int>();
                var path = new HashSet<int>();
                path.Add(key);
                if (HasCyle(key, stack, path))
                    return null;
            }
            return stack.ToList();
        }
        //Checks for cycle while building out the topological sort.
        bool HasCyle(int node, Stack<int> stack, HashSet<int> path)
        {
            _seen.Add(node);
            foreach (var nbor in _adj[node])
            {
                if (path.Contains(nbor))//Cycle
                    return true;
                if (_seen.Contains(nbor))//Already processed
                    continue;
                else
                {
                    path.Add(nbor);
                    if (HasCyle(nbor, stack, path))
                        return true;
                    path.Remove(nbor);
                }
            }
            stack.Push(node);
            return false;
        }
    }
}
