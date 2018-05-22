using System;
using System.Globalization;
using Xamarin.Forms;

namespace Monitor.Converters
{
	public class Inverse: IValueConverter
	{
		public Inverse()
		{
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return !(bool)value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
