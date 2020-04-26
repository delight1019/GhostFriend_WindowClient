using GhostFriendClient.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GhostFriendClient.Converters
{
    public class CardValueConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CardValue cardValue = (CardValue)value;

            if (cardValue == CardValue.INVALID)
            {
                return "";
            }

            return Card.getCardValueString(cardValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Card.ConvertCardValue(value as string);
        }
    }

    public class CardSuitConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CardSuit cardSuit = (CardSuit)value;

            if (cardSuit == CardSuit.INVALID)
            {
                return "";
            }

            return Card.getCardSuitString(cardSuit);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Card.ConvertCardSuit(value as string);
        }
    }

    public class CardConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Card card = (Card)value;

            if (card == null)
            {
                return "";
            }

            return card.GetString(' ');
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String[] cardInfo = (value as string).Split(' ');

            return new Card(Card.ConvertCardSuit(cardInfo[0]), Card.ConvertCardValue(cardInfo[1]));
        }
    }
}
