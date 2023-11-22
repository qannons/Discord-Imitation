using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace WpfApp.Service
{
    internal class ServerCommunicationService
    {

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

        public void SendData(string data)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                stream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to send data: " + e.Message);
            }
        }

        public string ReceiveData()
        {
            try
            {
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
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
        //Private 변수
        private TcpClient client;
        private NetworkStream stream;
    }
}