using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterator.Iterators
{
    public interface IIterator<T>
    {
        bool HasNext();
        T Next();
        void Reset();
        void RemoveCurrent();
        void ReplaceCurrent(T newElement);
    }
}
