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
        private static GameControl instance;

        public void Start()
        {
            ThreadPool.QueueUserWorkItem(ListenToServer);
        }

        public void Join(string playerName)
        {
            SendCommand(GameParams.JOIN_GAME, playerName);
        }

        private void SendCommand(string command, string data)
        {
            SocketClient.Instance.SendData(command + GameParams.DATA_DELIMITER + data + GameParams.COMMAND_DELIMITER);
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

        private void HandleCommand(String inputCommand)
        {
            String[] commandStructure = inputCommand.Split(GameParams.DATA_DELIMITER);
            String command, data;

            if (commandStructure.Length == 1)
            {
                command = commandStructure[0];
                data = "";
            }
            else if (commandStructure.Length == 2)
            {
                command = commandStructure[0];
                data = commandStructure[1];
            }
            else
            {
                command = "";
                data = "";
            }

            if (command.Equals(GameParams.JOIN_FAIL))
            {
                EventController.Instance.OnJoiningGameFailed(new EventArgs());
            }
            else if (command.Equals(GameParams.JOIN_NEW_PLAYER) || command.Equals(GameParams.EXIT_PLAYER))
            {
                StringEventArgs eventArgs = new StringEventArgs();
                eventArgs.param = data;

                EventController.Instance.OnPlayerUpdated(eventArgs);
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
