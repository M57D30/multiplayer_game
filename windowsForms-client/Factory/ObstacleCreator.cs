using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using windowsForms_client.Factory;
using windowsForms_client.Strategy;

namespace windowsForms_client
{
	public abstract class ObstacleCreator
	{
		public abstract Obstacle CreateObstacle(int x, int y, IStrategy s);
	}
}
