using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace PlanHora.Converters
{
    public class BoolToLibreConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool esLibre && esLibre)
                return "Libre";
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
