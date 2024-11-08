using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Tanks
{
    internal abstract class ShotgunTank : Tank
    {
        List<Bullet> shotgunBullets = new List<Bullet>();

        public ShotgunTank() 
        { 
        
        }

        public ShotgunTank(string id, int x, int y, string name) : base(id, x, y, name)
        { 
        
        }

        public override void setShootingMechanism(int bulletSpeed)
        {
        }

        public override void setBullets(List<Bullet> bullets)
        {
            this.shotgunBullets = bullets;
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

            foreach (var b in this.shotgunBullets)
            {
                b.Direction = TankTurretLookingDirection;
                b.SetBaseBulletPosition(x_coordinate, y_coordinate);
                bullets.Add(b);
            }

        }

    }
}
