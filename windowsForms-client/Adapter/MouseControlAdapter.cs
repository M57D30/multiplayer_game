using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using windowsForms_client.Adapter;
using windowsForms_client;

namespace indowsForms_client.Adapter
{
    public class MouseControlAdapter : IControl
    {
        private Tank _tank;

        public MouseControlAdapter(Tank tank)
        {
            _tank = tank;
        }

        public void MoveUp()
        {
            // Logic to move up if the mouse is above the tank
            if (_tank.y_coordinate > 0)
                _tank.y_coordinate -= 1;  // Adjust speed if needed
        }

        public void MoveDown()
        {
            // Logic to move down if the mouse is below the tank
            if (_tank.y_coordinate < 600) // Max Y boundary (adjust accordingly)
                _tank.y_coordinate += 1;
        }

        public void MoveLeft()
        {
            // Logic to move left if the mouse is to the left of the tank
            if (_tank.x_coordinate > 0)
                _tank.x_coordinate -= 1;
        }

        public void MoveRight()
        {
            // Logic to move right if the mouse is to the right of the tank
            if (_tank.x_coordinate < 800) // Max X boundary (adjust accordingly)
                _tank.x_coordinate += 1;
        }

        public void Shoot()
        {
            // Logic to shoot when the mouse is clicked
            _tank.Shoot();  // Assuming the tank class has a Shoot method
        }
        public void UpdateTankPosition(Point mousePosition)
        {
            // Update tank's position based on mouse position
            _tank.x_coordinate = mousePosition.X;
            _tank.y_coordinate = mousePosition.Y;
        }

        public void StopMovementY() => _tank.StopMovementY();

        public void StopMovementX() => _tank.StopMovementX();

        public void StopShooting() => _tank.StopShooting();

    }

}
