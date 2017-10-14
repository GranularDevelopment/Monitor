using System;
using System.Globalization;
using Xamarin.Forms;

namespace GranularMonitorSystem.Common.Utilities
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
