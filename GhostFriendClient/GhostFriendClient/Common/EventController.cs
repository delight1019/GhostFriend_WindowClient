using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GhostFriendClient.Common
{
    public class StringEventArgs : EventArgs
    {
        public String param { get; set; }
    }

    public class BoolEventArgs: EventArgs
    {
        public bool param { get; set; }
    }

    class EventController
    {
        private static EventController instance;
        public static EventController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EventController();
                }

                return instance;
            }
        }

        public event EventHandler JoiningGameFailed;
        public void OnJoiningGameFailed(EventArgs e)
        {
            EventHandler handler = JoiningGameFailed;
            handler?.Invoke(this, e);
        }

        public event EventHandler<StringEventArgs> PlayerUpdated;
        public void OnPlayerUpdated(StringEventArgs e)
        {
            EventHandler<StringEventArgs> handler = PlayerUpdated;
            handler?.Invoke(this, e);
        }

        public event EventHandler<StringEventArgs> CardDistributed;
        public void OnCardDistributed(StringEventArgs e)
        {
            EventHandler<StringEventArgs> handler = CardDistributed;
            handler?.Invoke(this, e);
        }

        public event EventHandler<BoolEventArgs> DealMissChecking;
        public void OnDealMissChecking(BoolEventArgs e)
        {
            EventHandler<BoolEventArgs> handler = DealMissChecking;
            handler?.Invoke(this, e);
        }

        public event EventHandler GameRestarted;
        public void OnGameRestarted(EventArgs e)
        {
            EventHandler handler = GameRestarted;
            handler?.Invoke(this, e);
        }

        public event EventHandler<StringEventArgs> ContractAsked;
        public void OnContractAsked(StringEventArgs e)
        {
            EventHandler<StringEventArgs> handler = ContractAsked;
            handler?.Invoke(this, e);
        }

        public event EventHandler<StringEventArgs> OtherPlayerDeclaringContract;
        public void OnOtherPlayerDeclaringContract(StringEventArgs e)
        {
            EventHandler<StringEventArgs> handler = OtherPlayerDeclaringContract;
            handler?.Invoke(this, e);
        }

        public event EventHandler<StringEventArgs> CasterDeclared;
        public void OnCasterDeclared(StringEventArgs e)
        {
            EventHandler<StringEventArgs> handler = CasterDeclared;
            handler?.Invoke(this, e);
        }

        public event EventHandler DeclarerCardSelectionStarted;
        public void OnDeclarerCardSelectionStarted(EventArgs e)
        {
            EventHandler handler = DeclarerCardSelectionStarted;
            handler?.Invoke(this, e);
        }

        public event EventHandler<StringEventArgs> CardSelectionAsked;
        public void OnCardSelectionAsked(StringEventArgs e)
        {
            EventHandler<StringEventArgs> handler = CardSelectionAsked;
            handler?.Invoke(this, e);
        }

        public event EventHandler<EventArgs> GiruChangeAsked;
        public void OnGiruChangeAsked(EventArgs e)
        {
            EventHandler<EventArgs> handler = GiruChangeAsked;
            handler?.Invoke(this, e);
        }

        public event EventHandler<StringEventArgs> ContractConfirmed;
        public void OnContractConfirmed(StringEventArgs e)
        {
            EventHandler<StringEventArgs> handler = ContractConfirmed;
            handler?.Invoke(this, e);
        }

        public event EventHandler<StringEventArgs> FriendCardAsked;
        public void OnFriendCardAsked(StringEventArgs e)
        {
            EventHandler<StringEventArgs> handler = FriendCardAsked;
            handler?.Invoke(this, e);
        }

        public event EventHandler<StringEventArgs> FriendConfirmed;
        public void OnFriendConfirmed(StringEventArgs e)
        {
            EventHandler<StringEventArgs> handler = FriendConfirmed;
            handler?.Invoke(this, e);
        }

        public event EventHandler FriendNotified;
        public void OnFriendNotified(EventArgs e)
        {
            EventHandler handler = FriendNotified;
            handler?.Invoke(this, e);
        }

        public event EventHandler GameStarted;
        public void OnGameStarted(EventArgs e)
        {
            EventHandler handler = GameStarted;
            handler?.Invoke(this, e);
        }

        public event EventHandler CardAsked;
        public void OnCardAsked(EventArgs e)
        {
            EventHandler handler = CardAsked;
            handler?.Invoke(this, e);
        }

        public event EventHandler<StringEventArgs> CardSubmissionNotified;
        public void OnCardSubmissionNotified(StringEventArgs e)
        {
            EventHandler<StringEventArgs> handler = CardSubmissionNotified;
            handler?.Invoke(this, e);
        }

        public event EventHandler<StringEventArgs> PhaseWinnerNotified;
        public void OnPhaseWinnerNotified(StringEventArgs e)
        {
            EventHandler<StringEventArgs> handler = PhaseWinnerNotified;
            handler?.Invoke(this, e);
        }

        public event EventHandler<StringEventArgs> CardListUpdated;
        public void OnCardListUpdated(StringEventArgs e)
        {
            EventHandler<StringEventArgs> handler = CardListUpdated;
            handler?.Invoke(this, e);
        }

        public event EventHandler<StringEventArgs> GameWinnerNotified;
        public void OnGameWinnerNotified(StringEventArgs e)
        {
            EventHandler<StringEventArgs> handler = GameWinnerNotified;
            handler?.Invoke(this, e);
        }
    }
}