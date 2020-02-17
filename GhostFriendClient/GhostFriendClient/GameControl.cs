using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostFriendClient
{
    public static class GameControl
    {
        public static void Join(string playerName)
        {            
            SocketClient.Instance.SendData(playerName);
        }
    }
}
