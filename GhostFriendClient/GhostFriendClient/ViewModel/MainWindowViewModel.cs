using GhostFriendClient.Common;
using GhostFriendClient.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GhostFriendClient.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Proeprties
        private string player1Name;
        public string Player1Name {
            get { return player1Name; }
            set
            {
                player1Name = value;
                NotifyPropertyChanged("Player1Name");
            }
        }

        private string player2Name;
        public string Player2Name
        {
            get { return player2Name; }
            set
            {
                player2Name = value;
                NotifyPropertyChanged("Player2Name");
            }
        }

        private string player3Name;
        public string Player3Name
        {
            get { return player3Name; }
            set
            {
                player3Name = value;
                NotifyPropertyChanged("Player3Name");
            }
        }

        private string player4Name;
        public string Player4Name
        {
            get { return player4Name; }
            set
            {
                player4Name = value;
                NotifyPropertyChanged("Player4Name");
            }
        }

        private string player5Name;
        public string Player5Name
        {
            get { return player5Name; }
            set
            {
                player5Name = value;
                NotifyPropertyChanged("Player5Name");
            }
        }

        private string playerName;
        public string PlayerName
        {
            get { return playerName; }
            set
            {
                playerName = value;
                NotifyPropertyChanged("PlayerName");
            }
        }

        private string messageAnnounced;
        public string MessageAnnounced
        {
            get { return messageAnnounced; }
            set
            {
                messageAnnounced = value;
                NotifyPropertyChanged("MessageAnnounced");
            }
        }
        #endregion

        private string cardValue;
        public string CardValue
        {
            get { return cardValue; }
            set
            {
                cardValue = value;
                NotifyPropertyChanged("CardValue");
            }
        }

        private ICommand joinGameCommand;
        public ICommand JoinGameCommand
        {
            get { return (this.joinGameCommand) ?? (this.joinGameCommand = new DelegateCommand(() => ThreadPool.QueueUserWorkItem(JoinGame))); }
        }

        private void JoinGame(object state)
        {
            CardValue = "3";

            SocketClient.Instance.StartConnection();
            GameControl.Instance.Join(PlayerName);

            String joinResult = SocketClient.Instance.ReceiveData();

            if (joinResult.Equals(GameParams.JOIN_SUCCESS))
            {
                WaitOtherPlayers();
            }
            else if (joinResult.Equals(GameParams.JOIN_FAIL))
            {
                AnnounceMessage("You cannot join the game");
            }            
        }

        private ICommand closeWindowCommand;
        public ICommand CloseWindowCommand
        {
            get { return (this.closeWindowCommand) ?? (this.closeWindowCommand = new DelegateCommand(CloseWindow)); }
        }

        private void CloseWindow()
        {
            SocketClient.Instance.CloseConnection(false);
        }

        private void WaitOtherPlayers()
        {
            String serverCommand = SocketClient.Instance.ReceiveData();

            while (!serverCommand.Equals(GameParams.ALL_PLAYERS_ENTERED))
            {
                if (serverCommand.Equals(GameParams.JOIN_NEW_PLAYER) || serverCommand.Equals(GameParams.EXIT_PLAYER)) {
                    String[] playersInfo = GameControl.Instance.ReceivePlayersInfo();

                    for (int i = 0; i < playersInfo.Length; i++)
                    {
                        SetPlayerName(i + 1, playersInfo[i]);
                    }
                }

                serverCommand = SocketClient.Instance.ReceiveData();
            }
        }

        private void SetPlayerName(int index, String name)
        {
            if (index == 1)
            {
                Player1Name = name;
            }
            else if (index == 2)
            {
                Player2Name = name;
            }
            else if (index == 3)
            {
                Player3Name = name;
            }
            else if (index == 4)
            {
                Player4Name = name;
            }
            else if (index == 5)
            {
                Player5Name = name;
            }
        }

        private void AnnounceMessage(string text)
        {
            MessageAnnounced = text;
        }

        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
