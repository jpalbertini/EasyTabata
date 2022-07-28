using EasyTabata.Models;
using System.Globalization;

namespace EasyTabata.Converters
{
    class DurationToStringConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";
            return (value as Duration).ToString();
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return new Duration();
            return Duration.FromString(value as string);
        }
    }
}
