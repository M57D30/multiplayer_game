﻿using System;
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
using windowsForms_client.Adapter;
using indowsForms_client.Adapter;
using System.Security.AccessControl;

using static System.Windows.Forms.AxHost;
using System.Diagnostics;
using windowsForms_client.Flyweight;
using static System.Windows.Forms.LinkLabel;
using System.Numerics;
using Iterator.Collections;
using Iterator.Iterators;
using windowsForms_client.Iterator.Collections;

using windowsForms_client.State;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using windowsForms_client.Interpreter;
using System.Runtime.InteropServices;
using System.Threading;


namespace windowsForms_client

{
    public partial class GameClientFacade : Form
    {
        private bool isMousePressed = false; 
        private Point mousePosition;
        private string controlType;
        
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();


        private IControl _controlAdapter;
        private Tank CurrentTank;
        //private List<Tank> allPlayers = new List<Tank>();
        private TankCollection tankCollection = new TankCollection();
        private System.Timers.Timer gameLoopTimer;
        private bool spacebarPressed = false;
        private (int x, int y) lastSentPosition;
        private WebSocketComunication webSocketComunication;

        private System.Timers.Timer gameTimer;
        private System.Timers.Timer coinTimer;
        private int elapsedSeconds = 0;


        private System.Timers.Timer temporaryEffectTimer;
       // private List<Obstacle> obstacles = new List<Obstacle>();

        private ObstaclesCollection obstaclesCollection = new ObstaclesCollection();
        private List<IStrategy> randomStrategies = new List<IStrategy>
        {
            new FastStrategy(),
            new SlowStrategy(),
            new StuckStrategy(),
            new BlindnessStrategy()
        };
       // private Dictionary<Obstacle, (Color OriginalColor, IStrategy OriginalStrategy)> originalObstacleStates = new Dictionary<Obstacle, (Color, IStrategy)>();
        private OriginalObstacleStatesCollection originalObstaclesStates = new OriginalObstacleStatesCollection();

        private Coin coin;
        private int gamePhase = 1;

        private CustomProgressBar healthBar;
        private CommandParser _commandParser;


        public  GameClientFacade(string tankType, string selectedUpgrade, string controlType)
        {
            InitializeComponent();
            PrintTankType(tankType);

            this.controlType = controlType;

            gameTimer = new System.Timers.Timer(1000);
            gameTimer.Elapsed += OnGameTimerElapsed;
            DisplayTime();

            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;

            MouseDown += OnMouseDownHandler;
            MouseUp += OnMouseUpHandler;

            webSocketComunication = WebSocketComunication.Instance(tankType, selectedUpgrade, this);
            this.FormClosing += GameClient_FormClosing;
            InitializeObstacles(); // Call to initialize obstacles


            temporaryEffectTimer = new System.Timers.Timer(10000);
            temporaryEffectTimer.Elapsed += OnTemporaryEffectTimerElapsed;
            temporaryEffectTimer.Start();

            coinTimer = new System.Timers.Timer(10000);

            coinTimer = new System.Timers.Timer(2000);

            coinTimer.Elapsed += OnCoinTimerElapsed;
            coinTimer.Start();


            IterateOverDictionary();

            //Interpreter
            AllocConsole();
            _commandParser = new CommandParser();
            StartConsoleInput();


        }

        // Initialize obstacles AND coind
        public void InitializeObstacles()
        {
            ObstacleCreator mistCreator = new MistCreator();
            ObstacleCreator mudCreator = new MudCreator();
            ObstacleCreator iceCreator = new IceCreator();
            ObstacleCreator snowCreator = new SnowCreator();


            obstaclesCollection.Add(mistCreator.CreateObstacle(100, 100, new BlindnessStrategy()));
            obstaclesCollection.Add(mistCreator.CreateObstacle(600, 100, new BlindnessStrategy()));
            obstaclesCollection.Add(mudCreator.CreateObstacle(200, 250, new SlowStrategy()));
            obstaclesCollection.Add(iceCreator.CreateObstacle(600, 350, new FastStrategy()));
            obstaclesCollection.Add(snowCreator.CreateObstacle(350, 150, new StuckStrategy()));



            IIterator<Obstacle> obstacles = obstaclesCollection.CreateIterator();

            while (obstacles.HasNext())
            {

                Obstacle obstacle = obstacles.Next();
                //originalObstacleStates[obstacle] = (GetObstacleColor(obstacle), obstacle.Strategy);
                originalObstaclesStates.Add(obstacle, GetObstacleColor(obstacle), obstacle.Strategy);
            }

            //foreach (var obstacle in obstacles)
            //{
            //    originalObstacleStates[obstacle] = (GetObstacleColor(obstacle), obstacle.Strategy);
            //}
            //PLEASE CHECK IF THIS CAUSES ERRORS TO U

            string imagePath = @"Images\gold.jpg";

                obstacle.SetOriginalColor(obstacle.GetDefaultColor());
                obstacle.SetTempColor(obstacle.GetDefaultColor());
                originalObstacleStates[obstacle] = (obstacle.GetDefaultColor(), obstacle.Strategy);
            }
            string imagePath = @"c:\pic\gold.jpg";


            Console.WriteLine(imagePath);


            //COMMENT THIS
            coin = new Coin("Gold", new Random().Next(0, 800), new Random().Next(0, 300), new CoinDetails(1, imagePath, Image.FromFile(imagePath)));

            Invalidate();
        }


        public void IterateOverDictionary()
        {
            IIterator<KeyValuePair<Obstacle, (Color OriginalColor, IStrategy OriginalStrategy)>> originalOb = originalObstaclesStates.CreateIterator();

            while (originalOb.HasNext())
            {
                KeyValuePair<Obstacle, (Color OriginalColor, IStrategy OriginalStrategy)> obstacle = originalOb.Next();

                Console.WriteLine($@"Original color: {obstacle.Value.OriginalColor}, Original Strategy: {obstacle.Value.OriginalStrategy}");
            }

        public void ChangeTankColor(Color color)
        {
           CurrentTank.SetColor(color);
        }
        public int GetHealth()
        {
            return CurrentTank.Health;
        }

        public void  UpdateSpecTank(CustomProgressBar healthBar, Tank tank)
        {
            healthBar.Value = Math.Max(0, Math.Min(tank.Health, 20));

            if (tank.CurrentState.GetType() == typeof(Healthy))
                healthBar.BarColor = Color.Green;
            else if (tank.CurrentState.GetType() == typeof(Damaged))
                healthBar.BarColor = Color.Orange;
            else if (tank.CurrentState.GetType() == typeof(Critical))
                healthBar.BarColor = Color.Red;
            else
            {
                healthBar.BarColor = Color.Black;
            }
            healthBar.Invalidate();
        }

        private async Task StartConsoleInput()
        {
            var consoleThread = new Thread(async () =>
            {
                while (true)
                {
                    string input = Console.ReadLine();
                    if (!string.IsNullOrEmpty(input))
                    {
                        _commandParser.ParseAndExecute(input, SynchronizationContext.Current, this);
                        await webSocketComunication.SendTankInformation(CurrentTank);
                    }
                }
            });

            consoleThread.IsBackground = true;  // Make the thread a background thread
            consoleThread.Start();

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
        private void OnCoinTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (gamePhase >= 3)
            {
                (sender as System.Timers.Timer).Stop();
                return;
            }
            Coin clonedCoin = coin.DeepCopy();
            coin = clonedCoin;
            gamePhase++;
            Invalidate();
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
                if (tankType == "Shotgun")
                {
                    CurrentTank = RF.createShotgunTank(playerId, 600, 200);
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
                if (tankType == "Shotgun")
                {
                    CurrentTank = BF.createShotgunTank(playerId, 100, 200);
                }
            }
            
            Console.WriteLine(CurrentTank.getNameOfTank());
            tankCollection.Add(CurrentTank);
            if (controlType == "Mouse")
            {
                _controlAdapter = new MouseControlAdapter(CurrentTank);
            }
            else
            {
                _controlAdapter = new KeyboardControlAdapter(CurrentTank);
            }
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

            if (!CurrentTank.IsFrozen && this.controlType == "Keyboard")
            {
                if (e.KeyCode == Keys.Up) _controlAdapter.MoveUp();
                if (e.KeyCode == Keys.Down) _controlAdapter.MoveDown();
                if (e.KeyCode == Keys.Left) _controlAdapter.MoveLeft();
                if (e.KeyCode == Keys.Right) _controlAdapter.MoveRight();
            }

            // Shoot when spacebar is pressed
            if (e.KeyCode == Keys.Space && !spacebarPressed && !CurrentTank.IsBulletFrozen)
            {
                spacebarPressed = true;
                _controlAdapter.Shoot();
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (CurrentTank == null || _controlAdapter == null)
            {
                return; // Do nothing if CurrentTank or _controlAdapter is null
            }
            if (this.controlType == "Keyboard")
            {
                if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down) _controlAdapter.StopMovementY();
                if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right) _controlAdapter.StopMovementX();

                if (e.KeyCode == Keys.Space)
                {
                    _controlAdapter.StopShooting(); // Stop shooting when space is released
                    spacebarPressed = false;
                }
            }
              
        }

        private async void OnGameLoop(object sender, ElapsedEventArgs e)
        {
            if (isMousePressed)
            {
                int dx = mousePosition.X - CurrentTank.x_coordinate;
                int dy = mousePosition.Y - CurrentTank.y_coordinate;

                if (Math.Abs(dx) > Math.Abs(dy))
                {
                    if (dx > 0) _controlAdapter.MoveRight();
                    else _controlAdapter.MoveLeft();
                }
                else
                {
                    if (dy > 0) _controlAdapter.MoveDown();
                    else _controlAdapter.MoveUp();
                }
            }

            await this.Move();

            await this.Shoot();
            

            if (IsCollidingWithCoin(CurrentTank, coin))
            {
                Coin clonedCoin = coin.ShallowCopy();
                coin = clonedCoin;
            }
        }

        private void OnMouseDownHandler(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left && controlType == "Mouse")
            {
                isMousePressed = true;
                mousePosition = e.Location;
                _controlAdapter.Shoot();  // Shoot when mouse button is pressed
            }
        }

        private void OnMouseUpHandler(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMousePressed = false;
            }
        }
        //protected override void OnMouseMove(MouseEventArgs e)
        //{
        //    base.OnMouseMove(e);

        //    // Update the current mouse position
        //    mousePosition = e.Location;

        //    // Calculate the angle from the tank's position to the mouse position
        //    if (CurrentTank != null)
        //    {
        //        float dx = mousePosition.X - CurrentTank.x_coordinate;
        //        float dy = mousePosition.Y - CurrentTank.y_coordinate;
        //        float angle = (float)(Math.Atan2(dy, dx) * (180 / Math.PI)); // Angle in degrees

        //        // If your tank has a turret rotation property, set it here
        //        CurrentTank.TurretAngle = angle;
        //    }

        //    Invalidate(); // Refresh to show turret rotation
        //}

        private async Task Move()
        {
            CurrentTank.UpdatePosition(ClientSize.Width, ClientSize.Height);

            IIterator<Obstacle> obstacles = obstaclesCollection.CreateIterator();

            while (obstacles.HasNext())
            {

                Obstacle obstacle = obstacles.Next();
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

                obstacle.CheckAndApplyEffect(CurrentTank);

            }


            //foreach (var obstacle in obstacles)
            //{
            //    if (IsCollidingWithObstacle(CurrentTank, obstacle))
            //    {
            //        if (!obstacle.HasBeenAffected)
            //        {
            //            obstacle.Strategy.ApplyStrategy(CurrentTank);
            //            obstacle.HasBeenAffected = true;
            //        }
            //    }
            //    else
            //    {
            //        if (obstacle.HasBeenAffected)
            //        {
            //            obstacle.HasBeenAffected = false; 
            //        }
            //    }
            //}

            if (CurrentTank.x_coordinate != lastSentPosition.x || CurrentTank.y_coordinate != lastSentPosition.y)
            {
                lastSentPosition = (CurrentTank.x_coordinate, CurrentTank.y_coordinate);
                await webSocketComunication.SendTankInformation(CurrentTank);
            }
        }

        private bool IsCollidingWithCoin(Tank tank, Coin coin)
        {
            Rectangle tankRect = new Rectangle(tank.x_coordinate, tank.y_coordinate, 50, 50);
            Rectangle coinRect = new Rectangle(coin.X, coin.Y, 10, 10);
            return tankRect.IntersectsWith(coinRect);
        }


        private async Task Shoot()
        {
            bool isBullet = false;

            for (int i = CurrentTank.bullets.Count - 1; i >= 0; i--)
            {
                isBullet = true;
                var bullet = CurrentTank.bullets[i];


                bullet.Move();  
                foreach (var tank in allPlayers)
                {
                    if (tank != CurrentTank)
                    {
                        Rectangle bulletRect = new Rectangle(bullet.X, bullet.Y, 7, 7);
                        Rectangle tankRect = new Rectangle(tank.x_coordinate, tank.y_coordinate, 50, 50);

                        if (bulletRect.IntersectsWith(tankRect))
                        {
                            tank.TakeDamage(1);
                            UpdateSpecTank(healthBar, tank);
                            CurrentTank.bullets.RemoveAt(i);
                            await webSocketComunication.SendTankInformation(tank);
                            break;
                        }
                    }
                }

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
            var existingPlayer = tankCollection.FindPlayer(receivedTank.playerId);
           // var existingPlayer = allPlayers.FirstOrDefault(t => t.playerId == receivedTank.playerId);
            if (existingPlayer != null)
            {
                // Update properties of the existing player tank
                existingPlayer.x_coordinate = receivedTank.x_coordinate; // Assuming Tank has a Position property
                existingPlayer.y_coordinate = receivedTank.y_coordinate; // If your Tank class has a Rotation property

                existingPlayer.bullets.Clear();
                existingPlayer.bullets.AddRange(receivedTank.bullets);


            }
            else
            {
                // Add new player tank
                //allPlayers.Add(receivedTank);
                tankCollection.Add(receivedTank);
            }

            Invalidate(); // Refresh the UI
        }


        public void RemovePlayer(string receivedTankId)
        {
            tankCollection.RemovePlayer(receivedTankId);
            //allPlayers.RemoveAll(t => t.playerId == receivedTankId);
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


        private void OnTemporaryEffectTimerElapsed(object sender, ElapsedEventArgs e)
        {
            ApplyRandomStrategiesToObstacles();
            Invalidate();
            Task.Delay(5000).ContinueWith(_ =>
            {
                RevertObstacleStrategies();
                Invalidate();
                temporaryEffectTimer.Stop();
                temporaryEffectTimer.Start();
            });
        }
        private void RevertObstacleStrategies()
        {
            IIterator<Obstacle> obstacles = obstaclesCollection.CreateIterator();

            while (obstacles.HasNext())
            {
                Obstacle obstacle = obstacles.Next();
                //var originalState = originalObstacleStates[obstacle];
                var originalState = originalObstaclesStates.Get(obstacle);
                obstacle.Strategy = originalState.OriginalStrategy;
                obstacle.SetTempColor(originalState.OriginalColor);
            }

            //foreach (var obstacle in obstacles)
            //{
            //    var originalState = originalObstacleStates[obstacle];
            //    obstacle.Strategy = originalState.OriginalStrategy;
            //    obstacle.SetTempColor(originalState.OriginalColor);
            //}
        }
        private void ApplyRandomStrategiesToObstacles()
        {
            Random random = new Random();

            IIterator<Obstacle> obstacles = obstaclesCollection.CreateIterator();

            while (obstacles.HasNext())
            {
                Obstacle obstacle = obstacles.Next();
                int randomIndex = random.Next(randomStrategies.Count);
                obstacle.Strategy = randomStrategies[randomIndex];
                obstacle.SetTempColor(Color.Magenta);
            }

            //foreach (var obstacle in obstacles)
            //{
            //    int randomIndex = random.Next(randomStrategies.Count);
            //    obstacle.Strategy = randomStrategies[randomIndex];
            //    obstacle.SetTempColor(Color.Magenta);
            //}
        }
        private Color GetObstacleColor(Obstacle obstacle)
        {
            if (obstacle is Mist)
            {
                obstacle.SetOriginalColor(Color.LightGray);
                obstacle.SetTempColor(Color.LightGray);
                return Color.LightGray;
            }

            if (obstacle is Mud)
            {
                obstacle.SetOriginalColor(Color.SaddleBrown);
                obstacle.SetTempColor(Color.SaddleBrown);
                return Color.SaddleBrown;
            }

            if (obstacle is Ice)
            {
                obstacle.SetOriginalColor(Color.LightSteelBlue);
                obstacle.SetTempColor(Color.LightSteelBlue);
                return Color.LightSteelBlue;
            }

            if (obstacle is Snow)
            {
                obstacle.SetOriginalColor(Color.LightBlue);
                obstacle.SetTempColor(Color.LightBlue);
                return Color.LightBlue;
            }
            obstacle.SetOriginalColor(Color.Black);
            obstacle.SetTempColor(Color.Black);
            return Color.Black;
        }


        protected override void OnPaint(PaintEventArgs e)
        {

            base.OnPaint(e);
            IIterator<Obstacle> obstacles = obstaclesCollection.CreateIterator();

            while (obstacles.HasNext())
            {
                Obstacle obstacle = obstacles.Next();
                e.Graphics.FillRectangle(new SolidBrush(obstacle.GetTempColor()), obstacle.x_coordinate, obstacle.y_coordinate, 50, 50);
            }

            //foreach (var obstacle in obstacles)
            //{
            //    e.Graphics.FillRectangle(new SolidBrush(obstacle.GetTempColor()), obstacle.x_coordinate, obstacle.y_coordinate, 50, 50);
            //}

            e.Graphics.DrawImage(coin.Details.image, coin.X, coin.Y, 10, 10);

            IIterator<Tank> tankIterator = tankCollection.CreateIterator();

            while (tankIterator.HasNext())
            {
                Tank player = tankIterator.Next();
                e.Graphics.FillRectangle(new SolidBrush(player.Color), player.x_coordinate, player.y_coordinate, 50, 50);

                foreach (var bullet in player.bullets.ToList())
                {
                    bullet.Rotate(e.Graphics);
                    e.Graphics.DrawImage(bullet.image, (float)bullet.X, (float)bullet.Y, bullet.Width, bullet.Height);
                    e.Graphics.ResetTransform();
                    //Image.FromFile(@"Images\PistolBullet.jpg")
                }

            }

            //foreach (var player in allPlayers.ToList())
            //{
            //    e.Graphics.FillRectangle(new SolidBrush(player.Color), player.x_coordinate, player.y_coordinate, 50, 50);

            //    foreach (var bullet in player.bullets2.ToList())
            //    {
            //        bullet.Rotate(e.Graphics);
            //        e.Graphics.DrawImage(bullet.image, (float)bullet.X, (float)bullet.Y, bullet.Width, bullet.Height);
            //        e.Graphics.ResetTransform();
            //        //Image.FromFile(@"Images\PistolBullet.jpg")
            //    }
            //}

            //Test.Test1(e);
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


        public class Test
        {
            public static void Test1(PaintEventArgs e)
            {
                // Measure memory before rendering
                long memoryBefore1 = GC.GetTotalMemory(true);
                // Measure start time
                var stopwatch1 = new System.Diagnostics.Stopwatch();

                stopwatch1.Start();

                // Setup the Flyweight bullets
                IFlyweightBullet flyweightBase = BulletFactory.getBullet("TommyGun");
                for (int i = 0; i < 1000; i++)
                {
                    string id = Guid.NewGuid().ToString();
                    Bullet bullet = flyweightBase.Create(id, i * 10 % 800, i * 5 % 600, "Right", 0, 0);
                }

                // Stop the stopwatch
                stopwatch1.Stop();

                // Measure memory after rendering
                long memoryAfter1 = GC.GetTotalMemory(false);

                // Output results
                Console.WriteLine($"Flyweight - Time Taken: {stopwatch1.ElapsedMilliseconds} ms");
                Console.WriteLine($"Flyweight - Memory Used: {memoryAfter1 - memoryBefore1} bytes");




                // Measure memory before rendering
                long memoryBefore2 = GC.GetTotalMemory(true);
                // Measure start time
                var stopwatch2 = new System.Diagnostics.Stopwatch();

                stopwatch2.Start();

                // Setup the non-Flyweight bullets
                for (int i = 0; i < 1000; i++)
                {
                    Bullet bullet = new Bullet(Image.FromFile(@"Images\TommyGunBullet.png"), "TommyGun", 10, Guid.NewGuid().ToString(),
                        0, 0, i * 10 % 800, i * 5 % 600, "Right");
                    e.Graphics.DrawImage(bullet.image, (float)bullet.X, (float)bullet.Y, bullet.Width, bullet.Height);
                    
                }

                // Stop the stopwatch
                stopwatch2.Stop();

                // Measure memory after rendering
                long memoryAfter2 = GC.GetTotalMemory(false);

                // Output results
                Console.WriteLine($"NONFlyweight - Time Taken: {stopwatch2.ElapsedMilliseconds} ms");
                Console.WriteLine($"NONFlyweight - Memory Used: {memoryAfter2 - memoryBefore2} bytes");

            }

        }

    }
}
