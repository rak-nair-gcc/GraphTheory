using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GraphTheory.Common
{
    public class IndexedMinPQ
    {
        List<HeapObject> _heap;
        Dictionary<int, int> _map; //Key is ItemKey, Value is index of the element in _heap.
                                   //Used to backup the index in the _heap for retrieval and tweaking the priority.

        public IndexedMinPQ()
        {
            _heap = new List<HeapObject>();
            _map = new Dictionary<int, int>();
        }

        public void Add(int priority, int key)//Value is the key
        {
            if(_map.ContainsKey(key))
                throw new InvalidOperationException("Key already exists.");
            _map[key] = _heap.Count;
            _heap.Add(new HeapObject
            {
                ItemKey = key,
                Priority = priority
            });
            SwimUp(_heap.Count-1);
        }

        public bool CanExtractMin()
        {
            return _heap!=null && _heap.Count > 0;
        }

        public int ExtractMin()
        {
            if(_heap.Count==0)
                throw new InvalidOperationException("No elements in the Heap");
            
            Swap(0,_heap.Count-1);
            var minElem = _heap[_heap.Count - 1];
            _heap.RemoveAt(_heap.Count - 1);
            _map.Remove(minElem.ItemKey);
            SwimDown(0);
            return minElem.ItemKey;
        }

        public void DecreaseKey(int newPriority, int key)
        {
            if(!_map.ContainsKey(key))
                throw new InvalidOperationException("Key does not exist.");

            var itemIndex = _map[key];
            _heap[itemIndex].Priority = newPriority;
            SwimUp(itemIndex);
            SwimDown(itemIndex);
        }

        void SwimUp(int index)
        {
            if (index == 0)
                return;
            var parentIndex = (index - 1) / 2;
            var parentPriority = _heap[parentIndex].Priority;
            var selfPriority = _heap[index].Priority;

            if(selfPriority<parentPriority)
            { 
                Swap(parentIndex,index);
                SwimUp(parentIndex);
            }
        }

        void SwimDown(int index)
        {
            var leftChildIndex = 2 * index + 1;
            if (leftChildIndex >= _heap.Count)
                return;
            var smallestChildIndex = GetSmallestPriorityChildIndex(index);
            if (_heap[smallestChildIndex].Priority < _heap[index].Priority)
            {
                Swap(smallestChildIndex,index);
                SwimDown(smallestChildIndex);
            }
        }

        int GetSmallestPriorityChildIndex(int index)
        {
            var leftChildIndex = 2 * index + 1;
            var rightChildIndex = 2 * index + 2;

            if (rightChildIndex < _heap.Count)
            {
                if (_heap[leftChildIndex].Priority < _heap[rightChildIndex].Priority)
                    return leftChildIndex;
                else
                    return rightChildIndex;
            }
            else
                return leftChildIndex;
        }

        void Swap(int indexA, int indexB)
        {
            var elemAInMap = _heap[indexA].ItemKey;
            var elemBInMap = _heap[indexB].ItemKey;
            var temp = _heap[indexA];

            _heap[indexA] = _heap[indexB];
            _heap[indexB] = temp;

            _map[elemAInMap] = indexB;
            _map[elemBInMap] = indexA;
        }

        class HeapObject
        {
            public int Priority { get; set; }
            public int ItemKey { get; set; }
        }
    }
}