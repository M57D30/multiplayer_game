using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using windowsForms_client.Strategy;

namespace windowsForms_client.Factory
{
    //You sink in the mud - You get slower

    public class Mud : Obstacle
    {
        private int curSpeed;
        private bool speedLimit;
        public Mud(int x, int y, IStrategy strategy) : base(x, y, strategy)
        {
        }
        public sealed override Color GetDefaultColor()
        {
            return Color.SaddleBrown;
        }
        protected sealed override void ApplyEffect(Tank tank)
        {
            if (!HasBeenAffected)
            {
                curSpeed = tank.GetTankSpeedX();
                if (curSpeed > 5)
                {
                    tank.UpdateMovement(-1);
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
            return speedLimit ? $"Speed limit is hit, no effect applied" : $"Speed decreased to {this.curSpeed}";
        }
    }
}
