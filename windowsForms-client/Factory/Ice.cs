using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using windowsForms_client.Strategy;

namespace windowsForms_client.Factory
{
    //You slip on ice - You get faster
    public class Ice : Obstacle
    {
        private int curSpeed;
        private bool speedLimit;
        public Ice(int x, int y, IStrategy strategy) : base(x, y, strategy)
        {
        }
        public sealed override Color GetDefaultColor()
        {
            return Color.LightSteelBlue;
        }
        protected sealed override void ApplyEffect(Tank tank)
        {
            if (!HasBeenAffected)
            {
                curSpeed = tank.GetTankSpeedX();
                if (curSpeed < 20)
                {
                    tank.UpdateMovement(1);
                    speedLimit = false;
                }
                else
                {
                    speedLimit = true;
                }
                HasBeenAffected = true; 
            }
        }
        protected sealed override string LogEffectDetails()
        {
            return speedLimit ? $"Speed limit is hit, no effect applied" : $"Speed increased to {this.curSpeed}";
        }


    }
}
