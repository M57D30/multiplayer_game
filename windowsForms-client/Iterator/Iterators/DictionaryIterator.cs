using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iterator.Iterators;
using windowsForms_client.Strategy;

namespace windowsForms_client.Iterator.Iterators
{
    public class DictionaryIterator : IIterator<KeyValuePair<Obstacle, (Color OriginalColor, IStrategy OriginalStrategy)>>
    {
        private readonly List<KeyValuePair<Obstacle, (Color OriginalColor, IStrategy OriginalStrategy)>> items;
        private int position = 0;

        public DictionaryIterator(Dictionary<Obstacle, (Color OriginalColor, IStrategy OriginalStrategy)> dictionary)
        {
            items = new List<KeyValuePair<Obstacle, (Color OriginalColor, IStrategy OriginalStrategy)>>(dictionary);
        }

        public bool HasNext()
        {
            return position < items.Count;
        }

        public KeyValuePair<Obstacle, (Color OriginalColor, IStrategy OriginalStrategy)> Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("No more elements in the collection.");

            return items[position++];
        }

        public void RemoveCurrent()
        {
            if (position - 1 < 0 || position - 1 >= items.Count)
                throw new InvalidOperationException("No current element to remove.");

            items.RemoveAt(position - 1);
            position--;
        }

        public void ReplaceCurrent(KeyValuePair<Obstacle, (Color OriginalColor, IStrategy OriginalStrategy)> newElement)
        {
            if (position - 1 < 0 || position - 1 >= items.Count)
                throw new InvalidOperationException("No current element to replace.");

            items[position - 1] = newElement;
        }

        public void Reset()
        {
            position = 0;
        }
    }
}
