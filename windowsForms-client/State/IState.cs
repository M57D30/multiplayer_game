using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client.State
{
    public interface IState
    {
        public void HandleChange(Tank tank);
    }
    public class Healthy : IState
    {
        public void HandleChange(Tank tank)
        {
            if (tank.Health <= tank.MaxHealth * 0.75)
            {
                tank.SetState(new Damaged());
                Console.WriteLine($"From healthy to damaged");
            }
        }
    }

    public class Damaged : IState
    {
        public void HandleChange(Tank tank)
        {
            if (tank.Health <= tank.MaxHealth * 0.25)
            {
                tank.SetState(new Critical());
                Console.WriteLine($"From damaged to critical");

            }
            else if (tank.Health > tank.MaxHealth * 0.75)
            {
                tank.SetState(new Healthy());
                Console.WriteLine($"From damaged to healthy");

            }
        }
    }
    public class Critical : IState
    {
        public void HandleChange(Tank tank)
        {
            if (tank.Health <= 0)
            {
                tank.SetState(new Dead());
                Console.WriteLine($"From critical to dead");

            }
            else if (tank.Health > tank.MaxHealth * 0.25)
            {
                tank.SetState(new Damaged());
                Console.WriteLine($"From critical to damaged");

            }
        }
    }
    public class Dead : IState
    {
        public void HandleChange(Tank tank)
        {
            tank.SetState(this);
            Console.WriteLine($"{tank.GetType()} is dead");
        }
    }

}
