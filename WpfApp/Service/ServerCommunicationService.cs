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
                Console.WriteLine("Failed to connect to server: " + e.Message);
            }
        }

        public void Send(Guid roomID, string data)
        {
            try
            {
                byte[] buffer = new byte[68 + data.Length * 2];
                //roomID.ToByteArray().CopyTo(buffer, 0);
                
                string s = roomID.ToString();
                
               // Encoding.Unicode.GetBytes(roomID.ToString()).CopyTo(buffer, 0);
                //Encoding.Unicode.GetBytes(data.Length.ToString()).CopyTo(buffer, 16);
                //Encoding.Unicode.GetBytes(data).CopyTo(buffer, 18);
                Encoding.Unicode.GetBytes(data).CopyTo(buffer, 0);
                stream.Write(buffer, 0, buffer.Length);
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to send data: " + e.Message);
            }
        }

        public string Recv()
        {
            try
            {
                
                byte[] buffer = new byte[512];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                //string receivedData = Encoding.UTF8.GetString(buffer, 1, bytesRead);
                string receivedData = Encoding.Unicode.GetString(buffer, 0, bytesRead);


                Protocol.S_TEST receivedMessage = Protocol.S_TEST.Parser.ParseFrom(buffer);

                // 수신한 메시지의 값을 출력 또는 처리
                Console.WriteLine($"Received ID: {receivedMessage.Id}");
                Console.WriteLine($"Received HP: {receivedMessage.Hp}");
                Console.WriteLine($"Received Attack: {receivedMessage.Attack}");

                return receivedData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to receive data: " + e.Message);
                return "";
            }
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