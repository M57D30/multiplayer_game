using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Composite
{
    public class TrapMine : Mine
    {
        private string Name { get; set; }

        public TrapMine(int x, int y, Color color) : base(x, y, color)
        {
            Name = "Trap mine";
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
