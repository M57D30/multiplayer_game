using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace csharp_server.Observer
{
    public class GameStartEvent
    {
        private System.Timers.Timer updateTimer;
        private Subject server;

        public GameStartEvent(Subject server)
        {
            this.server = server;
        }

        public void Start()
        {
            var message = "Start gameTimer";
            server.NotifyAll(message);

            updateTimer = new System.Timers.Timer(10000); // 10 seconds
            updateTimer.Elapsed += OnTimedEvent;
            updateTimer.AutoReset = true; // Reset the timer automatically after the interval
            updateTimer.Enabled = true; // Start the timer
            Task.Run(() => this.checkedIfToStopUpdating());
        }
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            string message = "";

            foreach (var upgrade in server.upgradeObservers.Keys)
            {
                
                if (upgrade == "Movement speed boost")
                {
                    message = $"Upgrade Movement {100}";
                }
                else if (upgrade == "Shooting speed boost")
                {
                    message = $"Upgrade Shooting {100}";
                }
                else if (upgrade == "Health boost")
                {
                    message = $"Upgrade Health {100}";
                }
                else if (upgrade == "Shield boost")
                {
                    message = $"Upgrade Shield {200}";
                }

                server.NotifyAboutUpgrade(upgrade, message);
            }
        }

        public void StopUpdating()
        {
            var message = "Stop gameTimer";
            server.NotifyAll(message);
            updateTimer.Stop();
            updateTimer.Dispose();
        }

        public void checkedIfToStopUpdating()
        {
            while (true)
            {
                if (server.GetNumberOfObservers() < 2)
                {
                    this.StopUpdating();
                    break;
                }
            }
        }

    }
}
