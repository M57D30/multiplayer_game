using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace csharp_server.Observer
{
    public abstract class Subject
    {
        private List<IObserver> observers = new List<IObserver>();
        public Dictionary<string, List<IObserver>> upgradeObservers = new Dictionary<string, List<IObserver>>();

        public string UsedColor { get; protected set;} = "Red";
        protected int NumberOfObservers { get; set; }

        public void Attach(IObserver observer)
        {
            observers.Add(observer);
            this.NumberOfObservers = observers.Count;
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
            this.NumberOfObservers = observers.Count;
        }

        public void NotifyAll(string message)
        {
            foreach (var observer in observers)
            {
                observer.Update(message); 
            }
        }




        public abstract void NotifyAboutUpgrade(string upgradeType, string message);

        public abstract void SubscribeToUpgrade(IObserver observer, string upgrade);

        public abstract void DeatchUpgradeUser(IObserver observer);



        public abstract Task HandleWebSocketCommunication(ClientObserver clientObserver);

        public abstract void SetUsedColor(string color);

        public abstract int GetNumberOfObservers();

    }
}
