using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Commands
{
    public class ShootCommand : ICommand
    {
        private Tank _tank;

        public ShootCommand(Tank tank)
        {
            _tank = tank;
        }

        public void Execute()
        {
            _tank.StartShooting();
        }

        public void Undo()
        {
            _tank.StopShooting();
        }
    }
}
