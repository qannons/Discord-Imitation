using System;
using System.IO;

namespace WpfApp.Service.Interface
{
    internal interface IMessagePacket
    {
        UInt16 CalculateSize();
        void WriteTo(MemoryStream memoryStream);
    }
}