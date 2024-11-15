using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Decorators
{
    public class SpeedXBoostDecorator : TankDecorator
    {
        public SpeedXBoostDecorator(ITankComponent tank) : base(tank) { }


        public override int GetXSpeed()
        {
            // Increase speed by 5
            return base.GetXSpeed() + 5;
        }
    }
}
