using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterator.Iterators
{
    public class ListIterator<T> : IIterator<T>
    {
        private List<T> _list;
        private int _currentIndex = 0;

        public ListIterator(List<T> list)
        {
            _list = list;
        }

        public bool HasNext()
        {
            return _currentIndex < _list.Count;
        }

        public T Next()
        {
            if (!HasNext())
            {
                throw new InvalidOperationException("No more elements.");
            }

            return _list[_currentIndex++];
        }

        public void RemoveCurrent()
        {
            if (_currentIndex - 1 < 0 || _currentIndex-1 >= _list.Count)
                throw new InvalidOperationException("Invalid operation: no current element to remove.");

            _list.RemoveAt(_currentIndex-1);
            _currentIndex--; // Adjust the index after removal
        }

        public void ReplaceCurrent(T newElement)
        {
            if (_currentIndex - 1 >= 0 && _currentIndex - 1 < _list.Count)
            {
                _list[_currentIndex - 1] = newElement;
            }
        }

        public void Reset()
        {
            _currentIndex = 0;
        }
    }
}
