using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace windowsForms_client.Tanks
{
    internal class RedTommyGunTank : TommyGunTank
    {
        public RedTommyGunTank() : base() 
        {
            Color = Color.Red;
        }

        public RedTommyGunTank(string id, int x, int y, string name) : base(id, x, y, name)
        {
            Color = Color.Red;
        }
    }
}
