using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using windowsForms_client.Strategy;

namespace windowsForms_client.Factory
{
    //You get lost in a mist - you cannot shoot for 3 seconds
    public class Mist : Obstacle
    {
        public Mist(int x, int y, IStrategy strategy) : base(x, y, strategy)
        {
        }

    }
}
