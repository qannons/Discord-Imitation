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
using WpfApp.Model;
using WpfApp.Protocol;
using System.IO;

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
                ChatMessage message = new ChatMessage();
                {
                    P_Sender sender = new P_Sender();
                    sender.UserID = 1;
                    sender.Username = "cannons";
                    message.Sender = sender;
                }
                message.Content = data;

                message.Timestamp = 100000;

                message.Type = EP_MessageType.Text;

                message.RoomID = "1212";

                MyPacketHeader header = new MyPacketHeader();
                header.id = 1;
                header.size = (UInt16)(sizeof(MyPacketHeader)+message.CalculateSize());
                using (var memoryStream = new MemoryStream())
                {
                    // PacketHeader를 바이트 배열로 변환하여 MemoryStream에 쓰기
                    using (var writer = new BinaryWriter(memoryStream, Encoding.Default, true))
                    {
                        writer.Write(header.size);
                        writer.Write(header.id);
                    }

                    // 현재 MemoryStream의 위치는 PacketHeader 다음 위치입니다.
                    // 여기서부터 Protobuf 메시지를 직렬화하여 쓸 수 있습니다.
                    message.WriteTo(memoryStream);

                    // 최종 바이트 배열 가져오기
                    byte[] finalBytes = memoryStream.ToArray();

                    // finalBytes를 네트워크 스트림에 쓰기
                    // 예: stream.Write(finalBytes, 0, finalBytes.Length);
                    stream.Write(finalBytes, 0, finalBytes.Length);
                }

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