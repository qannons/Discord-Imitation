using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using WpfApp.Service.Interface;

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

        public void Send(string data)
        {
            try
            {
                byte[] buffer = new byte[data.Length+13];
                buffer[0] = (byte)data.Length;
                //Encoding.UTF8.GetBytes(data).CopyTo(buffer, 1);
                Encoding.Unicode.GetBytes(data).CopyTo(buffer, 2);
                //Encoding.UTF32.GetBytes(data).CopyTo(buffer, 4);
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