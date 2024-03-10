using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Protocol
{
    public struct MyPacketHeader
    {
        public UInt16 size;
        public UInt16 id; // 프로토콜ID (ex. 1=로그인, 2=이동요청)
    }
}
