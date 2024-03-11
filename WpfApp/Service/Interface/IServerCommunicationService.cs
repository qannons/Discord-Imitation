using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.MVVM.Model;

namespace WpfApp.Service.Interface
{
    public interface IServerCommunicationService
    {
        public void Connect(string host, int port);
        public void Disconnect();
        public void Send(Guid roomID, string data, User user);
        public (byte[], int) Recv();
    }
}
