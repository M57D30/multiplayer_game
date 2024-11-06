using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Strategy
{
    //You cannot shoot for 3 seconds
    public class BlindnessStrategy : IStrategy
    {
        public void ApplyStrategy(Tank tank)
        {
            tank.StartBulletFreeze();
        }
    }
}
