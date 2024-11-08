using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Commands
{
    public class MoveUpCommand : ICommand
    {
        private Tank _tank;

        public MoveUpCommand(Tank tank)
        {
            if (tank == null)
            {
                throw new ArgumentNullException(nameof(tank), "Tank instance cannot be null");
            }
            _tank = tank;
        }

        public void Execute()
        {
            _tank.MoveUp();
        }
    }
}
