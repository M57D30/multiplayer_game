using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using windowsForms_client.AbstractFactoryPatternn.Factorys;
using windowsForms_client.AbstractFactoryPatternn;
using System.Text.Json.Serialization;
using System.Text.Json;
//using Newtonsoft.Json;

namespace windowsForms_client
{
    internal class WebSocketComunication
    {
        private ClientWebSocket clientSocket;
        private GameClient client;
        string tankType;
        string selectedUpgrade;

        public WebSocketComunication(string tankType, string selectedUpgrade, GameClient gameClient)
        {
            this.tankType = tankType;
            this.client = gameClient;
            this.selectedUpgrade = selectedUpgrade;
            Task.Run(ConnectWebSocket);
            
          
        }

        private async Task ConnectWebSocket()
        {
            clientSocket = new ClientWebSocket();
            await clientSocket.ConnectAsync(new Uri("ws://localhost:5000/ws"), CancellationToken.None);
            Console.WriteLine("Connected to server");


            ////////Tokie patys ID's kaip ir serveri
            var buffer = new byte[1024];
            var result = await clientSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            string playerId = Encoding.UTF8.GetString(buffer, 0, result.Count);

            buffer = new byte[1024];
            result = await clientSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            string tankColor = Encoding.UTF8.GetString(buffer, 0, result.Count);
            
            await SendSelectedUpgrade();

            client.InitializeTank(tankType, tankColor, playerId);
            await ReceiveUpdates();
        }


        public async Task SendSelectedUpgrade()
        {
            if (clientSocket.State == WebSocketState.Open && selectedUpgrade != "-")
            {
                var message = $"SubscribeUpgrade,{selectedUpgrade}"; // Create a message to send
                var buffer = Encoding.UTF8.GetBytes(message);
                await clientSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                Console.WriteLine($"Sent upgrade subscription: {selectedUpgrade}");
            }
        }


        private SemaphoreSlim _sendSemaphore = new SemaphoreSlim(1, 1);
        public async Task SendTankInformation(Tank currentTank)
        {
            await _sendSemaphore.WaitAsync(); // Ensure no concurrent sends
            try
            {
                if (clientSocket.State == WebSocketState.Open)
                {
                    var options = new JsonSerializerOptions();
                    options.Converters.Add(new TankConverter());
                    string message = JsonSerializer.Serialize(currentTank, options);
                    var buffer = Encoding.UTF8.GetBytes(message);
                    await clientSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}"); // Debug log
            }
            finally
            {
                _sendSemaphore.Release(); // Always release the semaphore
            }
        }

      
      

        private async Task ReceiveUpdates()
        {
            var buffer = new byte[8192];
            var options = new JsonSerializerOptions();
            options.Converters.Add(new TankConverter());

            while (clientSocket.State == WebSocketState.Open)
            {
                try
                {
                    var result = await clientSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        Console.WriteLine("Server requested to close the connection.");
                        break;
                    }

                    if (message.StartsWith("Start"))
                    {
                        client.StartCountingTime();
                        Console.WriteLine(message);
                        client.DisplayGameState("Start");
                        continue;
                    }

                    if (message.StartsWith("Stop"))
                    {
                        client.StopCountingTime();
                        client.DisplayGameState("Stop");
                        continue;
                    }

                    if (message.StartsWith("Upgrade"))
                    {
                        string upgradeValue = message.Split(' ')[2];
                        string upgradeType = message.Split(' ')[1];
                        Console.WriteLine($"Received upgrade: {message}");
                        client.UpgradePlayer(int.Parse(upgradeValue), upgradeType);
                        continue;
                    }

                    if (message.StartsWith("Remove"))
                    {
                        // Extract player ID from the message
                        string playerIdToRemove = message.Split(',')[1];
                        Console.WriteLine($"Removing player with ID: {playerIdToRemove}");
                        client.RemovePlayer(playerIdToRemove); // Call method to remove the player by ID
                        continue; // Skip processing the rest of the message
                    }


                    try
                    {
                        Tank receivedTank = JsonSerializer.Deserialize<Tank>(message, options);
                        client.UpdatePlayerPosition(receivedTank);
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"JSON deserialization error: {ex.Message}"); // Debug log
                    }       
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception in ReceiveUpdates: {ex.Message}"); // Debug log

                }
            }

        }

        public async Task SendAsyncClosing()
        {
            if (clientSocket.State == WebSocketState.Open)
            {
                
                await clientSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            }
        }


        private class TankConverter : JsonConverter<Tank>
        {
            public override Tank Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {

                using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
                {
                    
                    var root = doc.RootElement;
                    var tankType = root.GetProperty("TankType").GetString();
                    // Determine the specific type of tank
                    Type actualType = Type.GetType($"windowsForms_client.Tanks.{tankType}");

                    if (actualType == null)
                    {
                        throw new JsonException($"Unknown tank type: {tankType}");
                    }

                    // Deserialize into the appropriate subclass
                    return (Tank)JsonSerializer.Deserialize(root.GetRawText(), actualType, options);
                }
            }

            public override void Write(Utf8JsonWriter writer, Tank value, JsonSerializerOptions options)
            {
                // Serialize the entire object, including the type discriminator
                JsonSerializer.Serialize(writer, value, value.GetType(), options);
            }
        }
    }

}
