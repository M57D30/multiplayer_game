using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Composite
{
    public class ExplosiveMine : Mine
    {
        private string Name { get; set; }

        public ExplosiveMine(int x, int y, Color color) : base(x, y, color)
        {
            Name = "Explosive mine";
            isVisible = false; // Default visibility
        }

        public override void SetVisibility(bool isVisible)
        {
            this.isVisible = isVisible;
        }


        public override void Display()
        {
            Console.WriteLine($"Mine: {Name}, Visibility: {isVisible}");
        }

        public override bool IsLeaf()
        {
            return true;
        }

       
    }
}
