using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace csharp_server.Observer
{
    public class Server : Subject
    {
        public override async Task HandleWebSocketCommunication(ClientObserver clientObserver)
        {
            await clientObserver.HandleCommunication(); // Let the observer manage its own communication
        }

        public override void SetUsedColor(string color)
        {
            this.UsedColor = color;
        }

        public override int GetNumberOfObservers()
        {
            return this.NumberOfObservers;
        }


        public override void NotifyAboutUpgrade(string upgradeType, string message)
        {
            foreach (var observer in upgradeObservers[upgradeType])
            {
                observer.Update(message);
            }
        }


        public override void SubscribeToUpgrade(IObserver observer, string upgrade)
        {
            if (!upgradeObservers.ContainsKey(upgrade))
            {
                upgradeObservers[upgrade] = new List<IObserver>();
            }
            upgradeObservers[upgrade].Add(observer);
        }

        public override void DeatchUpgradeUser(IObserver observer)
        {
            foreach (var upgrade in upgradeObservers.Keys.ToList())
            {
                upgradeObservers[upgrade].Remove(observer);

                if (upgradeObservers[upgrade].Count == 0)
                {
                    upgradeObservers.Remove(upgrade);
                }
            }
        }

    }
}
