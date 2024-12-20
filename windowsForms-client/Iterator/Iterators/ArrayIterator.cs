using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iterator.Iterators;

namespace windowsForms_client.Iterator.Iterators
{
    public class ArrayIterator<T> : IIterator<T>
    {
        private readonly T[] _array;
        private int _currentIndex;
        private int size;

        public ArrayIterator(T[] array, int size)
        {
           // Console.WriteLine(_array.Length);
            _array = array;
            this.size = size;
            this._currentIndex = 0;
        }

        public bool HasNext()
        {
            return _currentIndex < size;
        }

        public T Next()
        {
            if (!HasNext())
            {
                throw new InvalidOperationException("No more elements.");
            }

            return _array[_currentIndex++];
        }

        public void RemoveCurrent()
        {
            if (_currentIndex < 0 || _currentIndex - 1 >= size)
                throw new InvalidOperationException("Invalid operation: no current element to remove.");

            // Shift elements to the left
            for (int i = _currentIndex - 1; i < size - 1; i++)
            {
                _array[i] = _array[i + 1];
            }

            size--;
            _currentIndex--; // Adjust the index after removal
        }

        public void ReplaceCurrent(T newElement)
        {
            if (_currentIndex >= 0 && _currentIndex < size)
            {
                _array[_currentIndex] = newElement;  // Replace the current element
            }
        }

        public void Reset()
        {
            _currentIndex = 0;
        }

    }
}
