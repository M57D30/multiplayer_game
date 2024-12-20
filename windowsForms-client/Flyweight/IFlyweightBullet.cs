using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Flyweight
{
    public interface IFlyweightBullet
    {
        Bullet Create(string Id, double positionX, double positionY, string direction, int x_aditionalHeight, int y_aditionalHeight);

        void UpdateShooting(double bulletSpeed);
    }
}
