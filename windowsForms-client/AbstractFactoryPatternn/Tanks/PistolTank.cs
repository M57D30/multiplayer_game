using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using windowsForms_client.Flyweight;

namespace windowsForms_client.Tanks
{
    internal abstract class PistolTank : Tank
    {

        public PistolTank() : base()
        {
        }

        public PistolTank(string id, int x, int y, string name) : base(id, x, y, name)
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

           
            IFlyweightBullet basePistolBullet = BulletFactory.getBullet("Pistol");

            string Id = Guid.NewGuid().ToString();
            Bullet newBullet = basePistolBullet.Create(Id, x_coordinate, y_coordinate, TankTurretLookingDirection, 0, 0);

            bullets.Add(newBullet);
           // bullets2.Add(newBullet);

        }


        public override void UpdateShooting(int value)
        {
            IFlyweightBullet basePistolBullet = BulletFactory.getBullet("Pistol");

            basePistolBullet.UpdateShooting(value);


        }


    }
}
