using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Decorators
{
    public class HealthDecorator : TankDecorator
    {

        public HealthDecorator(ITankComponent tank) : base(tank) { }


        public override int GetHealth()
        {
            // Add 22 extra health
            return base.GetHealth() + 22;
        }
    }
}
