using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            List<Bullet> bullets = new List<Bullet>();

            string Id1 = Guid.NewGuid().ToString();
            Bullet bullet1 = new Bullet(15, Id1, -30, -30);
            bullets.Add(bullet1);

            string Id2 = Guid.NewGuid().ToString();
            Bullet bullet2 = new Bullet(15, Id2, -10, -10);
            bullets.Add(bullet2);

            string Id3 = Guid.NewGuid().ToString();
            Bullet bullet3 = new Bullet(15, Id3, 10, 10);
            bullets.Add(bullet3);

            string Id4 = Guid.NewGuid().ToString();
            Bullet bullet4 = new Bullet(15, Id4, 30, 30);
            bullets.Add(bullet4);

            tank.setBullets(bullets);
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
            AddMediumArmor();
            AddMediumWheels();
            return this;
        }
    }
}
