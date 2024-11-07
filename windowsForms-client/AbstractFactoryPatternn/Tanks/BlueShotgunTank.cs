using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace windowsForms_client.Tanks
{
    internal class BlueShotgunTank : ShotgunTank
    {
        public BlueShotgunTank() : base()
        {
            Color = Color.Blue;
        }

        public BlueShotgunTank(string id, int x, int y, string name) : base(id, x, y, name)
        {
            Color = Color.Blue;
        }
    }
}
