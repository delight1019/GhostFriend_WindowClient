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
        JOKER,
        INVALID
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
        JOKER,
        INVALID
    }

    public class Card
    {
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

        static public CardSuit ConvertCardSuit(String value)
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
                    return CardSuit.INVALID;
            }

        }
        static public CardValue ConvertCardValue(String value)
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
                    return CardValue.INVALID;
            }
        }
        static public bool IsValidCard(String cardData)
        {
            String[] cardInfo = cardData.Split(' ');

            if ((cardInfo.Length == 1) && (ConvertCardSuit(cardInfo[0]) == CardSuit.JOKER))
            {
                return true;
            }
            else if ((cardInfo.Length == 2) &&
                    (ConvertCardSuit(cardInfo[0]) != CardSuit.INVALID) && (ConvertCardValue(cardInfo[1]) != CardValue.INVALID))
            {
                return true;
            } else
            {
                return false;
            }
        }

        public Card(String cardData)
        {
            String[] cardInfo = cardData.Split(' ');

            this.cardSuit = ConvertCardSuit(cardInfo[0]);
            this.cardValue = ConvertCardValue(cardInfo[1]);
        }

        public Card(CardSuit suit, CardValue value)
        {
            this.cardSuit = suit;
            this.cardValue = value;
        }
    }
}
