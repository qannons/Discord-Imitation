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