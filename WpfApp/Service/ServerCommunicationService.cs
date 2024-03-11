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
using WpfApp.MVVM.Model;
using System.Threading;

namespace WpfApp.Service
{
    internal class ServerCommunicationService : IServerCommunicationService
    {
        //변수
        private bool _connected = false;
        private TcpClient client;
        private NetworkStream stream;
        //생성자
        public ServerCommunicationService()
        {
            client = new TcpClient();
        }

        public bool Connect(string serverIp, int serverPort)
        {
            try
            {
                client.Connect(serverIp, serverPort);
                stream = client.GetStream();
                Volatile.Write(ref _connected, true);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to connect to server: " + e.Message);
                return false;
            }
        }

        public unsafe void Send(Guid roomID, string data, User user, ePacketID messageID = 0)
        {
            switch(messageID)
            {
                case ePacketID.CHAT_MESSAGE:          
                    try
                    {
                        P_ChatMessage message = new P_ChatMessage();
                        //1
                        message.Base.MessageID = "tmpID";
                        //2
                        message.Base.RoomID = "1212";
                        //3
                        {
                            P_Sender sender = new P_Sender();
                            sender.UserID = user.ID;
                            sender.Username = user.Name;
                            message.Base.Sender = sender;
                        }
                        //4
                        message.Content = data;

                        //5
                        //message.ExtraContent = ;
                        //6
                        //message.Timestamp = 100000;

                        //message.Type = EP_MessageType.Text;


                        PacketHeader header = new PacketHeader();
                        header.id = 1;
                        header.size = (UInt16)(sizeof(PacketHeader)+message.CalculateSize());
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
                    break;

                //case ePacketID.IMAGE:
                //    break;
            }
        }

        public MyBuffer? Recv()
        {
            if (!_connected)
                return null;

            byte[] buffer = new byte[512];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);

            MyBuffer ret = new MyBuffer
            {
                buffer = buffer,
                len = bytesRead
            };

            return ret;
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