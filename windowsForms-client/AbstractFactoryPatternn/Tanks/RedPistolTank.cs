using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace windowsForms_client.Tanks
{
    internal class RedPistolTank : PistolTank
    {
        public RedPistolTank() : base() 
        {
            Color = Color.Red;
        }

        public RedPistolTank(string id, int x, int y, string name) : base(id, x, y, name)
        {
            Color = Color.Red;
        }



    }
}
