using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Commands
{
    public class MoveRightCommand : ICommand
    {
        private Tank _tank;

        public MoveRightCommand(Tank tank)
        {
            _tank = tank;
        }

        public void Execute()
        {
            _tank.MoveRight();
        }
    }
}
