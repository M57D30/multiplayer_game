using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using windowsForms_client.Strategy;

namespace windowsForms_client.Factory
{
    //You sink in the mud - You get slower
    public class Mud : Obstacle
    {
        public Mud(int x, int y, IStrategy strategy) : base(x, y, strategy)
        {
        }

    }
}
