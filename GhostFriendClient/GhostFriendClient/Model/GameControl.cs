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
    public enum GamePhase
    {
        JOIN_GAME = 0,
        DISTRIBUTE_CARD = 1,
        DECLARE_CONTRACT = 2,
        DECLARATOR_DISCARD_CARD = 3,
        CHANGE_GIRU = 4,
        SELECT_FRIEND = 5,
        PLAY_GAME = 6,
        END_GAME = 7,
        INVALID = -1
    }

    public class GameControl
    {
        public const int MAX_CONTRACT_SCORE = 21;

        private static GameControl instance;
        private String name;
        private Boolean isDeclarer;

        private GameControl()
        {
            name = "";
            isDeclarer = false;
        }

        static public String getGamePhase(GamePhase phase)
        {
            switch (phase)
            {
                case GamePhase.JOIN_GAME:
                    return "플레이어 모집중";
                case GamePhase.DISTRIBUTE_CARD:
                    return "카드 확인중";
                case GamePhase.DECLARE_CONTRACT:
                    return "공약 선언중";
                case GamePhase.DECLARATOR_DISCARD_CARD:
                    return "버릴 카드 선택중";
                case GamePhase.CHANGE_GIRU:
                    return "기루 선택중";
                case GamePhase.SELECT_FRIEND:
                    return "친구 선택중";
                case GamePhase.PLAY_GAME:
                    return "게임 진행중";
                case GamePhase.END_GAME:
                    return "게임 종료";
                default:
                    return "";
            }
        }
        static public GamePhase convertGamePhase(string phaseStatement)
        {
            switch (phaseStatement)
            {
                case "플레이어 모집중":
                    return GamePhase.JOIN_GAME;
                case "카드 확인중":
                    return GamePhase.DISTRIBUTE_CARD;
                case "공약 선언중":
                    return GamePhase.DECLARE_CONTRACT;
                case "버릴 카드 선택중":
                    return GamePhase.DECLARATOR_DISCARD_CARD;
                case "기루 선택중":
                    return GamePhase.CHANGE_GIRU;
                case "친구 선택중":
                    return GamePhase.SELECT_FRIEND;
                case "게임 진행중":
                    return GamePhase.PLAY_GAME;
                case "게임 종료":
                    return GamePhase.END_GAME;
                default:
                    return GamePhase.INVALID;
            }
        }

        public void Start()
        {
            ThreadPool.QueueUserWorkItem(ListenToServer);
        }

        public void Join(string playerName)
        {
            name = playerName;
            SendCommand(GameParams.JOIN_GAME, playerName);
        }
        public void ReplyDealMiss(Boolean reply)
        {
            SendCommand(GameParams.REPLY_DEAL_MISS, reply.ToString());
        }
        public void DelcareContract(Contract contract)
        {
            SendCommand(GameParams.DECLARE_CONTRACT, Card.getCardSuitString(contract.ContractSuit) + GameParams.DATA_DELIMITER + contract.Score.ToString());
        }
        public void PassContractDelceration()
        {
            SendCommand(GameParams.PASS_CONTRACT_DECLERATION);
        }
        public void DiscardCard(Card card)
        {            
            SendCommand(GameParams.DISCARD_CARD, card.GetString(GameParams.DATA_DELIMITER));
        }
        public void SetDeclarar(String name)
        {
            if (this.name == name)
            {
                isDeclarer = true;
            }
        }
        public Boolean IsDeclarer()
        {
            return isDeclarer;
        }
        public void PassGiruChange()
        {
            SendCommand(GameParams.PASS_GIRU_CHANGE);
        }
        public void ChangeGiru(CardSuit giru)
        {
            SendCommand(GameParams.CHANGE_GIRU, Card.getCardSuitString(giru));
        }
        public void DetermineFriend(Card card)
        {
            SendCommand(GameParams.DETERMINE_FRIEND, card.GetString(GameParams.DATA_DELIMITER));
        }
        public void SubmitCard(Card card)
        {
            SendCommand(GameParams.SUBMIT_CARD, card.GetString(GameParams.DATA_DELIMITER));
        }

        private void SendCommand(string command, string data)
        {
            SocketClient.Instance.SendData(command + GameParams.COMMAND_DATA_DELIMITER + data + GameParams.COMMAND_DELIMITER);
        }
        private void SendCommand(string command)
        {
            SocketClient.Instance.SendData(command + GameParams.COMMAND_DELIMITER);
        }
        private void ListenToServer(object state)
        {
            while (SocketClient.Instance.IsConnected)
            {
                String serverCommand = SocketClient.Instance.ReceiveData();
                String[] commandList = serverCommand.Split(GameParams.COMMAND_DELIMITER);

                foreach (string command in commandList)
                {
                    HandleCommand(command);
                }
            }
        }
        private void HandleCommand(String inputCommand)
        {
            String[] commandStructure = inputCommand.Split(GameParams.COMMAND_DATA_DELIMITER);
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
            else if (command.Equals(GameParams.PHASE_CHANGE))
            {
                IntegerEventArgs eventArgs = new IntegerEventArgs();
                eventArgs.param = int.Parse(data);

                EventController.Instance.OnPhaseChanged(eventArgs);
            }
            else if (command.Equals(GameParams.JOIN_NEW_PLAYER) || command.Equals(GameParams.EXIT_PLAYER))
            {
                StringEventArgs eventArgs = new StringEventArgs();
                eventArgs.param = data;

                EventController.Instance.OnPlayerUpdated(eventArgs);
            }
            else if (command.Equals(GameParams.DISTRIBUTE_CARDS))
            {
                StringEventArgs eventArgs = new StringEventArgs();
                eventArgs.param = data;

                EventController.Instance.OnCardDistributed(eventArgs);
            }
            else if (command.Equals(GameParams.CHECK_DEAL_MISS))
            {
                BoolEventArgs eventArgs = new BoolEventArgs();
                eventArgs.param = Convert.ToBoolean(data);

                EventController.Instance.OnDealMissChecking(eventArgs);
            }
            else if (command.Equals(GameParams.RESTART_GAME))
            {
                EventController.Instance.OnGameRestarted(new EventArgs());
            }
            else if (command.Equals(GameParams.ASK_CONTRACT))
            {
                StringEventArgs eventArgs = new StringEventArgs();
                eventArgs.param = data;

                EventController.Instance.OnContractAsked(eventArgs);
            }
            else if (command.Equals(GameParams.OTHER_PLAYER_ASKING_CONTRACT))
            {
                StringEventArgs eventArgs = new StringEventArgs();
                eventArgs.param = data;

                EventController.Instance.OnOtherPlayerDeclaringContract(eventArgs);
            }
            else if (command.Equals(GameParams.CASTER_DECLARED))
            {
                StringEventArgs eventArgs = new StringEventArgs();
                eventArgs.param = data;

                EventController.Instance.OnCasterDeclared(eventArgs);
            }
            else if (command.Equals(GameParams.START_DECLARER_CARD_SELECTION))
            {
                EventController.Instance.OnDeclarerCardSelectionStarted(new EventArgs());
            }
            else if (command.Equals(GameParams.SELECT_CARDS_TO_DISCARD))
            {
                StringEventArgs eventArgs = new StringEventArgs();
                eventArgs.param = data;

                EventController.Instance.OnCardSelectionAsked(eventArgs);
            }
            else if (command.Equals(GameParams.ASK_GIRU_CHANGE))
            {             
                EventController.Instance.OnGiruChangeAsked(new EventArgs());
            }
            else if (command.Equals(GameParams.CONFIRM_CONTRACT))
            {
                StringEventArgs eventArgs = new StringEventArgs
                {
                    param = data
                };

                EventController.Instance.OnContractConfirmed(eventArgs);
            }
            else if (command.Equals(GameParams.ASK_FRIEND_CARD))
            {
                StringEventArgs eventArgs = new StringEventArgs
                {
                    param = data
                };

                EventController.Instance.OnFriendCardAsked(eventArgs);
            }
            else if (command.Equals(GameParams.CONFIRM_FRIEND))
            {
                StringEventArgs eventArgs = new StringEventArgs
                {
                    param = data
                };

                EventController.Instance.OnFriendConfirmed(eventArgs);
            }
            else if (command.Equals(GameParams.NOTIFY_FRIEND))
            {
                EventController.Instance.OnFriendNotified(new EventArgs());
            }
            else if (command.Equals(GameParams.START_PLAYING))
            {
                EventController.Instance.OnGameStarted(new EventArgs());
            }
            else if (command.Equals(GameParams.ASK_CARD))
            {
                EventController.Instance.OnCardAsked(new EventArgs());
            }
            else if (command.Equals(GameParams.NOTIFY_CARD_SUBMISSION))
            {
                StringEventArgs eventArgs = new StringEventArgs
                {
                    param = data
                };

                EventController.Instance.OnCardSubmissionNotified(eventArgs);
            }
            else if (command.Equals(GameParams.NOTIFY_PHASE_WINNER))
            {
                StringEventArgs eventArgs = new StringEventArgs
                {
                    param = data
                };

                EventController.Instance.OnPhaseWinnerNotified(eventArgs);
            }
            else if (command.Equals(GameParams.UPDATE_CARD_LIST))
            {
                StringEventArgs eventArgs = new StringEventArgs
                {
                    param = data
                };

                EventController.Instance.OnCardListUpdated(eventArgs);
            }
            else if (command.Equals(GameParams.NOTIFY_GAME_WINNER))
            {
                StringEventArgs eventArgs = new StringEventArgs
                {
                    param = data
                };

                EventController.Instance.OnGameWinnerNotified(eventArgs);
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
