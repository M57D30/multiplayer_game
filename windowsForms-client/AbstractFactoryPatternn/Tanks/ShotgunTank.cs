using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using windowsForms_client.Flyweight;

namespace windowsForms_client.Tanks
{
    internal abstract class ShotgunTank : Tank
    {
       

        public ShotgunTank() 
        { 
        
        }

        public ShotgunTank(string id, int x, int y, string name) : base(id, x, y, name)
        { 
        
        }

        public override void setShootingMechanism(int bulletSpeed)
        {
        }

        public override void setBullets(string tankTypeBullet)
        {
            BulletFactory.getBullet(tankTypeBullet);

        }

        public override void setTurretLookingDirections(string[] directions)
        {
            this.TankTurretLookingDirections = directions;
        }


        public override void StartShooting()
        {
            Shoot();
        }

        public override void StopShooting()
        {
        }

        public override void ShootInADirection()
        {
            if (this.TankBodyLookingDirection == "Left")
            {
                this.TankTurretLookingDirection = TankTurretLookingDirections.FirstOrDefault(d => d.Equals(TankBodyLookingDirection));
            }
            else if (this.TankBodyLookingDirection == "Right")
            {
                this.TankTurretLookingDirection = TankTurretLookingDirections.FirstOrDefault(d => d.Equals(TankBodyLookingDirection));
            }
            else if (this.TankBodyLookingDirection == "Up")
            {
                this.TankTurretLookingDirection = TankTurretLookingDirections.FirstOrDefault(d => d.Equals(TankBodyLookingDirection));
            }
            else if (this.TankBodyLookingDirection == "Down")
            {
                this.TankTurretLookingDirection = TankTurretLookingDirections.FirstOrDefault(d => d.Equals(TankBodyLookingDirection));
            }
        }

        public override void Shoot()
        {
            ShootInADirection();


            IFlyweightBullet baseShotgunBullet = BulletFactory.getBullet("Shotgun");

            string Id0 = Guid.NewGuid().ToString();
            Bullet newBullet1 = baseShotgunBullet.Create(Id0, x_coordinate, y_coordinate, TankTurretLookingDirection, -30, -30);
            bullets.Add(newBullet1);

            string Id1 = Guid.NewGuid().ToString();
            Bullet newBullet2 = baseShotgunBullet.Create(Id1, x_coordinate, y_coordinate, TankTurretLookingDirection, -10, -10);
            bullets.Add(newBullet2);

            string Id2 = Guid.NewGuid().ToString();
            Bullet newBullet3 = baseShotgunBullet.Create(Id2, x_coordinate, y_coordinate, TankTurretLookingDirection, 10, 10);
            bullets.Add(newBullet3);

            string Id3 = Guid.NewGuid().ToString();
            Bullet newBullet4 = baseShotgunBullet.Create(Id3, x_coordinate, y_coordinate, TankTurretLookingDirection, 30, 30);
            bullets.Add(newBullet4);




        }


        public override void UpdateShooting(int value)
        {


            IFlyweightBullet baseShotgunBullet = BulletFactory.getBullet("Shotgun");

            baseShotgunBullet.UpdateShooting(value);

        }

    }
}
