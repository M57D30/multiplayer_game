using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iterator.Iterators;

namespace Iterator.Collections
{
    public interface ICollectionA<T>
    {
        IIterator<T> CreateIterator();
    }
}
