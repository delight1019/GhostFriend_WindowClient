using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace GhostFriendClient
{
    class SocketClient
    {
        private static SocketClient instance;

        private const int PORT = 9000;
        private bool isConnected = false;

        private Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public void StartConnection()
        {
            var ep = new IPEndPoint(IPAddress.Parse(GetLocalIP()), PORT);
            while (!isConnected)
            {
                try
                {
                    socket.Connect(ep);
                    isConnected = true;
                }
                catch
                {
                    isConnected = false;
                }
            }                    
        }

        public void CloseConnection(bool isReused)
        {
            socket.Disconnect(isReused);
            socket.Close();
        }

        public void SendData(string data)
        {
            byte[] dataBuffer = Encoding.UTF8.GetBytes(data + "\r\n");
            socket.Send(dataBuffer);
        }

        public String ReceiveData()
        {
            byte[] dataBuffer = new byte[8192];
            int n = socket.Receive(dataBuffer);
            string data = Encoding.UTF8.GetString(dataBuffer, 0, n);

            return data;
        }

        private string GetLocalIP()
        {
            string localIP = "Not available, please check your network settings!";

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }

            return localIP;
        }

        public static SocketClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SocketClient();
                }

                return instance;
            }
        }
    }
}
