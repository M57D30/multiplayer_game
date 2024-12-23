﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using windowsForms_client.Flyweight;

namespace windowsForms_client.BuilderPattern
{
    internal class PistolTankBuilder : Builder
    {

        public void AddLightArmor()
        {

        }

        public void AddFastBullets()
        {
            tank.setBullets("Pistol");
        }

        public void AddFastWheels()
        {
            tank.setMovementSpeed(15, 15);
        }

        public void AddFourDirectionTurret()
        {
            string[] directions = new string[] { "Left", "Right", "Up", "Down" };

            tank.setTurretLookingDirections(directions);
        }

        public void AddFunnyMustache()
        {

        }

        public override Builder AddWeapons()
        {
            AddFastBullets();
            return this;
        }

        public override Builder AssembleBody()
        {
            this.isBodyAssembled = true;
            AddLightArmor();
            AddFastWheels();
            return this;
        }

        public override Builder AddTurret()
        {
            this.isTurretAdded = true;
            AddFourDirectionTurret();
            return this;
        }

        public override Builder AddDecoration()
        {
            AddFunnyMustache();
            return this;
        }
    }
}
