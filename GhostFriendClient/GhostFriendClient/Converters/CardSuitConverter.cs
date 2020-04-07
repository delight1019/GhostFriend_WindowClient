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
    public class CardSuitConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CardSuit cardSuit = (CardSuit)value;
            return cardSuit.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Card.ConvertCardSuit(value as string);
        }
    }
}
