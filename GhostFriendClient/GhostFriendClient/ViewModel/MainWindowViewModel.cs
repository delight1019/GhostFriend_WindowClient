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
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace GhostFriendClient.ViewModel
{
    enum MainGridStatus
    {
        INVISIBLE,
        JOIN_GAME,
        DECLARE_DEAL_MISS,
        ASK_GIRU
    }

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

        private Boolean joinGameVisible;
        public Boolean JoinGameVisible
        {
            get { return joinGameVisible; }
            set
            {
                joinGameVisible = value;
                NotifyPropertyChanged("JoinGameVisible");
            }
        }

        private Boolean askGiruVisible;
        public Boolean AskGiruVisible
        {
            get { return askGiruVisible; }
            set
            {
                askGiruVisible = value;
                NotifyPropertyChanged("AskGiruVisible");
            }
        }

        private Boolean declareDealMissVisible;
        public Boolean DeclareDealMissVisible
        {
            get { return declareDealMissVisible; }
            set
            {
                declareDealMissVisible = value;
                NotifyPropertyChanged("DeclareDealMissVisible");
            }
        }
        #endregion

        public ObservableCollection<Card> CardList
        {
            get; set;
        }

        private void SetMainGridStatus(MainGridStatus gridStatus)
        {
            switch (gridStatus)
            {
                case MainGridStatus.INVISIBLE:
                    {
                        JoinGameVisible = false;
                        DeclareDealMissVisible = false;
                        AskGiruVisible = false;
                        break;
                    }
                case MainGridStatus.JOIN_GAME:
                    {
                        JoinGameVisible = true;
                        DeclareDealMissVisible = false;
                        AskGiruVisible = false;
                        break;
                    }
                case MainGridStatus.DECLARE_DEAL_MISS:
                    {
                        JoinGameVisible = false;
                        DeclareDealMissVisible = true;
                        AskGiruVisible = false;                        
                        break;
                    }
                case MainGridStatus.ASK_GIRU:
                    {
                        JoinGameVisible = false;
                        DeclareDealMissVisible = false;
                        AskGiruVisible = true;                        
                        break;
                    }
            }
        }

        private ICommand joinGameCommand;
        public ICommand JoinGameCommand
        {
            get { return (this.joinGameCommand) ?? (this.joinGameCommand = new DelegateCommand(() => ThreadPool.QueueUserWorkItem(JoinGame))); }
        }

        private void JoinGame(object state)
        {
            SocketClient.Instance.StartConnection();
            GameControl.Instance.Start();
            GameControl.Instance.Join(PlayerName);

            SetMainGridStatus(MainGridStatus.INVISIBLE);
        }

        private ICommand declareDealMissCommand;
        public ICommand DeclareDealMissCommand
        {
            get { return (this.declareDealMissCommand) ?? (this.declareDealMissCommand = new DelegateCommand(() => ThreadPool.QueueUserWorkItem(DeclareDealMiss))); }
        }

        private void DeclareDealMiss(object state)
        {
            GameControl.Instance.ReplyDealMiss(true);
        }

        private ICommand doNotDeclareDealMissCommand;
        public ICommand DoNotDeclareDealMissCommand
        {
            get { return (this.doNotDeclareDealMissCommand) ?? (this.doNotDeclareDealMissCommand = new DelegateCommand(() => ThreadPool.QueueUserWorkItem(DoNotDeclareDealMiss))); }
        }

        private void DoNotDeclareDealMiss(object state)
        {
            GameControl.Instance.ReplyDealMiss(false);
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
            AnnounceMessage("Distribute cards...");

            String[] cardInfoList = e.param.Split(GameParams.DATA_DELIMITER);

            foreach (String cardInfo in cardInfoList)
            {
                if (Card.IsValidCard(cardInfo))
                {
                    AddCard(cardInfo);
                }                
            }
        }

        private void _GameRestartedHandler(object sender, EventArgs e)
        {
            AnnounceMessage("Restart game...");

            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
            {
                CardList.Clear();
            }
            ));
        }

        private void _DealMissCheckingHandler(object sender, BoolEventArgs e)
        {
            AnnounceMessage("Checking DealMiss..");

            if (e.param)
            {
                SetMainGridStatus(MainGridStatus.DECLARE_DEAL_MISS);
            } else
            {
                GameControl.Instance.ReplyDealMiss(false);
            }
        }

        private void _GiruAskedHandler(object sender, StringEventArgs e)
        {
            String[] contractInfo = e.param.Split(GameParams.DATA_DELIMITER);
            //String messageToAnnounce = "현재 공약은 " + contractInfo[1] + "\n" +
                                        //"최소 점수는 " + contractInfo[0] + "입니다.";

            //AnnounceMessage(messageToAnnounce);
            
        }

        private void AddCard(String cardData)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
            {
                CardList.Add(new Card(cardData));
            }
            ));
        }

        public MainWindowViewModel()
        {
            CardList = new ObservableCollection<Card>();

            EventController.Instance.JoiningGameFailed += _JoiningGameFailedHandler;
            EventController.Instance.PlayerUpdated += _PlayerUpdatedHandler;
            EventController.Instance.CardDistributed += _CardDistributedHandler;
            EventController.Instance.DealMissChecking += _DealMissCheckingHandler;
            EventController.Instance.GameRestarted += _GameRestartedHandler;
            EventController.Instance.GiruAsked += _GiruAskedHandler;

            SetMainGridStatus(MainGridStatus.JOIN_GAME);
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
