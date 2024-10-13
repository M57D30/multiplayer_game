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

namespace windowsForms_client

{
    public partial class MainForm : Form
    {
        private ClientWebSocket clientSocket;
        Tank CurrentTank;
        private List<Tank> otherPlayers = new List<Tank>();

        private int playerVelocityX = 0;
        private int playerVelocityY = 0;
        private System.Timers.Timer gameLoopTimer;

        public MainForm()
        {
            InitializeComponent();
            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;
            Task.Run(ConnectWebSocket); // Start WebSocket client
        }

        private async Task ConnectWebSocket()
        {
            clientSocket = new ClientWebSocket();
            await clientSocket.ConnectAsync(new Uri("ws://localhost:5000/ws"), CancellationToken.None);
            Console.WriteLine("Connected to server");

            string playerId = clientSocket.GetHashCode().ToString(); // Get playerId from WebSocket hashcode

            // Initialize the player's position
            CurrentTank = new Tank(playerId, 100, 100);
            otherPlayers.Add(CurrentTank);

            BeginGameLoop();

            await ReceiveUpdates();
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
            if (e.KeyCode == Keys.Up) playerVelocityY = -10;
            if (e.KeyCode == Keys.Down) playerVelocityY = 10;
            if (e.KeyCode == Keys.Left) playerVelocityX = -10;
            if (e.KeyCode == Keys.Right) playerVelocityX = 10;
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down) playerVelocityY = 0;
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right) playerVelocityX = 0;
        }


        private (int x, int y) lastSentPosition;

        private async void OnGameLoop(object sender, ElapsedEventArgs e)
        {


            // Update player position based on velocity
            var currentPlayer = otherPlayers.FirstOrDefault(tank => tank.playerId == this.CurrentTank.playerId);
            int newX = currentPlayer.x_coordinate + playerVelocityX;
            int newY = currentPlayer.y_coordinate + playerVelocityY;

            // Ensure player stays within form bounds
            newX = Math.Max(0, Math.Min(newX, ClientSize.Width - 50));
            newY = Math.Max(0, Math.Min(newY, ClientSize.Height - 50));

            // Update the player's position
            otherPlayers.FirstOrDefault(tank => tank.playerId == this.CurrentTank.playerId).x_coordinate = newX;
            otherPlayers.FirstOrDefault(tank => tank.playerId == this.CurrentTank.playerId).y_coordinate = newY;

            // Only send the position if it has changed
            if (newX != lastSentPosition.x || newY != lastSentPosition.y)
            {
                lastSentPosition = (newX, newY); // Update the last sent position
                await SendPlayerPosition();      // Send updated position to the server
            }


            Invalidate();
        }



        private async Task SendPlayerPosition()
        {
            var currentPosition = otherPlayers.FirstOrDefault(tank => tank.playerId == this.CurrentTank.playerId);
            string message = $"{this.CurrentTank.playerId},{currentPosition.x_coordinate},{currentPosition.y_coordinate}";
            var buffer = Encoding.UTF8.GetBytes(message);
            await clientSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
        }



        private async Task ReceiveUpdates()
        {
            var buffer = new byte[1024 * 4];
            while (clientSocket.State == WebSocketState.Open)
            {
                var result = await clientSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                var parts = message.Split(',');

                string otherPlayerId = parts[0];
                int x = int.Parse(parts[1]);
                int y = int.Parse(parts[2]);

                // Update position for other players
                if (otherPlayers.Any(tank => tank.playerId == otherPlayerId))
                {
                    otherPlayers.FirstOrDefault(tank => tank.playerId == otherPlayerId).x_coordinate = x;
                    otherPlayers.FirstOrDefault(tank => tank.playerId == otherPlayerId).y_coordinate = y;
                }
                else
                {
                    Tank newTank = new Tank(otherPlayerId, x, y);
                    otherPlayers.Add(newTank);
                }
                // Redraw the form to update the view
                Invalidate();
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw all players, including the current player
            foreach (var player in otherPlayers)
            {
                e.Graphics.FillRectangle(System.Drawing.Brushes.Blue, player.x_coordinate, player.y_coordinate, 50, 50);
            }
        }
    }
}
