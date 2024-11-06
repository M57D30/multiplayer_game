using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using windowsForms_client.Strategy;

namespace windowsForms_client
{
	public class Obstacle
	{
		public int x_coordinate { get; set; }
		public int y_coordinate { get; set; }
        public IStrategy Strategy { get; set; }
        public bool HasBeenAffected { get; set; } = false; 

        public Obstacle( int x, int y, IStrategy strategy)
		{
			this.x_coordinate = x;
			this.y_coordinate = y;
            Strategy = strategy;
        }
	}

}
