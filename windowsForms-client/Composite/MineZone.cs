using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iterator.Collections;
using Iterator.Iterators;

namespace windowsForms_client.Composite
{
    internal class MineZone : Mine, ICollectionA<Mine>
    {
        //private Mine mines2 = new MineZone();
        private List<Mine> mines = new List<Mine>();
        public string ZoneName { get; set; }
        

        public MineZone(string zoneName, int x, int y, Color color) : base(x, y, color)
        {
            ZoneName = zoneName;
        }

        public void Add(Mine mine)
        {
            mines.Add(mine);
        }

        //public override void Remove(Mine mine)
        //{
        //    mines.Remove(mine);
        //    foreach (var currentMine in mines)
        //    {
        //        if (!currentMine.IsLeaf())
        //           currentMine.Remove(mine);
        //    }
        //}

        public void Remove(Mine mine)
        {
            mines.Remove(mine);
            foreach (var currentMine in mines)
            {
                if (currentMine.IsLeaf())  // If the cast was successful
                {
                   (currentMine as MineZone).Remove(mine);  // Recursively call Remove on the MineZone
                }
            }
        }

        public override void SetVisibility(bool isVisible)
        {
            foreach (var mine in mines)
            {
                mine.SetVisibility(isVisible);
            }
        }


        public override void Display()
        {
            Console.WriteLine($"Mine Group: {ZoneName}");
            foreach (var mine in mines)
            {
                mine.Display();
            }
        }

        public override bool IsLeaf()
        {
            return false;
        }

        public IIterator<Mine> CreateIterator()
        {
            return new ListIterator<Mine>(mines);
        }
    }
}
