﻿using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.Timers;
using windowsForms_client.AbstractFactoryPatternn.Factorys;
using windowsForms_client.AbstractFactoryPatternn;
using System.Windows.Forms;
using System.Text.Json.Serialization;
using Iterator.Iterators;
using windowsForms_client.Flyweight;
using Iterator.Collections;

namespace windowsForms_client
{
    internal class WebSocketComunication
    {
        private static WebSocketComunication _instance;
        private static readonly object _lock = new object();  // Lock for thread-safety

        private ClientWebSocket clientSocket;
        private GameClientFacade client;
        private string tankType;
        private string selectedUpgrade;

        private WebSocketComunication(string tankType, string selectedUpgrade, GameClientFacade gameClient)
        {
            this.tankType = tankType;
            this.client = gameClient;
            this.selectedUpgrade = selectedUpgrade;
            Task.Run(ConnectWebSocket); // Start WebSocket connection asynchronously
        }

        // Singleton Instance
        public static WebSocketComunication Instance(string tankType, string selectedUpgrade, GameClientFacade gameClient)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new WebSocketComunication(tankType, selectedUpgrade, gameClient);
                }
                return _instance;
            }
        }

        private async Task ConnectWebSocket()
        {
            clientSocket = new ClientWebSocket();
            await clientSocket.ConnectAsync(new Uri("ws://localhost:5000/ws"), CancellationToken.None);
            Console.WriteLine("Connected to server");

            // Receive Player ID and tank color
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
                var message = $"SubscribeUpgrade,{selectedUpgrade}";
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
                Console.WriteLine($"Error sending message: {ex.Message}");
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
                        string playerIdToRemove = message.Split(',')[1];
                        Console.WriteLine($"Removing player with ID: {playerIdToRemove}");
                        client.RemovePlayer(playerIdToRemove);
                        continue;
                    }

                    try
                    {
                        Tank receivedTank = JsonSerializer.Deserialize<Tank>(message, options);
                    
                        client.UpdatePlayerPosition(receivedTank);
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"JSON deserialization error: {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception in ReceiveUpdates: {ex.Message}");
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
                    Type actualType = Type.GetType($"windowsForms_client.Tanks.{tankType}");

                    if (actualType == null)
                    {
                        throw new JsonException($"Unknown tank type: {tankType}");
                    }

                    Tank tank = (Tank)JsonSerializer.Deserialize(root.GetRawText(), actualType, options);



                    for (int i = 0; i < tank.bullets.Count; i++)
                    {
                        var originalBullet = tank.bullets[i];

                        IFlyweightBullet flyweightBullet = BulletFactory.getBullet(originalBullet.BulletType);

                        var newBullet = flyweightBullet.Create(originalBullet.Id, originalBullet.X, originalBullet.Y, originalBullet.Direction, originalBullet.X_aditional, originalBullet.Y_aditional);

                        tank.bullets[i] = newBullet;
                    }

                    return tank;
                }
            }

            public override void Write(Utf8JsonWriter writer, Tank value, JsonSerializerOptions options)
            {
               JsonSerializer.Serialize(writer, value, value.GetType(), options);

            }
        }

        //public class BulletCollectionConverter : JsonConverter<BulletCollection>
        //{
        //    public override BulletCollection Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        //    {
        //        BulletCollection bulletCollection = new BulletCollection();
        //        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        //        {
        //            var root = doc.RootElement;
        //            var bulletsArray = root.GetProperty("Bullets").EnumerateArray();

        //            foreach (var bulletElement in bulletsArray)
        //            {
        //                Bullet bullet = JsonSerializer.Deserialize<Bullet>(bulletElement.GetRawText(), options);
        //                bulletCollection.Add(bullet);
        //            }
        //        }
        //        return bulletCollection;
        //    }

        //    public override void Write(Utf8JsonWriter writer, BulletCollection value, JsonSerializerOptions options)
        //    {
        //        JsonSerializer.Serialize(writer, value., options);
        //    }
        //}

    }
}
