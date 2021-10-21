using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SolutionTree.Collections
{
    internal class SortedDictionaryByValue<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>> where TValue : IComparable
    {
        private KeyValuePair<TKey, TValue>[] data;
        private bool isAscending = true;
        
        public SortedDictionaryByValue()
        {
            data = Array.Empty<KeyValuePair<TKey, TValue>>();
        }

        public KeyValuePair<TKey, TValue> GetFirst()
        {
            return data[0];
        }

        public void Add(KeyValuePair<TKey, TValue> pair)
        {
            var dataDuplicate = new KeyValuePair<TKey, TValue>[data.Length + 1];

            bool isAdded = false;
            
            for (int sourceIndex = 0, duplicateIndex = 0; sourceIndex < data.Length; ++sourceIndex, ++duplicateIndex)
            {
                var comparable = data.ToList()[sourceIndex].Value;
                if (isAscending && pair.Value.CompareTo(comparable) > 0 ||
                    !isAscending && pair.Value.CompareTo(comparable) < 0)
                {
                    isAdded = true;
                    dataDuplicate[duplicateIndex] = pair;
                    ++duplicateIndex;
                }
                dataDuplicate[duplicateIndex] = data[sourceIndex];
            }

            if (!isAdded)
            {
                // I've never seen that form before
                // It is last element access  
                dataDuplicate[^1] = pair;
            }
            
            data = dataDuplicate;
        }
        
        public void Add(TKey key, TValue value)
        {
            Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return data.ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}