using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Composite
{
    public abstract class Mine
    {
        public int x_coordinate { get; set; }
        public int y_coordinate { get; set; }
        public bool isVisible { get; set; }
        public Color Color { get; set; }

        public Mine(int x, int y, Color color)
        {
            this.x_coordinate = x;
            this.y_coordinate = y;
            this.Color = color;
        }

        public abstract void SetVisibility(bool isVisible);
        public abstract void Display();
        public abstract bool IsLeaf();
        

    }
}
