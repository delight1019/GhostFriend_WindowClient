using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostFriendClient.Model
{
    public class Player : INotifyPropertyChanged
    {
        private int index;
        public int Index
        {
            get { return index; }
            set
            {
                index = value;
            }
        }

        private String name;
        public String Name
        {
            get { return name; }
            set
            {
                name = value;
            }
        }

        private CardSuit cardSuit;
        public CardSuit CardSuit
        {
            get { return cardSuit; }
            set
            {
                cardSuit = value;
                NotifyPropertyChanged("CardSuit");
            }
        }

        private CardValue cardValue;
        public CardValue CardValue
        {
            get { return cardValue; }
            set
            {
                cardValue = value;
                NotifyPropertyChanged("CardValue");
            }
        }

        private int score;        
        public int Score
        {
            get { return score; }
            set
            {
                score = value;
                NotifyPropertyChanged("Score");
            }
        }

        public void SubmitCard(Card card)
        {
            this.CardSuit = card.CardSuit;
            this.CardValue = card.CardValue;
        }
        public void ClearSubmittedCard()
        {
            this.CardSuit = CardSuit.INVALID;
            this.CardValue = CardValue.INVALID;
        }

        public Player(int index, String name)
        {
            this.Index = index;
            this.Name = name;
            this.CardSuit = CardSuit.INVALID;
            this.CardValue = CardValue.INVALID;
            this.Score = 0;
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
