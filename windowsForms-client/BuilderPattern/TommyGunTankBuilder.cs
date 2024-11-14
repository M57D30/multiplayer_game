using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.BuilderPattern
{
    internal class TommyGunTankBuilder : Builder
    {
        public void AddFullAutoShooting()
        {
            tank.setShootingMechanism(100);
        }

        public void AddHeavyArmor()
        {

        }

        public void AddSlowWheels()
        {
            tank.setMovementSpeed(5, 5);
        }

        public void AddSlowBullets()
        {
            tank.setBullets(10);
        }

        public void AddTwoDirectionTurret()
        {
            string[] directions = new string[] { "Left", "Right" };

            tank.setTurretLookingDirections(directions);
        }

        public void AddGoldenHat()
        {

        }


        public override Builder AddWeapons()
        {
            AddFullAutoShooting();
            AddSlowBullets();
            return this;
        }

        public override Builder AssembleBody()
        {
            this.isBodyAssembled = true;
            AddHeavyArmor();
            AddSlowWheels();
            return this;
        }

        public override Builder AddTurret()
        {
            this.isTurretAdded = true;
            AddTwoDirectionTurret();
            return this;
        }

        public override Builder AddDecoration()
        {
            AddGoldenHat();
            return this;
        }
    }
}
