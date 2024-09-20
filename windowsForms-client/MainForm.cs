using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace windowsForms_client

{
    public partial class MainForm : Form
    {
        private ClientWebSocket clientSocket;
        private string playerId;
        private ConcurrentDictionary<string, (int x, int y)> otherPlayers = new ConcurrentDictionary<string, (int, int)>();

        public MainForm()
        {
            InitializeComponent();
            KeyDown += new KeyEventHandler(OnKeyDown); //Kai dabartinis playeris pajuda yra pakeiciama pozicija
            Task.Run(ConnectWebSocket); // Start WebSocket client
        }

        private async Task ConnectWebSocket()
        {
            clientSocket = new ClientWebSocket();
            await clientSocket.ConnectAsync(new Uri("ws://localhost:5000/ws"), CancellationToken.None);
            Console.WriteLine("Connected to server");

            playerId = clientSocket.GetHashCode().ToString(); // Get playerId from WebSocket hashcode

            // Initialize the player's position
            otherPlayers[playerId] = (100, 100); // Start at (100, 100)

            Invalidate();
            await SendPlayerPosition();
            await ReceiveUpdates();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            var movement = (x: 0, y: 0);
            if (e.KeyCode == Keys.Up) movement.y = -10;
            if (e.KeyCode == Keys.Down) movement.y = 10;
            if (e.KeyCode == Keys.Left) movement.x = -10;
            if (e.KeyCode == Keys.Right) movement.x = 10;

            // Get the current player position
            var currentPosition = otherPlayers[playerId];

            // Calculate the new position
            int newX = currentPosition.x + movement.x;
            int newY = currentPosition.y + movement.y;

            // Clamp the new position to stay within the form's bounds
            // Assuming the player size is 50x50 (as you use in OnPaint) and the form's client area is used
            newX = Math.Max(0, Math.Min(newX, ClientSize.Width - 50));
            newY = Math.Max(0, Math.Min(newY, ClientSize.Height - 50));

            // Update the player's position
            otherPlayers[playerId] = (newX, newY);

            Invalidate(); // Force re-draw to immediately show changes
            SendPlayerPosition();
        }




        private async Task SendPlayerPosition()
        {
            var currentPosition = otherPlayers[playerId];
            string message = $"{playerId},{currentPosition.x},{currentPosition.y}";
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
                otherPlayers[otherPlayerId] = (x, y);

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
                e.Graphics.FillRectangle(System.Drawing.Brushes.Blue, player.Value.x, player.Value.y, 50, 50);
            }
        }
    }
}
