using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using windowsForms_client.BuilderPattern;
using windowsForms_client.Tanks;

namespace windowsForms_client.AbstractFactoryPatternn.Factorys
{
    internal class BlueFactory : AbstractFactory
    {
        public override Tank createPistolTank(string id, int x, int y)
        {
            Tank bluePistolTank = new BluePistolTank(id, x, y, "BluePistolTank");
            Builder builder = new PistolTankBuilder();

            return builder.StartNew(bluePistolTank).AssembleBody().AddWeapons().AddTurret().AddDecoration().GetBuildable();
        }

        public override Tank createTommyGunTank(string id, int x, int y)
        {
            Tank blueTommyGunTank = new BlueTommyGunTank(id, x, y, "BlueTommyGunTankTank");
            Builder builder = new TommyGunTankBuilder();

            return builder.StartNew(blueTommyGunTank).AssembleBody().AddWeapons().AddTurret().AddDecoration().GetBuildable();
        }
    }
}
