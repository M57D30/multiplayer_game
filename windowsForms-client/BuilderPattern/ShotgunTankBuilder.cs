using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using windowsForms_client.Flyweight;
using windowsForms_client.Tanks;

namespace windowsForms_client.BuilderPattern
{
    internal class ShotgunTankBuilder : Builder
    {
        public void AddMediumArmor()
        {

        }

        public void AddMediumBulletsMagazine()
        {

            tank.setBullets("Shotgun");
        }

        public void AddMediumWheels()
        {
            tank.setMovementSpeed(5, 20);
        }

        public void AddFourDirectionTurret()
        {
            string[] directions = new string[] { "Left", "Right", "Up", "Down" };

            tank.setTurretLookingDirections(directions);
        }

        public void AddPurpleCrown()
        {

        }


        public override Builder AddDecoration()
        {
            AddPurpleCrown();
            return this;
        }

        public override Builder AddTurret()
        {
            this.isTurretAdded = true;
            AddFourDirectionTurret();
            return this;
        }

        public override Builder AddWeapons()
        {
            AddMediumBulletsMagazine();
            return this;
        }

        public override Builder AssembleBody()
        {
            this.isBodyAssembled = true;
            AddMediumArmor();
            AddMediumWheels();
            return this;
        }
    }
}
