using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iterator.Iterators;
using windowsForms_client;
using static System.Windows.Forms.LinkLabel;

namespace Iterator.Collections
{
    public class TankCollection : ICollectionA<Tank>
    {
        private readonly List<Tank> tanks;

        public IIterator<Tank> CreateIterator()
        {
            return new ListIterator<Tank>(tanks);
        }

        public TankCollection()
        {
            tanks = new List<Tank>();
        }

        public void Add(Tank tank)
        {
            tanks.Add(tank);
        }

        public int Count => tanks.Count;

        public Tank Get(int index)
        {
            return tanks[index];
        }

        public Tank FindPlayer(string tankId)
        {
            return tanks.FirstOrDefault(t => t.playerId == tankId);
        }

        public void RemovePlayer(string tankId)
        {
            tanks.RemoveAll(t => t.playerId == tankId);
        }
    }
}
