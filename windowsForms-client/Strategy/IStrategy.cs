using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Strategy
{
    public interface IStrategy
    {
        void ApplyStrategy(Tank tank);
    }
}
