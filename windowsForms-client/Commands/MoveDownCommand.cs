using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Commands
{
    public class MoveDownCommand : ICommand
    {
        private Tank _tank;

        public MoveDownCommand(Tank tank)
        {
            _tank = tank;
        }

        public void Execute()
        {
            _tank.MoveDown();
        }
    }
}
