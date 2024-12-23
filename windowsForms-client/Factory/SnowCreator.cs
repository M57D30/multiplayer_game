﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using windowsForms_client.Strategy;

namespace windowsForms_client.Factory
{
    public class SnowCreator : ObstacleCreator
    {
        public override Obstacle CreateObstacle(int x, int y, IStrategy s)
        {
            return new Snow(x, y, s);
        }
    }
}
   