using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using windowsForms_client.AbstractFactoryPatternn;
using windowsForms_client.AbstractFactoryPatternn.Factorys;
using windowsForms_client.Tanks;
using System.Text.Json;
using System.Drawing;
using System.Runtime.CompilerServices;
using windowsForms_client.Strategy;
using windowsForms_client.Factory;
using windowsForms_client.Prototype;

namespace windowsForms_client

{
    public partial class GameClient : Form
    {
        private Tank CurrentTank;
        private List<Tank> allPlayers = new List<Tank>();
        private System.Timers.Timer gameLoopTimer;
        private bool spacebarPressed = false;
        private (int x, int y) lastSentPosition;
        private WebSocketComunication webSocketComunication;

        private System.Timers.Timer gameTimer;
        private int elapsedSeconds = 0;

        private List<Obstacle> obstacles = new List<Obstacle>();

        private Coin coin;



        public GameClient(string tankType, string selectedUpgrade)
        {
            InitializeComponent();
            PrintTankType(tankType);

            gameTimer = new System.Timers.Timer(1000);
            gameTimer.Elapsed += OnGameTimerElapsed;
            DisplayTime();

            InitializeObstacles(); // Call to initialize obstacles

            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;
            webSocketComunication = new WebSocketComunication(tankType, selectedUpgrade, this);
            this.FormClosing += GameClient_FormClosing; 
        }

        // Initialize obstacles AND coind
        public void InitializeObstacles()
        {
            ObstacleCreator mistCreator = new MistCreator();
            ObstacleCreator mudCreator = new MudCreator();
            ObstacleCreator iceCreator = new IceCreator();
            ObstacleCreator snowCreator = new SnowCreator();

            obstacles.Add(mistCreator.CreateObstacle(100, 100, new BlindnessStrategy()));
            obstacles.Add(mistCreator.CreateObstacle(600, 100, new BlindnessStrategy()));
            obstacles.Add(mudCreator.CreateObstacle(200, 250, new SlowStrategy()));    
            obstacles.Add(iceCreator.CreateObstacle(600, 350, new FastStrategy()));    
            obstacles.Add(snowCreator.CreateObstacle(350, 150, new StuckStrategy()));

            coin = new Coin("Gold", new Random().Next(0, 800), new Random().Next(0, 300));

            Invalidate();
        }

        public void StartCountingTime()
        {
            gameTimer.Start();
        }

        public void StopCountingTime()
        {
            gameTimer.Stop();
            elapsedSeconds = 0;
            DisplayTime();
        }

        private void OnGameTimerElapsed(object sender, ElapsedEventArgs e)
        {
            elapsedSeconds++;
            DisplayTime();

        }

        public void DisplayTime()
        {
            TimeLabel.Text = $"Time: {elapsedSeconds / 60:D2}:{elapsedSeconds % 60:D2}";
        }

        public void DisplayGameState(string state)
        {
            if (state == "Start")
            {
                gameState.Text = "Game Started";
            }
            else if (state == "Stop")
            {
                gameState.Text = "Game Stoped";
            }
        }

        private async void GameClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            TimeLabel.Dispose();
            await webSocketComunication.SendAsyncClosing();
        }

        public void InitializeTank(string tankType, string tankColor, string playerId)
        {

            if (tankColor == "Red")
            {
                AbstractFactory RF = new RedFactory();
                if (tankType == "Pistol")
                {
                    CurrentTank = RF.createPistolTank(playerId, 600, 200);
                    
                }
                if (tankType == "TommyGun")
                {
                    CurrentTank = RF.createTommyGunTank(playerId, 600, 200);
                }

            } 
            else if (tankColor == "Blue")
            {
                AbstractFactory BF = new BlueFactory();
                if (tankType == "Pistol")
                {
                    CurrentTank = BF.createPistolTank(playerId, 100, 200);
                }
                if (tankType == "TommyGun")
                {
                    CurrentTank = BF.createTommyGunTank(playerId, 100, 200);
                }
            }
            
            Console.WriteLine(CurrentTank.getNameOfTank());
            allPlayers.Add(CurrentTank);
            BeginGameLoop();
        }


        private void BeginGameLoop()
        {
            // Start the game loop (60 FPS -> 16ms interval)
            gameLoopTimer = new System.Timers.Timer(16);
            gameLoopTimer.Elapsed += OnGameLoop;
            gameLoopTimer.AutoReset = true;
            gameLoopTimer.Start();
        }

        
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!CurrentTank.IsFrozen)
            {
                if (e.KeyCode == Keys.Up) CurrentTank.MoveUp();
                if (e.KeyCode == Keys.Down) CurrentTank.MoveDown();
                if (e.KeyCode == Keys.Left) CurrentTank.MoveLeft();
                if (e.KeyCode == Keys.Right) CurrentTank.MoveRight();
            }

            // Shoot when spacebar is pressed
            if (e.KeyCode == Keys.Space && !spacebarPressed && !CurrentTank.IsBulletFrozen)
            {
                spacebarPressed = true;
                CurrentTank.StartShooting();
            }
        }


        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down) CurrentTank.StopMovementY();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right) CurrentTank.StopMovementX();

            if (e.KeyCode == Keys.Space)
            {
                CurrentTank.StopShooting(); // Stop shooting when space is released
                spacebarPressed = false;
            }
        }
       

        private async void OnGameLoop(object sender, ElapsedEventArgs e)
        {
            await this.Move();

            await this.Shoot();

            if (IsCollidingWithCoin(CurrentTank, coin))
            {
                Coin clonedCoin = (Coin)coin.DeepCopy();
                //Coin clonedCoin = (Coin)coin.ShallowCopy();
                clonedCoin.Position.X = new Random().Next(0, 800);
                clonedCoin.Position.Y = new Random().Next(0, 300);

                coin = clonedCoin;
            }
        }

        private async Task Move()
        {
            CurrentTank.UpdatePosition(ClientSize.Width, ClientSize.Height);

            foreach (var obstacle in obstacles)
            {
                if (IsCollidingWithObstacle(CurrentTank, obstacle))
                {
                    if (!obstacle.HasBeenAffected)
                    {
                        obstacle.Strategy.ApplyStrategy(CurrentTank);
                        obstacle.HasBeenAffected = true;
                    }
                }
                else
                {
                    if (obstacle.HasBeenAffected)
                    {
                        obstacle.HasBeenAffected = false; 
                    }
                }
            }

            if (CurrentTank.x_coordinate != lastSentPosition.x || CurrentTank.y_coordinate != lastSentPosition.y)
            {
                lastSentPosition = (CurrentTank.x_coordinate, CurrentTank.y_coordinate);
                await webSocketComunication.SendTankInformation(CurrentTank);
            }
        }


        private bool IsCollidingWithObstacle(Tank tank, Obstacle obstacle)
        {
            Rectangle tankRect = new Rectangle(tank.x_coordinate, tank.y_coordinate, 50, 50);
            Rectangle obstacleRect = new Rectangle(obstacle.x_coordinate, obstacle.y_coordinate, 50, 50);
            return tankRect.IntersectsWith(obstacleRect);
        }
        private bool IsCollidingWithCoin(Tank tank, Coin coin)
        {
            Rectangle tankRect = new Rectangle(tank.x_coordinate, tank.y_coordinate, 50, 50);
            Rectangle coinRect = new Rectangle(coin.Position.X, coin.Position.Y, 10, 10);
            return tankRect.IntersectsWith(coinRect);
        }


        private async Task Shoot()
        {
            bool isBullet = false;
            // Shooting

            for (int i = CurrentTank.bullets.Count - 1; i >= 0; i--)
            {

                isBullet = true;
                var bullet = CurrentTank.bullets[i];

                bullet.Move();
                // Remove bullet if it's out of bounds
                if (bullet.X > ClientSize.Width || bullet.X < 0 || bullet.Y > ClientSize.Height || bullet.Y < 0)
                {
                    CurrentTank.bullets.RemoveAt(i);
                }
                
            }
            if (isBullet)
            {
                await webSocketComunication.SendTankInformation(CurrentTank);
            }

        }


        public void UpdatePlayerPosition(Tank receivedTank)
        {
            // Find existing player tank
            var existingPlayer = allPlayers.FirstOrDefault(t => t.playerId == receivedTank.playerId);

            if (existingPlayer != null)
            {
                // Update properties of the existing player tank
                existingPlayer.x_coordinate = receivedTank.x_coordinate; // Assuming Tank has a Position property
                existingPlayer.y_coordinate = receivedTank.y_coordinate; // If your Tank class has a Rotation property

                // Optionally, update bullets if necessary
                existingPlayer.bullets.Clear(); // Clear existing bullets
                existingPlayer.bullets.AddRange(receivedTank.bullets);
                
            }
            else
            {
                // Add new player tank
                allPlayers.Add(receivedTank);
            }

            Invalidate(); // Refresh the UI
        }


        public void RemovePlayer(string receivedTankId)
        {
            allPlayers.RemoveAll(t => t.playerId == receivedTankId);

            Invalidate(); // Refresh the UI
        }


        public void UpgradePlayer(int upgradeValue, string upgradeType)
        {
            if (upgradeType == "Movement")
            {
                CurrentTank.UpdateMovement(upgradeValue);
            }
            else if (upgradeType == "Shooting")
            {
                CurrentTank.UpdateShooting(upgradeValue);
            }
            else if (upgradeType == "Health")
            {
                CurrentTank.UpdateHealth(upgradeValue);
            }
            else if (upgradeType == "Shield")
            {
                CurrentTank.UpdateShield(upgradeValue);
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Adding obstacles to the game
            foreach (var obstacle in obstacles)
            {
                if (obstacle is Mist)
                {
                    e.Graphics.FillRectangle(Brushes.LightGray, obstacle.x_coordinate, obstacle.y_coordinate, 50, 50);
                }
                else if (obstacle is Mud)
                {
                    e.Graphics.FillRectangle(Brushes.SaddleBrown, obstacle.x_coordinate, obstacle.y_coordinate, 50, 50);
                }
                else if (obstacle is Ice)
                {
                    e.Graphics.FillRectangle(Brushes.LightSteelBlue, obstacle.x_coordinate, obstacle.y_coordinate, 50, 50);
                }
                else if (obstacle is Snow)
                {
                    e.Graphics.FillRectangle(Brushes.LightBlue, obstacle.x_coordinate, obstacle.y_coordinate, 50, 50);
                }
            }

            //Adding magic coin that does nothing
            e.Graphics.FillRectangle(Brushes.Gold, coin.Position.X, coin.Position.Y, 10, 10);

            foreach (var player in allPlayers.ToList())
            {
                e.Graphics.FillRectangle(new SolidBrush(player.Color), player.x_coordinate, player.y_coordinate, 50, 50);

                foreach (var bullet in player.bullets.ToList())
                {
                    e.Graphics.FillRectangle(new SolidBrush(player.Color), bullet.X, bullet.Y, bullet.Width, bullet.Height);
                }
            }
        }


        private void PrintTankType(string tankType)
        {
           
            if (tankType == "Pistol")
            {
               
                Console.WriteLine("Pistol tank created.");
            }
            else if (tankType == "TommyGun")
            {
               
                Console.WriteLine("TommyGun tank created.");
            }
        }

    }
}
