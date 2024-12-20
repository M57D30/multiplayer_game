using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Composite
{
    public class MinePlacement
    {
        private Random random = new Random();
        private int width, height;

        // Constructor to initialize form width and height
        public MinePlacement(int formWidth, int formHeight)
        {
            width = formWidth;
            height = formHeight;
        }

        // Method to generate random coordinates for a given zone
        public Point GetRandomCoordinatesForZone(string zone)
        {
            int x = 0, y = 0;

            switch (zone)
            {
                case "ZoneA": // Upper-left
                    x = random.Next(0, width / 2);  // Random x between 0 and half width
                    y = random.Next(0, height / 2); // Random y between 0 and half height
                    break;

                case "ZoneB": // Upper-right
                    x = random.Next(width / 2, width);   // Random x between half width and full width
                    y = random.Next(0, height / 2);      // Random y between 0 and half height
                    break;

                case "ZoneC": // Lower-left
                    x = random.Next(0, width / 2);      // Random x between 0 and half width
                    y = random.Next(height / 2, height); // Random y between half height and full height
                    break;

                case "ZoneD": // Lower-right
                    x = random.Next(width / 2, width);   // Random x between half width and full width
                    y = random.Next(height / 2, height); // Random y between half height and full height
                    break;
            }

            return new Point(x, y);
        }
    }
}
