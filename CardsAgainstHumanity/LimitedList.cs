using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsAgainstHumanity
{
    internal class LimitedList<T> : IEnumerable<T>
    {
        private readonly int _capacity;
        protected List<T> _list;

        public int Count => _list.Count;
        public bool IsFull => _capacity <= Count;

        public T this[int index] => _list[index];

        public LimitedList(int capacity)
        {
            _capacity = capacity;
            _list = new List<T>(_capacity);
        }

        public void Add(T item)
        {
            ArgumentNullException.ThrowIfNull(item, "item");
            if (IsFull)
            {
                _list.RemoveAt(0);
            }
            _list.Add(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in _list)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool Remove(T item) => _list.Remove(item);
    }
}
