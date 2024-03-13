using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.MVVM.Model;
using WpfApp.Protocol;
using WpfApp.Service;

namespace WpfApp.Service.Interface
{
    public interface IServerCommunicationService
    {
        public bool Connect(string host, int port);
        public void Disconnect();
        public void Send(Guid roomID, byte[] data, User user, ePacketID messageID);
        public int Recv(Span<byte> recvBuffer);
    }
}
