using System;
using System.Globalization;
using System.Windows;

namespace Controls
{
    /// <summary>
    /// A converter that takes in a boolean and returns a <see cref="Visibility"/>
    /// </summary>
    public class BooleanToVisiblityConverter : BaseValueConverter<BooleanToVisiblityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Nutzung in XAML: 
            //  Visibility ="{Binding BOOL_PROPERTY, Converter={local:BooleanToVisiblityConverter}, ConverterParameter=PARAMETER }"

            // Ohne Parameter: true => Visible, false => Hidden
            if (parameter == null)
                if (value == null)
                    return Visibility.Hidden;
                else
                    return (bool)value ? Visibility.Visible : Visibility.Hidden;
            // Parameter Reverse: true => Hidden, false => Visible
            else if (parameter.ToString() == "Reverse")
                if (value == null)
                    return Visibility.Visible;
                else
                    return (bool)value ? Visibility.Hidden : Visibility.Visible;
            // Parameter Collapsed: true => Visible, false => Collapsed
            else if (parameter.ToString() == "Collapsed")
                if (value == null)
                    return Visibility.Collapsed;
                else
                    return (bool)value ? Visibility.Visible : Visibility.Collapsed;
            // Parameter ReverseCollapsed: true => Collapsed, false => visible
            else if (parameter.ToString() == "ReverseCollapsed")
                if (value == null)
                    return Visibility.Visible;
                else
                    return (bool)value ? Visibility.Collapsed : Visibility.Visible;
            // anderer Parameter == ohne Parameter: true => Visible, false => Hidden
            else
                if (value == null)
                return Visibility.Hidden;
            else
                return (bool)value ? Visibility.Visible : Visibility.Hidden;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}