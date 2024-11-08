using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Decorators
{
    public abstract class TankDecorator : ITankComponent
    {
        protected ITankComponent decoratedTank;

        public TankDecorator(ITankComponent tank)
        {
            this.decoratedTank = tank;
        }


        public virtual int GetHealth()
        {
            return decoratedTank.GetHealth();
        }

        public virtual int GetSpeed()
        {
            return decoratedTank.GetSpeed();
        }

        
    }
}
