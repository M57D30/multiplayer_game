using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using windowsForms_client.Strategy;

namespace windowsForms_client.Factory
{
    //You slip on ice - You get faster
    public class Ice : Obstacle
    {
        public Ice(int x, int y, IStrategy strategy) : base(x, y, strategy)
        {
        }

    }
}
