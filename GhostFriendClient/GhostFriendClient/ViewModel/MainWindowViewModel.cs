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
        DECLARE_CONTRACT
    }

    class MainWindowViewModel : INotifyPropertyChanged
    {      
        #region Proeprties       
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

        private Boolean declareContractVisible;
        public Boolean DeclareContractVisible
        {
            get { return declareContractVisible; }
            set
            {
                declareContractVisible = value;
                NotifyPropertyChanged("DeclareContractVisible");
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

        private Contract selectedContractScore;
        public Contract SelectedContractScore
        {
            get { return selectedContractScore; }
            set
            {
                selectedContractScore = value;
                NotifyPropertyChanged("SelectedContractScore");
            }
        }

        private Contract selectedContractSuit;
        public Contract SelectedContractSuit
        {
            get { return selectedContractSuit; }
            set
            {
                selectedContractSuit = value;
                NotifyPropertyChanged("SelectedContractSuit");
            }
        }

        private GamePhase gamePhase;
        public GamePhase GamePhase
        {
            get { return gamePhase; }
            set
            {
                gamePhase = value;
                NotifyPropertyChanged("GamePhase");
            }
        }
        #endregion

        public ObservableCollection<Player> PlayerList
        {
            get; set;
        }
        public ObservableCollection<Card> CardList
        {
            get; set;
        }
        public ObservableCollection<Contract> ContractScoreList
        {
            get; set;
        }
        public ObservableCollection<Contract> ContractSuitList
        {
            get; set;
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
        
        private ICommand declareContractCommand;
        public ICommand DeclareContractCommand
        {
            get { return (this.declareContractCommand) ?? (this.declareContractCommand = new DelegateCommand(() => ThreadPool.QueueUserWorkItem(DeclareContract))); }
        }
        private void DeclareContract(object state)
        {
            Contract declaredContract = new Contract(SelectedContractSuit.ContractSuit, selectedContractScore.Score);
            GameControl.Instance.DelcareContract(declaredContract);
        }        

        private ICommand passContractDeclerationCommand;
        public ICommand PassContractDeclerationCommand
        {
            get { return (this.passContractDeclerationCommand) ?? (this.passContractDeclerationCommand = new DelegateCommand(() => ThreadPool.QueueUserWorkItem(PassContractDecleration))); }
        }
        private void PassContractDecleration(object state)
        {
            GameControl.Instance.PassContractDelceration();
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

        private void SetMainGridStatus(MainGridStatus gridStatus)
        {
            switch (gridStatus)
            {
                case MainGridStatus.INVISIBLE:
                    {
                        JoinGameVisible = false;
                        DeclareDealMissVisible = false;
                        DeclareContractVisible = false;
                        break;
                    }
                case MainGridStatus.JOIN_GAME:
                    {
                        JoinGameVisible = true;
                        DeclareDealMissVisible = false;
                        DeclareContractVisible = false;
                        break;
                    }
                case MainGridStatus.DECLARE_DEAL_MISS:
                    {
                        JoinGameVisible = false;
                        DeclareDealMissVisible = true;
                        DeclareContractVisible = false;
                        break;
                    }
                case MainGridStatus.DECLARE_CONTRACT:
                    {
                        JoinGameVisible = false;
                        DeclareDealMissVisible = false;
                        DeclareContractVisible = true;
                        break;
                    }
            }
        }
        private void AnnounceMessage(string text)
        {
            MessageAnnounced = text;
        }
        private void SetGamePhase(GamePhase phase)
        {
            GamePhase = phase;
        }

        private void _JoiningGameFailedHandler(object sender, EventArgs e)
        {
            AnnounceMessage("You cannot join the game");
        }
        private void _PlayerUpdatedHandler(object sender, StringEventArgs e)
        {
            SetGamePhase(GamePhase.JOIN);

            String[] playersInfo = e.param.Split(GameParams.DATA_DELIMITER);

            ClearPlayerList();

            for (int i = 0; i < playersInfo.Length; i++)
            {
                if (playersInfo[i] != "")
                {
                    AddPlayer(i + 1, playersInfo[i]);
                }                
            }            
        }
        private void _CardDistributedHandler(object sender, StringEventArgs e)
        {
            SetGamePhase(GamePhase.CARD_DISTRIBUTION);

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
            SetGamePhase(GamePhase.DEAL_MISS_CHECK);

            if (e.param)
            {
                SetMainGridStatus(MainGridStatus.DECLARE_DEAL_MISS);
            } else
            {
                GameControl.Instance.ReplyDealMiss(false);
            }
        }
        private void _ContractAskedHandler(object sender, StringEventArgs e)
        {
            SetGamePhase(GamePhase.CONTRACT_DECLARATION);

            String[] contractInfo = e.param.Split(GameParams.DATA_DELIMITER);
            String messageToAnnounce = contractInfo[0] + "\n" +
                                        "최소 점수는 " + contractInfo[1] + "입니다.";

            AnnounceMessage(messageToAnnounce);

            SetContractSuitList();
            SetContractScoreList(Convert.ToInt32(contractInfo[1]));

            SetMainGridStatus(MainGridStatus.DECLARE_CONTRACT);
        }
        private void _OtherPlayerDeclaringContractHandler(object sender, StringEventArgs e)
        {
            SetGamePhase(GamePhase.CONTRACT_DECLARATION);

            String currentDeclarer = e.param;

            AnnounceMessage(currentDeclarer + "님이(가) 공약 선언 중입니다.");
            SetMainGridStatus(MainGridStatus.INVISIBLE);
        }
        private void _CasterDeclaredEventHandler(object sender, StringEventArgs e)
        {
            String[] contractInfo = e.param.Split(GameParams.DATA_DELIMITER);
            String messageToAnnounce = "공약이 선언되었습니다.\n"
                                        + "기루는 " + contractInfo[0] + ", " + "목표 점수는 " + contractInfo[1] + "입니다.";

            AnnounceMessage(messageToAnnounce);
            SetMainGridStatus(MainGridStatus.INVISIBLE);
        }
        private void _DeclarerCardSelectionStartedEventHandler(object sender, EventArgs e)
        {
            AnnounceMessage("주공이 버릴 카드를 선택중입니다.");
            SetMainGridStatus(MainGridStatus.INVISIBLE);
        }
        private void _CardSelectionAsked(object sender, StringEventArgs e)
        {
            AnnounceMessage("버릴 카드를 3장 선택하세요.");
            
            ClearCardList();

            String[] cardInfoList = e.param.Split(GameParams.DATA_DELIMITER);

            foreach (String cardInfo in cardInfoList)
            {
                if (Card.IsValidCard(cardInfo))
                {
                    AddCard(cardInfo);
                }
            }
        }
            
        private void AddPlayer(int index, String name)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
            {
                PlayerList.Add(new Player(index, name));
            }));
        }
        private void ClearPlayerList()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
            {
                PlayerList.Clear();
            }
            ));
        }
        private void SetContractScoreList(int minScore)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
            {
                ContractScoreList.Clear();

                for (int i = minScore; i <= GameControl.MAX_CONTRACT_SCORE; i++)
                {
                    ContractScoreList.Add(new Contract(CardSuit.INVALID, i));
                }
            }
            ));
        }
        private void SetContractSuitList()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
            {
                ContractSuitList.Clear();

                ContractSuitList.Add(new Contract(CardSuit.DIAMOND, -1));
                ContractSuitList.Add(new Contract(CardSuit.HEART, -1));
                ContractSuitList.Add(new Contract(CardSuit.SPADE, -1));
                ContractSuitList.Add(new Contract(CardSuit.CLUB, -1));
            }
            ));
        }
        private void AddCard(String cardData)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
            {
                CardList.Add(new Card(cardData));
            }
            ));
        }
        private void ClearCardList()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
            {
                CardList.Clear();
            }
            ));
        }

        public MainWindowViewModel()
        {
            PlayerList = new ObservableCollection<Player>();
            CardList = new ObservableCollection<Card>();
            ContractScoreList = new ObservableCollection<Contract>();
            ContractSuitList = new ObservableCollection<Contract>();

            EventController.Instance.JoiningGameFailed += _JoiningGameFailedHandler;
            EventController.Instance.PlayerUpdated += _PlayerUpdatedHandler;
            EventController.Instance.CardDistributed += _CardDistributedHandler;
            EventController.Instance.DealMissChecking += _DealMissCheckingHandler;
            EventController.Instance.GameRestarted += _GameRestartedHandler;
            EventController.Instance.ContractAsked += _ContractAskedHandler;
            EventController.Instance.OtherPlayerDeclaringContract += _OtherPlayerDeclaringContractHandler;
            EventController.Instance.CasterDeclared += _CasterDeclaredEventHandler;
            EventController.Instance.DeclarerCardSelectionStarted += _DeclarerCardSelectionStartedEventHandler;
            EventController.Instance.CardSelectionAsked += _CardSelectionAsked;

            SetMainGridStatus(MainGridStatus.JOIN_GAME);
            //SetContractSuitList();
            //SetContractScoreList(13);
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
