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
    public class GameWinnerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int indexValue = (int)value;

            if ((value as string).Equals(GameParams.DECLARER_WIN))
            {
                return "여당이 승리했습니다.";
            }
            else if ((value as string).Equals(GameParams.DECLARER_LOSE))
            {
                return "야당이 승리했습니다.";
            }
            else
            {
                return "";
            }           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value as string).Equals("여당이 승리했습니다."))
            {
                return GameParams.DECLARER_WIN;
            }
            else if ((value as string).Equals("야당이 승리했습니다."))
            {
                return GameParams.DECLARER_LOSE;
            }
            else
            {
                return "";
            }
        }
    }
}
