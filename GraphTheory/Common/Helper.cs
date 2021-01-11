using System.Collections.Generic;

namespace GraphTheory.Common
{
    public static class Helper
    {
        public static Dictionary<int, List<int>> BuildAdjList(List<SharedClasses.IEdge> edges)
        {
            var _adj = new Dictionary<int, List<int>>();
            foreach (var edge in edges)
            {
                if (!_adj.ContainsKey(edge.FromNode))
                    _adj.Add(edge.FromNode, new List<int>());
                if (!_adj.ContainsKey(edge.ToNode))
                    _adj.Add(edge.ToNode, new List<int>());

                _adj[edge.FromNode].Add(edge.ToNode);
            }

            return _adj;
        }

        public static Dictionary<int, Dictionary<int, int>> BuildAdjListWithWeights(List<SharedClasses.IEdgeWithWeight> edges)
        {
            var _adj = new Dictionary<int, Dictionary<int, int>>();
            foreach (var edge in edges)
            {
                if (!_adj.ContainsKey(edge.FromNode))
                {
                    _adj[edge.FromNode] = new Dictionary<int, int>();
                }
                //_adj.Add(edge.FromNode, new List<int>());
                if (!_adj.ContainsKey(edge.ToNode))
                {
                    _adj[edge.ToNode] = new Dictionary<int, int>();
                }
                //_adj.Add(edge.ToNode, new List<int>());

                _adj[edge.FromNode].Add(edge.ToNode,edge.Weight);
            }

            return _adj;
        }
    }
}
