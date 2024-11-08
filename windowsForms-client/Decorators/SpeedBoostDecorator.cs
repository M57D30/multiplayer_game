using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Decorators
{
    public class SpeedBoostDecorator : TankDecorator
    {
        public SpeedBoostDecorator(ITankComponent tank) : base(tank) { }


        public override int GetSpeed()
        {
            // Increase speed by 5
            return base.GetSpeed() + 5;
        }
    }
}
