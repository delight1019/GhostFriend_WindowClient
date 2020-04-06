using GhostFriendClient.Common;
using GhostFriendClient.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

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

        private ObservableCollection<Card> cardList;
        public ObservableCollection<Card> CardList
        {
            get { return cardList; }
            set
            {
                cardList = value;
                NotifyPropertyChanged("CardList");
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
            GameControl.Instance.Start();
            GameControl.Instance.Join(PlayerName);            
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

        private void _JoiningGameFailedHandler(object sender, EventArgs e)
        {
            AnnounceMessage("You cannot join the game");
        }

        private void _PlayerUpdatedHandler(object sender, StringEventArgs e)
        {
            String[] playersInfo = e.param.Split(GameParams.DATA_DELIMITER);

            for (int i = 0; i < playersInfo.Length; i++)
            {
                SetPlayerName(i + 1, playersInfo[i]);
            }
        }

        private void _CardDistributedHandler(object sender, StringEventArgs e)
        {
            String[] cardInfoList = e.param.Split(GameParams.DATA_DELIMITER);

            foreach (String cardInfo in cardInfoList)
            {
                if (Card.IsValidCard(cardInfo))
                {
                    AddCard(cardInfo);
                }                
            }
        }

        private void AddCard(String cardInfo)
        {
            Card card = new Card(cardInfo);
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                CardList.Add(card);
            }
            ));            
        }

        public MainWindowViewModel()
        {
            CardList = new ObservableCollection<Card>();

            EventController.Instance.JoiningGameFailed += _JoiningGameFailedHandler;
            EventController.Instance.PlayerUpdated += _PlayerUpdatedHandler;
            EventController.Instance.CardDistributed += _CardDistributedHandler;
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
