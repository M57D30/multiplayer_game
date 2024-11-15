using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Decorators
{
    public class SpeedYBoostDecorator : TankDecorator
    {
        public SpeedYBoostDecorator(ITankComponent tank) : base(tank) { }


        public override int GetYSpeed()
        {
            // Increase speed by 5
            return base.GetYSpeed() + 5;
        }
    }
}
