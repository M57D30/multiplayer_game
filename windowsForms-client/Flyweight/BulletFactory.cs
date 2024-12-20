using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Flyweight
{
    public class BulletFactory
    {

        private static Dictionary<string, IFlyweightBullet> bulletsHash = new Dictionary<string, IFlyweightBullet>();


        public static IFlyweightBullet getBullet(string tankTypeBullet)
        {
            

            if (!bulletsHash.ContainsKey(tankTypeBullet))
            {
                Console.WriteLine($"Creating a new Bullet for tank type: {tankTypeBullet}");

                if (tankTypeBullet.Equals("Pistol"))
                {
                    bulletsHash[tankTypeBullet] = new FlyweightPistolBullet(@"Images\PistolBullet.jpg", tankTypeBullet, 20);
                }
                else if (tankTypeBullet.Equals("TommyGun"))
                {
                     bulletsHash[tankTypeBullet] = new FlyweightTommyGunBullet(@"Images\TommyGunBullet.png", tankTypeBullet, 10);
                }
                else if (tankTypeBullet.Equals("Shotgun"))
                {
                    bulletsHash[tankTypeBullet] = new FlyweightShotgunBullet(@"Images\TommyGunBullet.png", tankTypeBullet, 10);
                }

            }

            return bulletsHash[tankTypeBullet];
        }

    }
}
