using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Prototype
{
    public class CoinDetails
    {
        public int Value { get; set; }
        public string ImagePath { get; set; }
        public Image image;

        public CoinDetails(int value, string imagePath, Image image)
        {
            Value = value;
            ImagePath = imagePath;
            this.image = image;
        }
    }
}
