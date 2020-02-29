using GhostFriendClient.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GhostFriendClient.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
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

        private ICommand joinGameCommand;
        public ICommand JoinGameCommand
        {
            get { return (this.joinGameCommand) ?? (this.joinGameCommand = new DelegateCommand(JoinGame)); }
        }

        private void JoinGame()
        {
            SocketClient.Instance.StartConnection();
            GameControl.Instance.Join(PlayerName);
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
