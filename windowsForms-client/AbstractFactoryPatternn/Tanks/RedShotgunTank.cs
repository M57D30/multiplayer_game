using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Tanks
{
    internal class RedShotgunTank : ShotgunTank
    {
        public RedShotgunTank() : base()
        {
            Color = Color.Red;
        }

        public RedShotgunTank(string id, int x, int y, string name) : base(id, x, y, name)
        {
            Color = Color.Red;
        }
    }
}
