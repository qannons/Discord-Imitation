using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WpfApp.Service;

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

    public struct MyBuffer
    {
        public byte[] buffer;
        public int len;

        public MyBuffer(byte[] pBuffer, int pLen)
        {
            buffer = pBuffer; len = pLen;
        }
    }
}
