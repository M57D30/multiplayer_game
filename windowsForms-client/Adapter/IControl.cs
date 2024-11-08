using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Adapter
{
    public interface IControl
    {
        void MoveUp();
        void MoveDown();
        void MoveLeft();
        void MoveRight();
        void Shoot();
    }

}

