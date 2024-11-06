using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Strategy
{
    //You get stopped for 3 seconds
    public class StuckStrategy: IStrategy
    {
        public void ApplyStrategy(Tank tank)
        {
            tank.StartFreeze();
        }
    }
}
