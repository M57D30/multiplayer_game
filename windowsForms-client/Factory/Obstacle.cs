using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using windowsForms_client.Strategy;

namespace windowsForms_client
{
	public abstract class Obstacle
	{
		public int x_coordinate { get; set; }
		public int y_coordinate { get; set; }
        public IStrategy Strategy { get; set; }
        public bool HasBeenAffected { get; set; } = false;

        private Color OriginalColor { get; set; }
        private Color TempColor { get; set; }
        public Obstacle( int x, int y, IStrategy strategy)
		{
			this.x_coordinate = x;
			this.y_coordinate = y;
            Strategy = strategy;
        }
        public void SetTempColor(Color color)
        {
            TempColor = color;
        }
        public void SetOriginalColor(Color color)
        {
            OriginalColor = color;
        }
        public Color GetTempColor()
        {
            return this.TempColor;
        }
        public void ResetColor()
        {
            TempColor = OriginalColor;
        }

        //TEMPLATE
        public abstract Color GetDefaultColor();
        protected abstract void ApplyEffect(Tank tank);
        protected abstract string LogEffectDetails();
        private void LogStep()
        {
            Console.WriteLine($"You stepped on a {this.GetType().Name}! - {LogEffectDetails()}");
        }
        public void CheckAndApplyEffect(Tank tank)
        {
            if (IsCollidingWithTank(tank))
            {
                if (!HasBeenAffected)  
                {
                    ApplyEffect(tank);
                    LogStep(); 
                }
            }
            else
            {
                if (HasBeenAffected)  
                {
                    ResetEffect(tank);
                }
            }
        }
        private bool IsCollidingWithTank(Tank tank)
        {
            Rectangle tankRect = new Rectangle(tank.x_coordinate, tank.y_coordinate, 50, 50);
            Rectangle obstacleRect = new Rectangle(this.x_coordinate, this.y_coordinate, 50, 50);
            return tankRect.IntersectsWith(obstacleRect);
        }
        private void ResetEffect(Tank tank)
        {
            if (HasBeenAffected)
            {
                HasBeenAffected = false;
            }
        }
    }
}
