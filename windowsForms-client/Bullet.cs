using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client
{
    public class Bullet
    {
        public string Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int BulletSpeed { get; set; }
        public int Width { get; set; } = 7;
        public int Height { get; set; } = 7;
        public string Direction { get; set; }

        public Bullet() { }


        public Bullet(int bulletSpeed, string Id)
        {
            this.Id = Id;
            this.BulletSpeed = bulletSpeed;
        }

        public void SetBaseBulletPosition(int x_coordinate, int y_coordinate)
        {
            if (Direction == "Right")
            {
                this.X = x_coordinate + 50;
                this.Y = y_coordinate + 25;
            }
            else if (Direction == "Left")
            {
                this.X = x_coordinate - 25;
                this.Y = y_coordinate + 25;
            }
            else if (Direction == "Up")
            {
                this.X = x_coordinate + 25;
                this.Y = y_coordinate + 25;
            }
            else if (Direction == "Down")
            {
                this.X = x_coordinate + 25;
                this.Y = y_coordinate + 25;
            }
        }

        public void Move()
        {
            if (Direction == "Right")
            {
                X += BulletSpeed;
            }
            else if (Direction == "Left")
            {
                X -= BulletSpeed;
            }
            else if (Direction == "Up")
            {
                Y -= BulletSpeed;
            }
            else if (Direction == "Down")
            {
                Y += BulletSpeed;
            }
        }

    }
}
