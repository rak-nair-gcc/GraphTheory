using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GraphTheory.Common;

namespace GraphTheory.Algorithms
{
    //Get Single Source, all other nodes shortest path when there are NO negative weight cycles.
    //Add source to PQ with priority 0
    //Use a PQ to get the next lowest PQ node, then relax all edges from this node,
    //      i.e. if d[u] + uv < d[w], d[w] = d[u] + uv, parent[w] = u and priority[w] = d[w]
    //Continue until PQ no longer returns nodes.
    // VlogV + ElogV, E - edges, V - nodes.
    public class Dijkstra
    {
        List<SharedClasses.IEdgeWithWeight> _edges;
        int _source;
        IndexedMinPQ _pQ;
        Dictionary<int, Dictionary<int, int>> _adj;//Adj List, key is source, values is <dest, edge weight>
        Dictionary<int, int> _parents;//Key is current node, Value is the parent
        Dictionary<int, int> _currMinDistances;// Key is current node, Value is current min Distance
        HashSet<int> _processed;//All Processed nodes.

        public Dijkstra(List<SharedClasses.IEdgeWithWeight> edges, int source)
        {
            _edges = edges;
            _source = source;
            _pQ = new IndexedMinPQ();
            _adj = Helper.BuildAdjListWithWeights(_edges);
            _parents = new Dictionary<int, int>();
            _currMinDistances = new Dictionary<int, int>();
            _processed = new HashSet<int>();
        }

        //VlogV + ElogV
        public Dictionary<int, int> GetMinPath()
        {
            _pQ.Add(0,_source);
            _currMinDistances.Add(_source,0);

            while (_pQ.CanExtractMin())
            {
                var min = _pQ.ExtractMin();//Executed V times, logV complexity
                _processed.Add(min);

                foreach (var nbor in _adj[min])//Executed E times in all
                {
                    if (_processed.Contains(nbor.Key))
                        continue;
                    var newDist = _currMinDistances[min] + nbor.Value;
                    if (!_currMinDistances.ContainsKey(nbor.Key))
                    {
                        _pQ.Add(newDist, nbor.Key);//log V complexity
                        _parents.Add(nbor.Key,min);
                        _currMinDistances.Add(nbor.Key, newDist);
                    }
                    else
                    {
                        var currMinDist = _currMinDistances[nbor.Key];
                        if (newDist < currMinDist)
                        {
                            _pQ.DecreaseKey(newDist, nbor.Key);//log V complexity
                            _parents[nbor.Key] = min;
                            _currMinDistances[nbor.Key] = newDist;
                        }
                    }
                }
            }
            return _parents;
        }
    }
}
