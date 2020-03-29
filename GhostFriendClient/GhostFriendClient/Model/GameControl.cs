using GhostFriendClient.Common;
using GhostFriendClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GhostFriendClient.Model
{
    public class GameControl
    {        
        private const int MAX_PLAYERS_NUM = 5;
        private String[] playersInfo;

        private static GameControl instance;

        public void Start()
        {
            ThreadPool.QueueUserWorkItem(ListenToServer);
        }

        public void Join(string playerName)
        {
            SocketClient.Instance.SendData(GameParams.JOIN_GAME);
            SocketClient.Instance.SendData(playerName);
        }                

        public String[] ReceivePlayersInfo()
        {
            SocketClient.Instance.SendData(GameParams.ASK_PLAYERS_INFO);

            String playersInfoData = SocketClient.Instance.ReceiveData();
            this.playersInfo = playersInfoData.Split(GameParams.PLAYER_INFO_DELIMITER);

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

        private void ListenToServer(object state)
        {
            while (SocketClient.Instance.IsConnected)
            {
                String serverCommand = SocketClient.Instance.ReceiveData();
                String[] commandList = serverCommand.Split(GameParams.COMMAND_DELIMITER);

                for (int i = 0; i < commandList.Length; i++)
                {
                    HandleCommand(commandList[i]);
                }
            }
        }

        private void HandleCommand(String command)
        {
            if (command.Equals(GameParams.JOIN_FAIL))
            {
                EventController.Instance.OnJoiningGameFailed(new EventArgs());
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
