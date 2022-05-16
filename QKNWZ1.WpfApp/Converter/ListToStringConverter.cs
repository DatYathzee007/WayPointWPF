using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace QKNWZ1.WpfApp
{
    [ValueConversion(typeof(IEnumerable<Waypoint>), typeof(string))]
    class ListToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            StringBuilder sb = new("Waypoints:");
            if (value is IEnumerable<Waypoint> waypoints)
            {
                int counter = 1;
                foreach (Waypoint waypoint in waypoints)
                {
                    sb.Append($"  {counter++}. ").Append(waypoint.PropsToString());
                }
                return sb.ToString();
            }
            return "Input was not of the desired type.";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
