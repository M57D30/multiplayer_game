using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using windowsForms_client.Strategy;

namespace windowsForms_client.Factory
{
    //You fall into snow - You get stopped for 3 seconds
    public class Snow : Obstacle
    {
        public Snow(int x, int y, IStrategy strategy) : base(x, y, strategy)
        {
        }
        public sealed override Color GetDefaultColor()
        {
            return Color.LightBlue;
        }
        protected sealed override void ApplyEffect(Tank tank)
        {
            if (!HasBeenAffected)
            {
                tank.StartFreeze();
                HasBeenAffected = true; 
            }
        }
        protected sealed override string LogEffectDetails()
        {
            return $"Tank is frozen for 5 seconds";
        }
    }
}
