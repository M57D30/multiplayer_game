using System;
using System.Collections.Generic;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebSocketServer
{
    class WebSocketServer
    {
        // Singleton instance
        private static readonly Lazy<WebSocketServer> instance = new Lazy<WebSocketServer>(() => new WebSocketServer());
        public static WebSocketServer Instance => instance.Value;

        private List<WebSocket> connectedClients = new List<WebSocket>();
        private HttpListener httpListener;

        // Private constructor to prevent external instantiation
        private WebSocketServer() { }

        public async Task StartAsync(string url = "http://localhost:5000/ws/")
        {
            httpListener = new HttpListener();
            httpListener.Prefixes.Add(url);
            httpListener.Start();
            Console.WriteLine($"WebSocket server started at {url}");

            while (true)
            {
                HttpListenerContext httpContext = await httpListener.GetContextAsync();

                if (httpContext.Request.IsWebSocketRequest)
                {
                    HttpListenerWebSocketContext wsContext = await httpContext.AcceptWebSocketAsync(null);
                    WebSocket webSocket = wsContext.WebSocket;
                    connectedClients.Add(webSocket);
                    Console.WriteLine("Client connected");

                    _ = HandleWebSocketCommunication(webSocket);
                }
                else
                {
                    httpContext.Response.StatusCode = 400;
                    httpContext.Response.Close();
                }
            }
        }

        const int minX = 0;
        const int minY = 0;
        const int maxX = 750; // Width of the playable area - player width
        const int maxY = 550; // Height of the playable area - player height

        private async Task HandleWebSocketCommunication(WebSocket webSocket)
        {
            string playerId = Guid.NewGuid().ToString();
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!result.CloseStatus.HasValue)
            {
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine($"Received message from {playerId}: {message}");

                var parts = message.Split(',');
                if (parts.Length == 3)
                {
                    // Extract and parse player position
                    int x = int.Parse(parts[1]);
                    int y = int.Parse(parts[2]);

                    // Create the message to broadcast
                    var clampedMessage = $"{playerId},{x},{y}";
                    await BroadcastMessageToAllClients(clampedMessage);
                }

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            connectedClients.Remove(webSocket);
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            Console.WriteLine($"Client {playerId} disconnected");
        }

        private async Task BroadcastMessageToAllClients(string message)
        {
            foreach (var client in connectedClients)
            {
                if (client.State == WebSocketState.Open)
                {
                    var buffer = Encoding.UTF8.GetBytes(message);
                    await client.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }

        // Custom Clamp function
        private int Clamp(int value, int min, int max)
        {
            return Math.Min(Math.Max(value, min), max);
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            await WebSocketServer.Instance.StartAsync();
        }
    }
}