using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.BuilderPattern
{
    public abstract class Builder
    {
        protected Tank tank { get; set; }
        protected bool isBodyAssembled = false;
        protected bool isTurretAdded = false;

        public Tank GetBuildable()
        {
            if (!isBodyAssembled || !isTurretAdded)
            {
                throw new InvalidOperationException("Tank lacks body and turret.");
            }
            return this.tank;
        }

        public Builder StartNew(Tank newTank)
        {
            this.tank = newTank;
            return this;
        }


        public abstract Builder AssembleBody();

        public abstract Builder AddWeapons();

        public abstract Builder AddTurret();

        public abstract Builder AddDecoration();
    }
}
