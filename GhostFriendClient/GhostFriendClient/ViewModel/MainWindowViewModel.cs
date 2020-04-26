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
        DECLARE_CONTRACT,
        SELECT_CARD,
        CHANGE_GIRU,
        SELECT_FRIEND,
        PLAY_GAME
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

        private Boolean selectCardVisible;
        public Boolean SelectCardVisible
        {
            get { return selectCardVisible; }
            set
            {
                selectCardVisible = value;
                NotifyPropertyChanged("SelectCardVisible");
            }
        }

        private Boolean giruChangeVisible;
        public Boolean GiruChangeVisible
        {
            get { return giruChangeVisible; }
            set
            {
                giruChangeVisible = value;
                NotifyPropertyChanged("GiruChangeVisible");
            }
        }

        private Boolean friendSelectionVisible;
        public Boolean FriendSelectionVisible
        {
            get { return friendSelectionVisible; }
            set
            {
                friendSelectionVisible = value;
                NotifyPropertyChanged("FriendSelectionVisible");
            }
        }

        private Boolean gameBoardVisible;
        public Boolean GameBoardVisible
        {
            get { return gameBoardVisible; }
            set
            {
                gameBoardVisible = value;
                NotifyPropertyChanged("GameBoardVisible");
            }
        }

        private String submitButtonVisible;
        public String SubmitButtonVisible
        {
            get { return submitButtonVisible; }
            set
            {
                submitButtonVisible = value;
                NotifyPropertyChanged("SubmitButtonVisible");
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

        private Contract currentContract;
        public Contract CurrentContract
        {
            get { return currentContract; }
            set
            {
                currentContract = value;
                NotifyPropertyChanged("CurrentContract");
            }
        }

        private Card selectedCard;
        public Card SelectedCard
        {
            get { return selectedCard; }
            set
            {
                selectedCard = value;
                NotifyPropertyChanged("SelectedCard");
            }
        }

        private Contract selectedGiru;
        public Contract SelectedGiru
        {
            get { return selectedGiru; }
            set
            {
                selectedGiru = value;
                NotifyPropertyChanged("SelectedGiru");
            }
        }

        private Card selectedFriendCardSuit;
        public Card SelectedFriendCardSuit
        {
            get { return selectedFriendCardSuit; }
            set
            {
                selectedFriendCardSuit = value;
                NotifyPropertyChanged("SelectedFriendCardSuit");
            }
        }

        private Card selectedFriendCardValue;
        public Card SelectedFriendCardValue
        {
            get { return selectedFriendCardValue; }
            set
            {
                selectedFriendCardValue = value;
                NotifyPropertyChanged("SelectedFriendCardValue");
            }
        }

        private Card friendCard;
        public Card FriendCard
        {
            get { return friendCard; }
            set
            {
                friendCard = value;
                NotifyPropertyChanged("FriendCard");
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
        public ObservableCollection<Card> FriendCardSuitList
        {
            get; set;
        }
        public ObservableCollection<Card> FriendCardValueList
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

        private ICommand discardCardCommand;
        public ICommand DiscardCardCommand
        {
            get { return (this.discardCardCommand) ?? (this.discardCardCommand = new DelegateCommand(() => ThreadPool.QueueUserWorkItem(DiscardCard))); }
        }
        private void DiscardCard(object state)
        {
            GameControl.Instance.DiscardCard(SelectedCard);
        }

        private ICommand passGiruChangeCommand;
        public ICommand PassGiruChangeCommand
        {
            get { return (this.passGiruChangeCommand) ?? (this.passGiruChangeCommand = new DelegateCommand(() => ThreadPool.QueueUserWorkItem(PassGiruChange))); }
        }
        private void PassGiruChange(object state)
        {
            GameControl.Instance.PassGiruChange();
        }

        private ICommand changeGiruCommand;
        public ICommand ChangeGiruCommand
        {
            get { return (this.changeGiruCommand) ?? (this.changeGiruCommand = new DelegateCommand(() => ThreadPool.QueueUserWorkItem(ChangeGiru))); }
        }
        private void ChangeGiru(object state)
        {
            if (SelectedGiru != null)
            {
                GameControl.Instance.ChangeGiru(SelectedGiru.ContractSuit);
            }            
        }

        private ICommand determineFriendCommand;
        public ICommand DetermineFriendCommand
        {
            get { return (this.determineFriendCommand) ?? (this.determineFriendCommand = new DelegateCommand(() => ThreadPool.QueueUserWorkItem(DetermineFriend))); }
        }
        private void DetermineFriend(object state)
        {
            if ((SelectedFriendCardSuit != null) && (SelectedFriendCardValue != null))
            {
                GameControl.Instance.DetermineFriend(new Card(SelectedFriendCardSuit.CardSuit, SelectedFriendCardValue.CardValue));
            }
        }

        private ICommand submitCardCommand;
        public ICommand SubmitCardCommand
        {
            get { return (this.submitCardCommand) ?? (this.submitCardCommand = new DelegateCommand(() => ThreadPool.QueueUserWorkItem(SubmitCard))); }
        }
        private void SubmitCard(object state)
        {
            if (SelectedCard != null)
            {
                GameControl.Instance.SubmitCard(SelectedCard);
                SetSubmitButtonVisible(false);
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

        private void SetMainGridStatus(MainGridStatus gridStatus)
        {
            switch (gridStatus)
            {
                case MainGridStatus.INVISIBLE:
                    {
                        JoinGameVisible = false;
                        DeclareDealMissVisible = false;
                        DeclareContractVisible = false;
                        SelectCardVisible = false;
                        GiruChangeVisible = false;
                        FriendSelectionVisible = false;
                        GameBoardVisible = false;
                        break;
                    }
                case MainGridStatus.JOIN_GAME:
                    {
                        JoinGameVisible = true;
                        DeclareDealMissVisible = false;
                        DeclareContractVisible = false;
                        SelectCardVisible = false;
                        GiruChangeVisible = false;
                        FriendSelectionVisible = false;
                        GameBoardVisible = false;
                        break;
                    }
                case MainGridStatus.DECLARE_DEAL_MISS:
                    {
                        JoinGameVisible = false;
                        DeclareDealMissVisible = true;
                        DeclareContractVisible = false;
                        SelectCardVisible = false;
                        GiruChangeVisible = false;
                        FriendSelectionVisible = false;
                        GameBoardVisible = false;
                        break;
                    }
                case MainGridStatus.DECLARE_CONTRACT:
                    {
                        JoinGameVisible = false;
                        DeclareDealMissVisible = false;
                        DeclareContractVisible = true;
                        SelectCardVisible = false;
                        GiruChangeVisible = false;
                        FriendSelectionVisible = false;
                        GameBoardVisible = false;
                        break;
                    }
                case MainGridStatus.SELECT_CARD:
                    {
                        JoinGameVisible = false;
                        DeclareDealMissVisible = false;
                        DeclareContractVisible = false;
                        SelectCardVisible = true;
                        GiruChangeVisible = false;
                        FriendSelectionVisible = false;
                        GameBoardVisible = false;
                        break;
                    }
                case MainGridStatus.CHANGE_GIRU:
                    {
                        JoinGameVisible = false;
                        DeclareDealMissVisible = false;
                        DeclareContractVisible = false;
                        SelectCardVisible = false;
                        GiruChangeVisible = true;
                        FriendSelectionVisible = false;
                        GameBoardVisible = false;
                        break;
                    }
                case MainGridStatus.SELECT_FRIEND:
                    {
                        JoinGameVisible = false;
                        DeclareDealMissVisible = false;
                        DeclareContractVisible = false;
                        SelectCardVisible = false;
                        GiruChangeVisible = false;
                        FriendSelectionVisible = true;
                        GameBoardVisible = false;
                        break;
                    }
                case MainGridStatus.PLAY_GAME:
                    {
                        JoinGameVisible = false;
                        DeclareDealMissVisible = false;
                        DeclareContractVisible = false;
                        SelectCardVisible = false;
                        GiruChangeVisible = false;
                        FriendSelectionVisible = false;
                        GameBoardVisible = true;
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

            int minScore = -1;

            if (contractInfo[0].Equals(GameParams.NO_CONTRACT))
            {
                SetCurrentContract(CardSuit.INVALID, -1);
                minScore = Convert.ToInt32(contractInfo[1]);
            } else
            {
                SetCurrentContract(Card.ConvertCardSuit(contractInfo[0]), Convert.ToInt32(contractInfo[1]));
                minScore = Convert.ToInt32(contractInfo[2]);
            }

            SetContractSuitList();
            SetContractScoreList(minScore);

            SetMainGridStatus(MainGridStatus.DECLARE_CONTRACT);
        }
        private void _OtherPlayerDeclaringContractHandler(object sender, StringEventArgs e)
        {
            SetGamePhase(GamePhase.CONTRACT_DECLARATION);            

            String[] contractInfo = e.param.Split(GameParams.DATA_DELIMITER);

            String currentDeclarer = "";
            int minScore = -1;

            if (contractInfo[0].Equals(GameParams.NO_CONTRACT))
            {
                SetCurrentContract(CardSuit.INVALID, -1);
                minScore = Convert.ToInt32(contractInfo[1]);
                currentDeclarer = contractInfo[2];
            }
            else
            {
                SetCurrentContract(Card.ConvertCardSuit(contractInfo[0]), Convert.ToInt32(contractInfo[1]));
                minScore = Convert.ToInt32(contractInfo[2]);
                currentDeclarer = contractInfo[3];
            }

            AnnounceMessage(currentDeclarer + "님이(가) 공약 선언 중입니다.");
            SetMainGridStatus(MainGridStatus.INVISIBLE);
        }
        private void _CasterDeclaredEventHandler(object sender, StringEventArgs e)
        {
            String[] contractInfo = e.param.Split(GameParams.DATA_DELIMITER);

            GameControl.Instance.SetDeclarar(contractInfo[0]);

            String messageToAnnounce = "공약이 선언되었습니다.\n"
                                        + "기루는 " + contractInfo[1] + ", " + "목표 점수는 " + contractInfo[2] + "입니다.";

            AnnounceMessage(messageToAnnounce);

            if (!GameControl.Instance.IsDeclarer())
            {
                SetMainGridStatus(MainGridStatus.INVISIBLE);
            }            
        }
        private void _DeclarerCardSelectionStartedEventHandler(object sender, EventArgs e)
        {
            AnnounceMessage("주공이 버릴 카드를 선택중입니다.");
            SetGamePhase(GamePhase.DISCARD_CARD);
            SetMainGridStatus(MainGridStatus.INVISIBLE);
        }
        private void _CardSelectionAskedHandler(object sender, StringEventArgs e)
        {
            AnnounceMessage("버릴 카드를 1장 선택하세요.");
            
            ClearCardList();

            String[] cardInfoList = e.param.Split(GameParams.DATA_DELIMITER);

            foreach (String cardInfo in cardInfoList)
            {
                if (Card.IsValidCard(cardInfo))
                {
                    AddCard(cardInfo);
                }
            }
            
            SetGamePhase(GamePhase.DISCARD_CARD);
            SetMainGridStatus(MainGridStatus.SELECT_CARD);            
        }
        private void _GiruChangeAskedHandler(object sender, StringEventArgs e)
        {
            SetContractSuitList();
            SetGamePhase(GamePhase.CHANGE_GIRU);
            SetMainGridStatus(MainGridStatus.CHANGE_GIRU);
        }
        private void _ContractConfirmedHandler(object sender, StringEventArgs e)
        {
            String[] contractInfo = e.param.Split(GameParams.DATA_DELIMITER);

            SetCurrentContract(Card.ConvertCardSuit(contractInfo[0]), Convert.ToInt32(contractInfo[1]));
        }
        private void _FriendCardAsekdHandler(object sender, StringEventArgs e)
        {
            String[] currentRuleInfo = e.param.Split(GameParams.DATA_DELIMITER);

            SetFriendCardSuitList();
            SetFriendCardValueList();

            SetGamePhase(GamePhase.FRIEND_SELECTION);
            SetMainGridStatus(MainGridStatus.SELECT_FRIEND);
        }
        private void _FriendConfirmedHandler(object sender, StringEventArgs e)
        {
            String[] cardInfo = e.param.Split(' ');

            FriendCard = new Card(Card.ConvertCardSuit(cardInfo[0]), Card.ConvertCardValue(cardInfo[1]));            
        }
        private void _FriendNotifiedHandler(object sender, EventArgs e)
        {
            AnnounceMessage("친구로 선언되었습니다.");
        }
        private void _GameStartedHandler(object sender, EventArgs e)
        {
            AnnounceMessage("게임을 시작합니다.");

            SetGamePhase(GamePhase.PLAY_GAME);
            SetMainGridStatus(MainGridStatus.PLAY_GAME);
        }
        private void _CardAskedHandler(object sender, EventArgs e)
        {
            AnnounceMessage("제출할 카드를 선택하세요.");

            SetSubmitButtonVisible(true);
        }
        private void _CardSubmissionNotifiedHandler(object sender, StringEventArgs e)
        {
            String[] submissionInfo = e.param.Split(GameParams.DATA_DELIMITER);
            String[] cardInfo = submissionInfo[1].Split(' ');

            Card card = new Card(Card.ConvertCardSuit(cardInfo[0]), Card.ConvertCardValue(cardInfo[1]));

            SetPlayerSubmittedCard(submissionInfo[0], card);
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
        private void SetCurrentContract(CardSuit suit, int score)
        {
            CurrentContract = new Contract(suit, score);
        }        
        private void SetFriendCardSuitList()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
            {
                FriendCardSuitList.Clear();

                FriendCardSuitList.Add(new Card(CardSuit.CLUB, CardValue.ACE));
                FriendCardSuitList.Add(new Card(CardSuit.DIAMOND, CardValue.ACE));
                FriendCardSuitList.Add(new Card(CardSuit.SPADE, CardValue.ACE));
                FriendCardSuitList.Add(new Card(CardSuit.HEART, CardValue.ACE));
            }));
        }
        private void SetFriendCardValueList()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
            {
                FriendCardValueList.Clear();

                FriendCardValueList.Add(new Card(CardSuit.SPADE, CardValue.ACE));
                FriendCardValueList.Add(new Card(CardSuit.SPADE, CardValue.TWO));
                FriendCardValueList.Add(new Card(CardSuit.SPADE, CardValue.THREE));
                FriendCardValueList.Add(new Card(CardSuit.SPADE, CardValue.FOUR));
                FriendCardValueList.Add(new Card(CardSuit.SPADE, CardValue.FIVE));
                FriendCardValueList.Add(new Card(CardSuit.SPADE, CardValue.SIX));
                FriendCardValueList.Add(new Card(CardSuit.SPADE, CardValue.SEVEN));
                FriendCardValueList.Add(new Card(CardSuit.SPADE, CardValue.EIGHT));
                FriendCardValueList.Add(new Card(CardSuit.SPADE, CardValue.NINE));
                FriendCardValueList.Add(new Card(CardSuit.SPADE, CardValue.TEN));
                FriendCardValueList.Add(new Card(CardSuit.SPADE, CardValue.JACK));
                FriendCardValueList.Add(new Card(CardSuit.SPADE, CardValue.QUEEN));
                FriendCardValueList.Add(new Card(CardSuit.SPADE, CardValue.KING));
            }));
        }        
        private void SetSubmitButtonVisible(Boolean value)
        {
            if (value)
            {
                SubmitButtonVisible = "100";
            } else
            {
                SubmitButtonVisible = "0";
            }
        }
        private void SetPlayerSubmittedCard(String playerName, Card card)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
            {
                foreach (Player player in PlayerList)
                {
                    if (player.Name == playerName)
                    {
                        player.SubmitCard(card);
                        break;
                    }
                }
            }));            
        }

        public MainWindowViewModel()
        {
            PlayerList = new ObservableCollection<Player>();
            CardList = new ObservableCollection<Card>();
            ContractScoreList = new ObservableCollection<Contract>();
            ContractSuitList = new ObservableCollection<Contract>();
            FriendCardSuitList = new ObservableCollection<Card>();
            FriendCardValueList = new ObservableCollection<Card>();

            EventController.Instance.JoiningGameFailed += _JoiningGameFailedHandler;
            EventController.Instance.PlayerUpdated += _PlayerUpdatedHandler;
            EventController.Instance.CardDistributed += _CardDistributedHandler;
            EventController.Instance.DealMissChecking += _DealMissCheckingHandler;
            EventController.Instance.GameRestarted += _GameRestartedHandler;
            EventController.Instance.ContractAsked += _ContractAskedHandler;
            EventController.Instance.OtherPlayerDeclaringContract += _OtherPlayerDeclaringContractHandler;
            EventController.Instance.CasterDeclared += _CasterDeclaredEventHandler;
            EventController.Instance.DeclarerCardSelectionStarted += _DeclarerCardSelectionStartedEventHandler;
            EventController.Instance.CardSelectionAsked += _CardSelectionAskedHandler;
            EventController.Instance.GiruChangeAsked += _GiruChangeAskedHandler;
            EventController.Instance.ContractConfirmed += _ContractConfirmedHandler;
            EventController.Instance.FriendCardAsked += _FriendCardAsekdHandler;
            EventController.Instance.FriendConfirmed += _FriendConfirmedHandler;
            EventController.Instance.FriendNotified += _FriendNotifiedHandler;
            EventController.Instance.GameStarted += _GameStartedHandler;
            EventController.Instance.CardAsked += _CardAskedHandler;
            EventController.Instance.CardSubmissionNotified += _CardSubmissionNotifiedHandler;

            SetMainGridStatus(MainGridStatus.JOIN_GAME);
            SetSubmitButtonVisible(false);

            //SetMainGridStatus(MainGridStatus.SELECT_FRIEND);
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
