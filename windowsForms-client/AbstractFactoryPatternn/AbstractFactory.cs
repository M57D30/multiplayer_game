using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.AbstractFactoryPatternn
{
    internal abstract class AbstractFactory
    {
        public abstract Tank createPistolTank(string id, int x, int y);

        public abstract Tank createTommyGunTank(string id, int x, int y);

        public abstract Tank createShotgunTank(string id, int x, int y);
    }
}
