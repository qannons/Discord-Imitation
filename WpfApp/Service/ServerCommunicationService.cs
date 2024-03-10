using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using WpfApp.Service.Interface;
using System.Diagnostics;
using Google.Protobuf;
using Protocol;
using WpfApp.Protocol;
namespace WpfApp.Service
{
    internal class ServerCommunicationService : IServerCommunicationService
    {
        //Private 변수
        private TcpClient client;
        private NetworkStream stream;
        //생성자
        public ServerCommunicationService()
        {
            client = new TcpClient();
        }

        public void Connect(string serverIp, int serverPort)
        {
            try
            {
                client.Connect(serverIp, serverPort);
                stream = client.GetStream();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to connect to server: " + e.Message);
            }
        }

        public unsafe void Send(Guid roomID, string data)
        {
            try
            {
                byte[] buffer = new byte[68 + data.Length * 2];
                //roomID.ToByteArray().CopyTo(buffer, 0);
                
                string s = roomID.ToString();
                
                PacketHeader header = new PacketHeader();
                header.id = (ushort)ePacketID.S_TEST;
                header.size = (ushort)(data.Length + sizeof(PacketHeader));

                S_TEST s_TEST = new S_TEST();
                s_TEST.Id = 10;
                s_TEST.Attack = 20;
                s_TEST.Hp = 30;
                s_TEST.WriteTo(stream);
               // Encoding.Unicode.GetBytes(roomID.ToString()).CopyTo(buffer, 0);
                //Encoding.Unicode.GetBytes(data.Length.ToString()).CopyTo(buffer, 16);
                //Encoding.Unicode.GetBytes(data).CopyTo(buffer, 18);
                Encoding.Unicode.GetBytes(data).CopyTo(buffer, 0);
               
                stream.Write(buffer, 0, buffer.Length);
                
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to send data: " + e.Message);
            }
        }

        public (byte[], int) Recv()
        {
            byte[] buffer = new byte[512];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);

            return (buffer, bytesRead);
            OnRecv(buffer, bytesRead);  
        }

        public int OnRecv(byte[] buffer, int len)
        {
            int processLen = 0;

            unsafe
            {
                fixed (byte* p = buffer)
                {
                    while (true)
                    {
                        int dataSize = len - processLen;
                        if (dataSize < sizeof(PacketHeader))
                            break;

                        PacketHeader* headerPtr = (PacketHeader*)(p + processLen);
                        PacketHeader header = *headerPtr;
                        if (dataSize < header.size)
                            break;
                        byte[] tmp = new byte[dataSize];
                        Buffer.BlockCopy(buffer, processLen, tmp, 0, header.size);
                        //OnRecvPacket(p + processLen, header.size);
                        HandlePacket(tmp, header.size);
                        processLen += header.size;
                    }
                }
            }
            return processLen;
        }

        unsafe void HandlePacket(byte[] buffer,  int len)
        {
            S_TEST pkt = S_TEST.Parser.ParseFrom(buffer, sizeof(PacketHeader), len - sizeof(PacketHeader));
            //S_TEST pkt =  S_TEST.Parser.ParseFrom(buffer + sizeof(PacketHeader), len - sizeof(PacketHeader));
            Debug.WriteLine($"Received ID: {pkt.Id}");
            Debug.WriteLine($"Received HP: {pkt.Hp}");
            Debug.WriteLine($"Received Attack: {pkt.Attack}");

        }

        public void Disconnect()
        {
            stream.Close();
            client.Close();
        }

        struct Packet
        {
            public Int32 size;
            public string data;
        }
    }
}