using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace csharp_server.Observer
{
    public class ClientObserver : IObserver
    {
        private static readonly SemaphoreSlim _sendLock = new SemaphoreSlim(1, 1);
        private WebSocket WebSocket;
        private Subject Server;
        public string PlayerId { get; private set; }
        public string PlayerColor { get; private set; }

        public ClientObserver(WebSocket webSocket)
        {
            this.WebSocket = webSocket;
        }

        public void SetServer(Subject server)
        {
            this.Server = server;
        }

        public void SetPlayerIdAndColor()
        {
            this.SetId();
            this.SetColor();
        }

        public void SetId()
        {
            PlayerId = Guid.NewGuid().ToString();
            this.Update(PlayerId);
        }

        public void SetColor()
        {
            if (Server.UsedColor == "Red")
            {
                this.PlayerColor = "Blue";
            }
            else if (Server.UsedColor == "Blue")
            {
                this.PlayerColor = "Red";
            }

            Server.SetUsedColor(this.PlayerColor);
            this.Update(this.PlayerColor);
        }


        public void NotifyServer(string message)
        {
            Server.NotifyAll(message); 
        }


        public async void Update(string message)
        {
            await _sendLock.WaitAsync();
            try
            {
                if (WebSocket.State == WebSocketState.Open)
                {
                    var buffer = Encoding.UTF8.GetBytes(message);
                    await WebSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
            finally
            {
                _sendLock.Release();
            }
        }

        public async Task HandleCommunication()
        {
            var buffer = new byte[8192];
            WebSocketReceiveResult result = await WebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);        

            while (!result.CloseStatus.HasValue)
            {
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine($"Received message from {PlayerId}: {message}");

                if (message.StartsWith("SubscribeUpgrade"))
                {
                    var upgrade = message.Split(',')[1];
                    this.Server.SubscribeToUpgrade(this, upgrade); // Subscribe this client to the upgrade
                }
                else
                {
                    // Notify all other clients (observers)
                    this.NotifyServer(message);
                }

                result = await WebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            this.RemoveClient(result);
            
            
        }

        public async void RemoveClient(WebSocketReceiveResult result)
        {
            if (this.PlayerColor == "Blue")
            {
                Server.SetUsedColor("Red");
            }
            else if (this.PlayerColor == "Red")
            {
                Server.SetUsedColor("Blue");
            }

            Server.Detach(this);
            Server.DeatchUpgradeUser(this);

            var removeMessage = $"Remove,{PlayerId}";
            this.NotifyServer(removeMessage);

            await WebSocket.CloseOutputAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            Console.WriteLine($"Client {PlayerId} disconnected");
        }

        

    }
}
