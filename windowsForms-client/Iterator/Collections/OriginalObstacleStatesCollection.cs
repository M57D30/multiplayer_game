using Iterator.Collections;
using Iterator.Iterators;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using windowsForms_client;
using windowsForms_client.Iterator.Iterators;
using windowsForms_client.Strategy;

namespace windowsForms_client.Iterator.Collections
{
    public class OriginalObstacleStatesCollection : ICollectionA<KeyValuePair<Obstacle, (Color OriginalColor, IStrategy OriginalStrategy)>>
    {
        private Dictionary<Obstacle, (Color OriginalColor, IStrategy OriginalStrategy)> dictionary;


        public OriginalObstacleStatesCollection()
        {
            dictionary = new Dictionary<Obstacle, (Color OriginalColor, IStrategy OriginalStrategy)>();
        }

        public IIterator<KeyValuePair<Obstacle, (Color OriginalColor, IStrategy OriginalStrategy)>> CreateIterator()
        {
            return new DictionaryIterator(dictionary);
        }

        public void Add(Obstacle key, Color originalColor, IStrategy originalStrategy)
        {
            dictionary[key] = (originalColor, originalStrategy);
        }

        public void Remove(Obstacle key)
        {
            dictionary.Remove(key);
        }

        public int Count => dictionary.Count;

        public (Color OriginalColor, IStrategy OriginalStrategy) Get(Obstacle key)
        {
            dictionary.TryGetValue(key, out var value);
            return value;
        }
    }
}
