using csharp_server.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebSocketServer
{
    class Program
    {

        static async Task Main(string[] args)
        {
            HttpListener httpListener = new HttpListener();
            httpListener.Prefixes.Add("http://localhost:5000/ws/");
            httpListener.Start();
            Console.WriteLine("WebSocket server started at ws://localhost:5000/ws");
            Subject server = new Server();

            var gameStartEvent = new GameStartEvent(server);
            int numberOfPlayers = 1;
            while (true)
            {
                HttpListenerContext httpContext = await httpListener.GetContextAsync();

                if (httpContext.Request.IsWebSocketRequest && server.GetNumberOfObservers() < 2)
                {
                    HttpListenerWebSocketContext wsContext = await httpContext.AcceptWebSocketAsync(null);
                    WebSocket webSocket = wsContext.WebSocket;

                    var clientObserver = new ClientObserver(webSocket);
                    clientObserver.SetServer(server);
                    clientObserver.SetPlayerIdAndColor();
                    server.Attach(clientObserver); // Attach the client observer
                    Console.WriteLine("Client connected");

                    _ = server.HandleWebSocketCommunication(clientObserver); // Pass the observer to handle communication

                    if (numberOfPlayers == 2)
                    {
                        _ = Task.Run(() => gameStartEvent.Start());
                        numberOfPlayers--;


                    }
                    numberOfPlayers++;
                }
                else
                {
                    httpContext.Response.StatusCode = 400;
                    httpContext.Response.Close();
                }
            }
        }

       
    }
}
