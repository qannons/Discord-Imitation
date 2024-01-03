using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Service.Interface
{
    public interface IServerCommunicationService
    {
        public void Connect(string host, int port);
        public void Disconnect();
        public void Send(string data);
        public string Recv();
    }
}
