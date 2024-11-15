using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.Decorators
{
    public interface ITankComponent
    {
        int GetHealth();
        int GetXSpeed();
        int GetYSpeed();
         
        ITankComponent ApplyUpgrade(string upgradeType);
    }
}
