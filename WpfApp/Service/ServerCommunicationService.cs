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
using System.Collections;

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

        public unsafe void Send(Guid roomID, byte[] byteBuffer, User user, ePacketID messageID = 0)
        {
            switch(messageID)
            {
                case ePacketID.CHAT_MESSAGE:          
                    try
                    {
                        P_BaseMessage b_pkt = Create_P_BaseMessage(user);
                        P_ChatMessage pkt = new P_ChatMessage();

                        //2-1) 메시지 공동 내용
                        pkt.Base = b_pkt;
                        //2-2) 메시지 내용
                        string resultString = Encoding.UTF8.GetString(byteBuffer);
                        pkt.Content = resultString;

                        //패킷 헤더
                        PacketHeader header = new PacketHeader();
                        header.id = (UInt16)messageID;
                        header.size = (UInt16)(sizeof(PacketHeader)+ pkt.CalculateSize());
                        using (var memoryStream = new MemoryStream())
                        {
                            // PacketHeader를 바이트 배열로 변환하여 MemoryStream에 쓰기
                            using (var writer = new BinaryWriter(memoryStream, Encoding.Default, true))
                            {
                                writer.Write(header.size);
                                writer.Write(header.id);
                            }
                            pkt.WriteTo(memoryStream);

                            // 최종 바이트 배열 가져오기
                            byte[] finalBytes = memoryStream.ToArray();
                            stream.Write(finalBytes, 0, finalBytes.Length);
                        }

                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Failed to send data: " + e.Message);
                    }
                    break;

                case ePacketID.IMAGE_MESSAGE:
                    try
                    {
                        P_BaseMessage b_pkt = Create_P_BaseMessage(user);
                        P_ImageMessage pkt = new P_ImageMessage();

                        //2-1) 메시지 공동 내용
                        pkt.Base = b_pkt;
                        //2-2) 메시지 내용
                        ByteString imageData = ByteString.CopyFrom(byteBuffer);
                        pkt.Content = imageData;

                        //패킷 헤더
                        PacketHeader header = new PacketHeader();
                        header.id = (UInt16)messageID;
                        header.size = (UInt16)(sizeof(PacketHeader) + pkt.CalculateSize());
                        using (var memoryStream = new MemoryStream())
                        {
                            // PacketHeader를 바이트 배열로 변환하여 MemoryStream에 쓰기
                            using (var writer = new BinaryWriter(memoryStream, Encoding.Default, true))
                            {
                                writer.Write(header.size);
                                writer.Write(header.id);
                            }
                            pkt.WriteTo(memoryStream);

                            // 최종 바이트 배열 가져오기
                            byte[] finalBytes = memoryStream.ToArray();
                            stream.Write(finalBytes, 0, finalBytes.Length);
                        }

                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Failed to send data: " + e.Message);
                    }
                    break;
            }
        }

        public int Recv(Span<byte> recvBuffer)
        {
            if (!_connected)
                return 0;
            int bytesRead = stream.Read(recvBuffer);

            return bytesRead;
        }

        public void Disconnect()
        {
            stream.Close();
            client.Close();
        }

        private P_BaseMessage Create_P_BaseMessage(User user)
        {
            P_BaseMessage b_pkt = new P_BaseMessage();
            //1) 메시지 식별자
            b_pkt.MessageID = "tmpID";
            //2) 채팅방 정보
            b_pkt.RoomID = "1212";
            //3) 전송자의 정보
            {
                P_Sender sender = new P_Sender();
                sender.UserID = user.ID;
                sender.UserName = user.Name;
                b_pkt.Sender = sender;
            }
            //4) 전송시간
            b_pkt.Timestamp = 100000;

            return b_pkt;
        }
    }

}