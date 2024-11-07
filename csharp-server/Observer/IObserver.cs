using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace csharp_server.Observer
{
    public interface IObserver
    {
        void Update(string message);

        void NotifyServer(string message);

        void SetServer(Subject server);
    }
}
