using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostFriendClient
{
    public class GameControl
    {
        private static GameControl instance;

        public void Join(string playerName)
        {            
            SocketClient.Instance.SendData(playerName);
            WaitOtherPlayers();
        }

        private void WaitOtherPlayers()
        {
            while (true)
            {
                String playersInfo =  SocketClient.Instance.ReceiveData();
            }
        }

        public static GameControl Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameControl();
                }

                return instance;
            }
        }
    }
}
