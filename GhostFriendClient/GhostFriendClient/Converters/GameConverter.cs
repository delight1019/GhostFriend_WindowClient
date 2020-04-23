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
    public class GamePhaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            GamePhase gamePhase = (GamePhase)value;

            return GameControl.getGamePhase(gamePhase);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GameControl.convertGamePhase(value as string);
        }
    }
}
