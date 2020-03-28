using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using GhostFriendClient.Model;

namespace GhostFriendClient
{
    class SocketClient
    {
        private static SocketClient instance;

        public bool IsConnected { get; private set; } = false;
        private const int PORT = 9000;
        private Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public void StartConnection()
        {
            var ep = new IPEndPoint(IPAddress.Parse(GetLocalIP()), PORT);
            while (!IsConnected)
            {
                try
                {
                    socket.Connect(ep);
                    IsConnected = true;
                }
                catch
                {
                    IsConnected = false;
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

            string tempData = data.Replace("\n", "");
            data = tempData.Replace("\r", "");

            CompleteResponse();

            return data;
        }

        private void CompleteResponse()
        {
            SendData(GameParams.COMPLETE_REQUEST);
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
