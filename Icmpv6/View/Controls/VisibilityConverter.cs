using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Icmpv6.View.Controls;

[ValueConversion(typeof(bool), typeof(Visibility))]
public class VisibilityConverter : IValueConverter {

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        if (value == null) {
            return Visibility.Hidden;
        }
        var source = (bool)value;
        return source ? Visibility.Visible : Visibility.Hidden;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        if (value == null) {
            return false;
        }
        var source = (Visibility)value;
        return source == Visibility.Visible;
    }
}
