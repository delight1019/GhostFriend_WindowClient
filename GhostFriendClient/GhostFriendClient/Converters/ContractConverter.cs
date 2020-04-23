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
    public class ContractSuitConverter: IValueConverter
    {
        private const string DIAMOND_IMAGE_PATH = "/Resources/Diamond.png";
        private const string HEART_IMAGE_PATH = "/Resources/Heart.png";
        private const string SPADE_IMAGE_PATH = "/Resources/Spade.png";
        private const string CLUB_IMAGE_PATH = "/Resources/Club.png";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }

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
            return (CardSuit)value;            
        }
    }
    public class ContractConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Contract contract = (Contract)value;

            if (contract == null)
            {
                return "";
            }

            if (contract.ContractSuit == CardSuit.INVALID)
            {
                return "선언되지 않았습니다.";
            }

            return Card.getCardSuitString(contract.ContractSuit) + " " + contract.Score.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String[] contractInfo = (value as string).Split(' ');

            Contract contract = new Contract(Card.ConvertCardSuit(contractInfo[0]), System.Convert.ToInt32(contractInfo[1]));

            return contract;
        }
    }
}
