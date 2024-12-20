using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using windowsForms_client.Flyweight;

namespace windowsForms_client
{
    public class Bullet
    {
        [JsonIgnore]
        public Image image { get; set; }
        public string BulletType {get; set; }

        public string Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public int Y_aditional { get; set; }
        public int X_aditional { get; set; }
        public double BulletSpeed { get; set; }
        public int Width { get; set; } = 50;
        public int Height { get; set; } = 50;
        public string Direction { get; set; }

        private double baseX {get; set; }
        private double baseY {get; set; }

        public Bullet() { }


        public Bullet(Image image, string bulletType, double bulletSpeed, string Id, int y_aditionalHeight, int x_aditionalHeight, double x_coordinate, double y_coordinate, string direction)
        {
            this.image = image;
            this.BulletType = bulletType;
            this.Id = Id;
            this.BulletSpeed = bulletSpeed;
            this.Y_aditional = y_aditionalHeight;
            this.X_aditional = x_aditionalHeight;
            this.baseX = x_coordinate;
            this.baseY = y_coordinate;
            this.Direction = direction;

            this.SetBaseBulletPosition();
        }

        public void SetBaseBulletPosition()
        {

            if (Direction == "Right")
            {
                this.X = baseX + 25;
                this.Y = baseY + Y_aditional;
            }
            else if (Direction == "Left")
            {
                this.X = baseX - 25;
                this.Y = baseY + Y_aditional;
            }
            else if (Direction == "Up")
            {
                this.X = baseX + X_aditional;
                this.Y = baseY - 25;
            }
            else if (Direction == "Down")
            {
                this.X = baseX + X_aditional;
                this.Y = baseY + 25;
            }
        }

        public void Rotate(Graphics eGraphics)
        {
            eGraphics.TranslateTransform((float)(this.X + Width / 2), (float)(this.Y + Height / 2));

            if (Direction == "Right")
            {
                eGraphics.RotateTransform(0);
            }
            else if (Direction == "Left")
            {
                eGraphics.RotateTransform(180);
            }
            else if (Direction == "Up")
            {
                eGraphics.RotateTransform(270);
            }
            else if (Direction == "Down")
            {
                eGraphics.RotateTransform(90);
            }

            eGraphics.TranslateTransform((float)-(this.X + this.Width / 2), (float)-(this.Y + this.Height / 2));
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
