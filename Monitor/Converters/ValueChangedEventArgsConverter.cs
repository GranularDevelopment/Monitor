using System;
using System.Globalization;
using Xamarin.Forms;

namespace Monitor.Converters
{
    public class ValueChangedEventArgsConverter: IValueConverter
    {
        public ValueChangedEventArgsConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eventArgs = value as ValueChangedEventArgs;
            if (eventArgs == null)
                throw new ArgumentException("Expected TappedEventArgs as value", "value");

            return eventArgs.NewValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();

        }
    }
}
