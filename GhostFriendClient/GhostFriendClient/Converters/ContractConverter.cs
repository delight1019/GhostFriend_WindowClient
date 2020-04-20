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
    public class ContractConverter: IValueConverter
    {
        private const string DIAMOND_IMAGE_PATH = "/Resources/Diamond.png";
        private const string HEART_IMAGE_PATH = "/Resources/Heart.png";
        private const string SPADE_IMAGE_PATH = "/Resources/Spade.png";
        private const string CLUB_IMAGE_PATH = "/Resources/Club.png";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CardSuit cardSuit = (CardSuit)value;

            if (cardSuit == CardSuit.DIAMOND)
            {
                return DIAMOND_IMAGE_PATH;
            }
            else if (cardSuit == CardSuit.HEART)
            {
                return HEART_IMAGE_PATH;
            }
            else if (cardSuit == CardSuit.SPADE)
            {
                return SPADE_IMAGE_PATH;
            }
            else if (cardSuit == CardSuit.CLUB)
            {
                return CLUB_IMAGE_PATH;
            }
            else
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Contract)value;            
        }
    }
}
