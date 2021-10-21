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

        public KeyValuePair<TKey, TValue> this[int index]
        {
            get
            {
                if (0 <= index && index < data.Length)
                {
                    return data[index];
                }

                throw new ArgumentOutOfRangeException();
            }
        }
        
        public void Add(KeyValuePair<TKey, TValue> pair)
        {
            var dataDuplicate = new KeyValuePair<TKey, TValue>[data.Length + 1];

            for (int sourceIndex = 0, duplicateIndex = 0; sourceIndex < data.Length; ++sourceIndex, ++duplicateIndex)
            {
                var comparable = data.ToList()[sourceIndex].Value;
                if (isAscending && pair.Value.CompareTo(comparable) > 0 ||
                    !isAscending && pair.Value.CompareTo(comparable) < 0)
                {
                    dataDuplicate[duplicateIndex] = pair;
                    ++duplicateIndex;
                }
                dataDuplicate[duplicateIndex] = data[sourceIndex];
            }

            data = dataDuplicate;
        }
        
        public void Add(TKey key, TValue value)
        {
            Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public void Asc()
        {
            if (data.Length > 1 && !isAscending)
            {
                Expend();
            }
        }

        public void Desc()
        {
            if (data.Length > 1 && isAscending)
            {
                Expend();
            }
        }

        private void Expend()
        {
            int counter = data.Length / 2;
            for (int i = 0; i < counter; ++i)
            {
                var temp = data[i];
                data[i] = data[data.Length - i - 1];
                data[data.Length - i - 1] = data[i];
            }
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