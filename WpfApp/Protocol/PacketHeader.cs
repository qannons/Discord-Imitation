using System;

namespace WpfApp.Protocol
{
    struct PacketHeader
    {
        public UInt16 size;
        public UInt16 id; // 프로토콜ID (ex. 1=로그인, 2=이동요청)
    }

    public enum ePacketSize : int
    {
        eS_TEST = 16
    }

    public enum ePacketID : UInt16
    {
        S_TEST = 1
    };
}