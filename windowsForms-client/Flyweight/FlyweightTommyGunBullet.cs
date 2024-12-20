using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Flyweight
{
    public class FlyweightTommyGunBullet : IFlyweightBullet
    {
        private readonly Image image;
        private double upgradedBulletSpeed;

        private readonly string BulletType;

        public FlyweightTommyGunBullet(string imagePath, string bulletType, double baseBulletSpeed)
        {
            this.image = Image.FromFile(imagePath);

            this.BulletType = bulletType;

            this.upgradedBulletSpeed = baseBulletSpeed;
        }

        public Bullet Create(string Id, double positionX, double positionY, string direction, int x_aditionalHeight, int y_aditionalHeight)
        {
            return new Bullet(image, BulletType, upgradedBulletSpeed, Id, y_aditionalHeight, x_aditionalHeight, positionX, positionY, direction);
        }

        public void UpdateShooting(double bulletSpeed)
        {
            this.upgradedBulletSpeed += bulletSpeed;
        }
    }
}
