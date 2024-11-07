using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using windowsForms_client.Prototype;

namespace windowsForms_client
{
    public abstract class Tank
    {
        //Base values
        public string playerId { get; set; }
        public int x_coordinate { get; set; }
        public int y_coordinate { get; set; }
        private string nameOfTheTank { get; set; }
        private int health { get; set; }

        //For movement
        private int playerVelocityX { get ; set; }
        private int playerVelocityY { get; set; }
        private int MovementSpeedX { get; set; }
        private int MovementSpeedY { get; set; }
        protected string TankBodyLookingDirection { get; set; }

        //For shooting
        public List<Bullet> bullets { get; set; }
        protected Bullet bullet { get; set; }
        protected int ShootingInterval { get; set; }
        protected System.Timers.Timer ShootingTimer { get; set; }
        protected string[] TankTurretLookingDirections { get; set; }
        protected string TankTurretLookingDirection { get; set; } = "Right";

        //For Json format
        public virtual string TankType => GetType().Name;

        //Tank collor
        public Color Color { get; protected set; }


        public bool IsFrozen { get; private set; }
        public bool IsBulletFrozen { get; private set; }
        private System.Timers.Timer freezeTimer = new System.Timers.Timer(3000);
        private System.Timers.Timer freezebulletsTimer = new System.Timers.Timer(3000);


        public Tank() 
        {
            freezeTimer.Elapsed += OnFreezeTimerElapsed;
            freezebulletsTimer.Elapsed += OnFreezeBulletTimerElapsed;

        }

        public Tank(string id, int x, int y, string name)
        {
            this.bullets = new List<Bullet>();

            this.playerId = id;
            this.x_coordinate = x;
            this.y_coordinate = y;
            this.nameOfTheTank = name;
            freezeTimer.Elapsed += OnFreezeTimerElapsed; 
            freezebulletsTimer.Elapsed += OnFreezeBulletTimerElapsed;

        }

        public void StartFreeze()
        {
            if (!IsFrozen)
            {
                IsFrozen = true;
                freezeTimer.Start(); // Start the freeze timer
            }
        }
        public void StartBulletFreeze()
        {
            if (!IsBulletFrozen)
            {
                IsBulletFrozen = true;
                freezebulletsTimer.Start();
            }
        }
        private void OnFreezeTimerElapsed(object sender, ElapsedEventArgs e)
        {
            IsFrozen = false;
            freezeTimer.Stop();
        }
        private void OnFreezeBulletTimerElapsed(object sender, ElapsedEventArgs e)
        {
            IsBulletFrozen = false;
            freezebulletsTimer.Stop();
        }



        public void UpdateMovement(int value)
        {

            this.MovementSpeedX += value;
            this.MovementSpeedY += value;
        }

        public void UpdateShooting(int value)
        {
            this.bullet.BulletSpeed += value;
        }
        public int GetTankSpeedX()
        {
            return MovementSpeedX;
        }

        public void UpdateHealth(int value)
        {

        }

        public void UpdateShield(int value)
        {

        }



        public string getNameOfTank()
        {
            return nameOfTheTank;
        }


        public void MoveUp()
        {
            playerVelocityY = -Math.Abs(MovementSpeedY);
            TankBodyLookingDirection = "Up";
        }

        public void MoveDown()
        {
            playerVelocityY = Math.Abs(MovementSpeedY);
            TankBodyLookingDirection = "Down";
        }

        public void MoveLeft()
        {
            playerVelocityX = -Math.Abs(MovementSpeedX);
            TankBodyLookingDirection = "Left";
        }

        public void MoveRight()
        {
            playerVelocityX = Math.Abs(MovementSpeedX);
            TankBodyLookingDirection = "Right"; 
        }

        public void StopMovementY()
        {
            playerVelocityY = 0;
        }

        public void StopMovementX()
        {
            playerVelocityX = 0;
        }

        public void UpdatePosition(int screenWidth, int screenHeight)
        {
            x_coordinate = Math.Max(0, Math.Min(x_coordinate + playerVelocityX, screenWidth - 50));
            y_coordinate = Math.Max(0, Math.Min(y_coordinate + playerVelocityY, screenHeight - 50));
        }



        public void setMovementSpeed(int speedX, int speedY)
        {
            this.MovementSpeedX = speedX;
            this.MovementSpeedY = speedY;
        }

        public abstract void setTurretLookingDirections(string[] directions);
        public abstract void setBullets(int bulletSpeed);
        public abstract void setShootingMechanism(int bulletSpeed);
        public abstract void ShootInADirection();
        public abstract void Shoot();
        public abstract void StartShooting();
        public abstract void StopShooting();
    }
}
