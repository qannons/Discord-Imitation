using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Protocol
{
    public struct PacketHeader
    {
        public UInt16 size;
        public UInt16 id; // 프로토콜ID (ex. 1=로그인, 2=이동요청)
    }

    public enum ePacketID : UInt16
    {
        CHAT_MESSAGE = 0,
        IMAGE_MESSAGE = 1
    }

    public class ImageMessageEventArgs : EventArgs
    {
        public string Content { get; }

        public ImageMessageEventArgs(string content)
        {
            Content = content;
        }
    }

}
