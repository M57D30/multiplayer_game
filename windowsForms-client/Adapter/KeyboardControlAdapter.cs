using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Adapter
{
    public class KeyboardControlAdapter : IControl
    {
        private Tank _tank;

        public KeyboardControlAdapter(Tank tank)
        {
            _tank = tank;
        }

        public void MoveUp() => _tank.MoveUp();
        public void MoveDown() => _tank.MoveDown();
        public void MoveLeft() => _tank.MoveLeft();
        public void MoveRight() => _tank.MoveRight();
        public void Shoot() => _tank.StartShooting();

    }
}
