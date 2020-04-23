using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GhostFriendClient.Converters
{
    public class PlayerConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int indexValue = (int)value;

            return "Player " + indexValue.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String[] valueList = (value as string).Split(' ');

            return System.Convert.ToInt32(valueList[1]);
        }
    }
}
