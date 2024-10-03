using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client
{
    internal class Tank
    {
        public string playerId { get; set; }
        public int x_coordinate {  get; set; }
        public int y_coordinate { get; set; }

        public Tank(string id, int x, int y)
        {
            this.playerId = id;
            this.x_coordinate = x;
            this.y_coordinate = y;
        }
    }
}
