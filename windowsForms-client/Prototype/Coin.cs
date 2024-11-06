using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Prototype
{
    public class Coin : IPrototype<Coin>
    {
        public string Type { get; set; }
        public Position Position { get; set; } //Lacking creativity :D

        public Coin(string type, int x, int y)
        {
            Type = type;
            Position = new Position(x, y);
        }

        public Coin ShallowCopy()
        {
            return (Coin)this.MemberwiseClone();
        }

        public Coin DeepCopy()
        {
            Coin clone = (Coin)this.MemberwiseClone();
            clone.Position = new Position(Position.X, Position.Y);
            return clone;
        }
    }
}
