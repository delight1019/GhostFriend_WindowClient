using GhostFriendClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostFriendClient.Model
{
    public class GameControl
    {
        private const char PLAYER_INFO_DELIMITER = '/';
        private const int MAX_PLAYERS_NUM = 5;
        private String[] playersInfo;

        private static GameControl instance;

        public void Join(string playerName)
        {
            SocketClient.Instance.SendData(GameParams.JOIN_GAME);
            SocketClient.Instance.SendData(playerName);
        }                

        public String[] ReceivePlayersInfo()
        {
            SocketClient.Instance.SendData(GameParams.ASK_PLAYERS_INFO);

            String playersInfoData = SocketClient.Instance.ReceiveData();
            this.playersInfo = playersInfoData.Split(PLAYER_INFO_DELIMITER);

            return playersInfo;
        }

        public Boolean IsAllPlayersParticipatedIn()
        {
            if (this.playersInfo == null)
            {
                return false;
            }

            return (this.playersInfo.Length == MAX_PLAYERS_NUM);
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
