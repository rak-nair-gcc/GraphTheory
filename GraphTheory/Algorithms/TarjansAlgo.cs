using GraphTheory.Common;
using System;
using System.Collections.Generic;

namespace GraphTheory.Algorithms
{
    //DFS O(V+E), track DiscoveryTime and EarliestAccessibleNode for each node.
    public class TarjansAlgo
    {
        int _time;
        readonly List<SharedClasses.IEdge> _edges;
        Dictionary<int, List<int>> _adj;
        Dictionary<int, int> _parents;
        Dictionary<int, NodeDiscAndLow> _nodeDict;
        Stack<int> _myStack;
        HashSet<int> _inStack;
        List<SharedClasses.IEdge> _criticalEdges; //Edges which when removed no longer allow full traversal of the graph
        List<List<int>> _components; //List of strongly connected components
        List<SharedClasses.IEdge> _bridges;
        HashSet<int> _artPoints;

        public TarjansAlgo(List<SharedClasses.IEdge> edges)
        {
            _edges = edges;
            _adj = Helper.BuildAdjList(_edges);
            _time = 0;
            _parents = new Dictionary<int, int>();
            _nodeDict = new Dictionary<int, NodeDiscAndLow>();
            _myStack = new Stack<int>();
            _inStack = new HashSet<int>();
            _criticalEdges = new List<SharedClasses.IEdge>();
            _components = new List<List<int>>();
            _bridges = new List<SharedClasses.IEdge>();
            _artPoints = new HashSet<int>();
        }

        public List<List<int>> GetStronglyConnectedComponents()
        {
            foreach (var node in _adj.Keys)
            {
                if (!_nodeDict.ContainsKey(node))
                    RunDFSForSCC(node);
            }
            return _components;
        }

        public Tuple<HashSet<int>,List<SharedClasses.IEdge>> GetBridgesAndArticulationPoints()
        {
            foreach (var node in _adj.Keys)
            {
                if (!_nodeDict.ContainsKey(node))
                    RunDFSForBridges(node, -1);
            }
            return new Tuple<HashSet<int>, List<SharedClasses.IEdge>>(_artPoints,_bridges); 
        }
        //O(V+E)
        void RunDFSForBridges(int node, int parent)
        {
            _nodeDict[node] = new NodeDiscAndLow { DiscoveryTime = _time, EarliestAccessibleNode = _time };
            _time++;
            if (parent != -1)
                _parents.Add(node, parent);
            var childrenCount = 0; //Track children count
            foreach (var nbor in _adj[node])
            {
                childrenCount++;
                if (nbor == parent)
                    continue;
                if (_nodeDict.ContainsKey(nbor))
                {
                    _nodeDict[node].EarliestAccessibleNode =
                        Math.Min(_nodeDict[node].EarliestAccessibleNode, _nodeDict[nbor].DiscoveryTime);
                }
                else
                {
                    RunDFSForBridges(nbor, node);
                    _nodeDict[node].EarliestAccessibleNode =
                        Math.Min(_nodeDict[node].EarliestAccessibleNode, _nodeDict[nbor].EarliestAccessibleNode);

                    if (_nodeDict[nbor].EarliestAccessibleNode > _nodeDict[node].DiscoveryTime)//Bridge
                        _bridges.Add(new SharedClasses.Edge { FromNode = node, ToNode = nbor });

                    if (parent == -1 && childrenCount > 1) //If this is the root, and it has 2 or more children, it must be an articulation point.
                        _artPoints.Add(node);
                    else if (parent != -1 && _nodeDict[nbor].EarliestAccessibleNode >= _nodeDict[node].DiscoveryTime)//IF not root, this is an Articulation point
                        _artPoints.Add(node);
                }
            }
        }
        //O(V+E)
        void RunDFSForSCC(int node)
        {
            _nodeDict[node] = new NodeDiscAndLow { DiscoveryTime = _time, EarliestAccessibleNode = _time };
            _time++;
            _inStack.Add(node);
            _myStack.Push(node);
            foreach (var nbor in _adj[node])
            {
                if (_nodeDict.ContainsKey(nbor)) //Already processed
                {
                    if (_inStack.Contains(nbor)) //BackEdge, ignore cross edges
                    {//For BE u->v, EAN (Earliest Accessible Node) is Min(u.EAN,v.DiscoveryTime)
                        _nodeDict[node].EarliestAccessibleNode =
                            Math.Min(_nodeDict[node].EarliestAccessibleNode, _nodeDict[nbor].DiscoveryTime);
                    }
                }
                else
                {
                    RunDFSForSCC(nbor);
                    //Update EarliestAccessibleNode
                    _nodeDict[node].EarliestAccessibleNode =
                        Math.Min(_nodeDict[node].EarliestAccessibleNode, _nodeDict[nbor].EarliestAccessibleNode);

                }
            }
            //When exiting check if this is the start of a strongly connected component
            if (_nodeDict[node].EarliestAccessibleNode == _nodeDict[node].DiscoveryTime) //Is a strongly connected component 
            {
                var component = new List<int>();
                PopFromStack(_nodeDict[node].EarliestAccessibleNode, component);
                _components.Add(component);
            }
        }

        void PopFromStack(int value, List<int> component)
        {
            while (_myStack.Count > 0)
            {
                var topVal = _myStack.Peek();
                if (_nodeDict.ContainsKey(topVal) && topVal != value)
                {
                    _inStack.Remove(topVal);
                    component.Add(_myStack.Pop());
                }
                else
                {
                    component.Add(_myStack.Pop());
                    break;
                }
            }
        }

        class NodeDiscAndLow
        {
            public int DiscoveryTime { get; set; } //Tracks when node was discovered
            public int EarliestAccessibleNode { get; set; } //Tracks the earliest discovered node one can get to from the given node.
        }
    }
}
