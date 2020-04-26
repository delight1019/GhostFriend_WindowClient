using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostFriendClient.Model
{
    public class Player
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
            }
        }

        private CardValue cardValue;
        public CardValue CardValue
        {
            get { return cardValue; }
            set
            {
                cardValue = value;
            }
        }

        private int score;
        public int Score
        {
            get { return score; }
            set
            {
                score = value;
            }
        }

        public void SubmitCard(Card card)
        {
            this.CardSuit = card.CardSuit;
            this.CardValue = card.CardValue;
        }

        public Player(int index, String name)
        {
            this.Index = index;
            this.Name = name;
            this.CardSuit = CardSuit.INVALID;
            this.CardValue = CardValue.INVALID;
            this.Score = 0;
        }
    }
}
