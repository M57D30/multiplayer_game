using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Strategy
{
    //You get slower
    public class SlowStrategy : IStrategy
    {
        public void ApplyStrategy(Tank tank)
        {
            int curSpeed = tank.GetTankSpeedX();
            if (curSpeed > 5)
            {
                tank.UpdateMovement(-1);
            }
        }
    }
}
