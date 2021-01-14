using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphTheory.Common;

namespace GraphTheory.Algorithms
{
    public class BellmanFord
    {
        List<SharedClasses.IEdgeWithWeight> _edges;
        int _source;
        Dictionary<int, int> _parents; //Key is current node, Value is the parent
        Dictionary<int, int> _currMinDistances; // Key is current node, Value is current min Distance

        public BellmanFord(List<SharedClasses.IEdgeWithWeight> edges, int source)
        {
            _parents = new Dictionary<int, int>();
            _currMinDistances = new Dictionary<int, int>();
            _edges = edges;
            _source = source;
            InitialiseMinDistances();
        }

        public Dictionary<int, int> GetMinPath()
        {
            _currMinDistances[_source] = 0;
            for (int i = 0; i <= _currMinDistances.Keys.Count; i++)
            {
                bool wasUpdateMade = false;
                foreach (var edge in _edges)
                {
                    var sourceVal = _currMinDistances[edge.FromNode];
                    if (sourceVal == int.MaxValue)
                        continue;
                    var currDist = sourceVal + edge.Weight;
                    var destVal = _currMinDistances[edge.ToNode];
                    if (currDist < destVal)
                    {
                        _currMinDistances[edge.ToNode] = currDist;
                        _parents[edge.ToNode] = edge.FromNode;
                        wasUpdateMade = true;
                        if (i == _currMinDistances.Keys.Count) // Negative weight cycle after V-1 iterations.
                            return null;
                    }
                }

                if (!wasUpdateMade)
                    break;
            }
            return _parents;
        }

        void InitialiseMinDistances()
        {
            foreach (var edge in _edges)
            {
                if(!_currMinDistances.ContainsKey(edge.FromNode))
                    InitialiseInDictionary(edge.FromNode);
                if (!_currMinDistances.ContainsKey(edge.ToNode))
                    InitialiseInDictionary(edge.ToNode);
            }
        }

        void InitialiseInDictionary(int key)
        {
            if (!_currMinDistances.ContainsKey(key))
                _currMinDistances.Add(key, int.MaxValue);
        }
    }
}
