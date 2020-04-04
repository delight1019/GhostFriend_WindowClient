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

        private CardSuit convertCardSuit(String value)
        {
            switch (value)
            {
                case "DIAMOND":
                    return CardSuit.DIAMOND;
                case "SPADE":
                    return CardSuit.SPADE;
                case "CLUB":
                    return CardSuit.CLUB;
                case "HEART":
                    return CardSuit.HEART;
                case "JOKER":
                    return CardSuit.JOKER;
                default:
                    return CardSuit.JOKER;
            }

        }
        private CardValue convertCardValue(String value)
        {
            switch (value)
            {
                case "ACE":
                    return CardValue.ACE;
                case "TWO":
                    return CardValue.TWO;
                case "THREE":
                    return CardValue.THREE;
                case "FOUR":
                    return CardValue.FOUR;
                case "FIVE":
                    return CardValue.FIVE;
                case "SIX":
                    return CardValue.SIX;
                case "SEVEN":
                    return CardValue.SEVEN;
                case "EIGHT":
                    return CardValue.EIGHT;
                case "NINE":
                    return CardValue.NINE;
                case "TEN":
                    return CardValue.TEN;
                case "JACK":
                    return CardValue.JACK;
                case "QUEEN":
                    return CardValue.QUEEN;
                case "KING":
                    return CardValue.KING;
                case "JOKER":
                    return CardValue.JOKER;
                default:
                    return CardValue.JOKER;
            }
        }

        public Card(String cardData)
        {
            String[] cardInfo = cardData.Split(' ');

            this.suit = convertCardSuit(cardInfo[0]);
            this.value = convertCardValue(cardInfo[1]);
        }

        public Card(CardSuit suit, CardValue value)
        {
            this.suit = suit;
            this.value = value;
        }
    }
}
