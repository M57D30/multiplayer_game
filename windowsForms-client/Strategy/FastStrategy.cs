using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Strategy
{
    //You get faster
    public class FastStrategy : IStrategy
    {
        public void ApplyStrategy(Tank tank)
        {
            int curSpeed = tank.GetTankSpeedX();
           // var curType = tank.GetType();
           //Maybe depending on type add customize possible speed increase?
            if (curSpeed < 20)
            {
                //Console.WriteLine(curType.ToString());
                tank.UpdateMovement(1);
            }
        }
    }
}
