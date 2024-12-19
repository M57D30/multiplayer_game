using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iterator.Iterators;
using windowsForms_client;
using windowsForms_client.Iterator.Iterators;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Iterator.Collections
{
    public class ObstaclesCollection : ICollectionA<Obstacle>
    {
        private Obstacle[] obstacles;
        private int ArrayLenght = 10;
        private int count;

        public IIterator<Obstacle> CreateIterator()
        {

            return new ArrayIterator<Obstacle>(obstacles, count);
        }

        public ObstaclesCollection()
        {
            obstacles = new Obstacle[ArrayLenght];
            count = 0;
        }

        public void Add(Obstacle obstacle)
        {
            if (count == ArrayLenght)
            {
                // Resize the array when full
                ArrayLenght *= 2;
                Array.Resize(ref obstacles, ArrayLenght);
            }
            obstacles[count++] = obstacle;
            Console.WriteLine("BulletAdded");
        }

    }
}
