using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using windowsForms_client.Decorators;

namespace windowsForms_client.Prototype
{
    public class Coin : IPrototype<Coin>
    {
        private const string pathB = @"c:\pic\";
        public string Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public CoinDetails Details { get; set; }

        public Coin(string type, int x, int y, CoinDetails details)
        {
            Type = type;
            X = x; 
            Y = y;
            Details = details;
        }

        public Coin ShallowCopy()
        {
            Coin clone = (Coin)this.MemberwiseClone();
            clone.X = new Random().Next(0, 800);
            clone.Y = new Random().Next(0, 300);
            return clone;
        }

        public Coin DeepCopy()
        {
            Coin clone = (Coin)this.MemberwiseClone();
            clone.X = new Random().Next(0, 800);
            clone.Y = new Random().Next(0, 300);
            string newType = GetNextCoinType(); 
            clone.Type = newType;
            string imagePath = GetImagePathForCoin(newType);
            clone.Details = new CoinDetails(Details.Value + 1, imagePath, Image.FromFile(imagePath));

            return clone;
        }

        public ITankComponent Collect(ITankComponent tank)
        {
            Console.WriteLine($"Collecting {Type} coin. Applying upgrade...");
            return tank.ApplyUpgrade(Type);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        private string GetNextCoinType()
        {

            if (Type == "Gold") return "Diamond";
            else if (Type == "Diamond") return "Emerald";
            else return Type; 
        }
        private string GetImagePathForCoin(string coinType)
        {
            if (coinType == "Gold") return pathB +  "gold.jpg";
            else if (coinType == "Diamond") return pathB +  "diamond.jpg";
            else if (coinType == "Emerald") return pathB +  "emerald.jpg";
            else return pathB + "gold.jpg";
        }
    }
}
