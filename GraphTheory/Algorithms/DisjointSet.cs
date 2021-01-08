using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory.Algorithms
{
    public class DisjointSet
    {
        Dictionary<int,TreeNode> _map = null;

        public DisjointSet()
        {
            _map = new Dictionary<int, TreeNode>();
        }

        void MakeSet(int val)
        {
            if (!_map.ContainsKey(val))
            {
                var node = new TreeNode {Data = val};
                node.Parent = node;
                _map[val] = node;
            }

        }

        TreeNode GetParent(TreeNode node)
        {
            if (node.Parent == node)
                return node;
            node.Parent = GetParent(node.Parent);
            return node.Parent;
        }

        void AddToSet(int fromVal, int toVal)
        {
            MakeSet(fromVal);
            MakeSet(toVal);
        }

        bool IsInSameSet(TreeNode fromNode, TreeNode toNode)
        {
            GetParent(fromNode);
            GetParent(toNode);
            return (fromNode.Parent!=null && fromNode.Parent == toNode.Parent);
        }

        void Unify(TreeNode fromNode, TreeNode toNode)
        {
            var fromParent = fromNode.Parent;
            var toParent = toNode.Parent;

            if (fromParent.Rank > toParent.Rank)
                SetParent(fromParent, toParent);
            else
                SetParent(toParent, fromParent);
        }

        void SetParent(TreeNode parent, TreeNode child)
        {
            child.Parent = parent;
            if (parent.Rank == child.Rank)
                parent.Rank++;
            child.Rank = 0;
        }

        public bool AddEdge(int fromVal, int toVal)
        {
            AddToSet(fromVal,toVal);

            var fromNode = _map[fromVal];
            var toNode = _map[toVal];

            if (IsInSameSet(fromNode, toNode))
                return false;

            Unify(fromNode,toNode);

            return true;
        }
    }

    class TreeNode
    {
        public int Data { get; set; }
        public TreeNode Parent { get; set; }
        public int Rank { get; set; } = 0;
    }
}
