using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ManagementProject.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!bool.TryParse(value.ToString(), out bool isTrue))
                return Visibility.Collapsed;

            return isTrue? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Visibility)value == Visibility.Visible;
        }
    }
}
