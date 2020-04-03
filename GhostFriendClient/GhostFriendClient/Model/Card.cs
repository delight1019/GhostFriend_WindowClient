using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostFriendClient.Model
{
    public enum CardSuit
    {
        DIAMOND,
        SPADE,
        CLUB,
        HEART,
        JOKER
    }
    public enum CardValue
    {
        ACE,
        TWO,
        THREE,
        FOUR,
        FIVE,
        SIX,
        SEVEN,
        EIGHT,
        NINE,
        TEN,
        JACK,
        QUEEN,
        KING,
        JOKER
    }

    public class Card
    {
        private CardSuit suit;
        private CardValue value;        

        public Card(String cardData)
        {
            String[] cardInfo = cardData.Split(' ');

        }

        public Card(CardSuit suit, CardValue value)
        {
            this.suit = suit;
            this.value = value;
        }
    }
}
