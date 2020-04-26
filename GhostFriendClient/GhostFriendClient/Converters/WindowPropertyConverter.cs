using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace GhostFriendClient.Converters
{ 
    public class VisibleConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Boolean visibleValue = (Boolean)value;

            if (visibleValue)
            {
                return Visibility.Visible;
            } else
            {
                return Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Visibility)value == Visibility.Visible)
            {
                return true;
            }
            else if ((Visibility)value == Visibility.Hidden)
            {
                return false;
            } else
            {
                return false;
            }
        }
    }    
}
