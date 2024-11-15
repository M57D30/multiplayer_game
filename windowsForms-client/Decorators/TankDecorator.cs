using System;
using System.Collections;
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

        public virtual int GetXSpeed()
        {
            return decoratedTank.GetXSpeed();
        }

        public virtual int GetYSpeed()
        {
            return decoratedTank.GetYSpeed();
        }

        public ITankComponent ApplyUpgrade(string upgradeType)
        {
            switch (upgradeType)
            {
                case "Gold":
                    return new HealthDecorator(this);
                case "Diamond":
                    return new SpeedXBoostDecorator(this);
                case "Emerald":
                    return new SpeedYBoostDecorator(this);
                default:
                    Console.WriteLine("Unknown upgrade type.");
                    return this;
            }
        }
    }
}
