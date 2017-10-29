// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;
namespace GranularMonitorSystem
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private const string AccessToken = "access_token";
		private const string IdToken = "id_token";
		private const string IdUseMocks = "use_mocks";
		private const string IdUrlBase = "url_base";
		private const string IdUseFakeLocation = "use_fake_location";
		private const string IdLatitude = "latitude";
		private const string IdLongitude = "longitude";
		private const string IdAllowGpsLocation = "allow_gps_location";
		private static readonly string AccessTokenDefault = string.Empty;
		private static readonly string IdTokenDefault = string.Empty;
		private static readonly bool UseMocksDefault = true;
		private static readonly bool UseFakeLocationDefault = false;
		private static readonly bool AllowGpsLocationDefault = false;
		private static readonly double FakeLatitudeDefault = 47.604610d;
		private static readonly double FakeLongitudeDefault = -122.315752d;
		//private static readonly string UrlBaseDefault = GlobalSettings.Instance.BaseEndpoint;

		#endregion

		public static string AuthAccessToken
        {
            get => AccessToken;
			set => value = AccessToken;
		}
		//	get => AppSettings.GetValueOrDefault<string>(AccessToken, AccessTokenDefault);
		//	set => AppSettings.AddOrUpdateValue<string>(AccessToken, value);
		//}

		//public static string AuthIdToken
		//{
		//	get => AppSettings.GetValueOrDefault<string>(IdToken, IdTokenDefault);
		//	set => AppSettings.AddOrUpdateValue<string>(IdToken, value);
		//}

		//public static bool UseMocks
		//{
		//	get => AppSettings.GetValueOrDefault<bool>(IdUseMocks, UseMocksDefault);
		//	set => AppSettings.AddOrUpdateValue<bool>(IdUseMocks, value);
		//}

		//public static string UrlBase
		//{
		//	get => AppSettings.GetValueOrDefault<string>(IdUrlBase, UrlBaseDefault);
		//	set => AppSettings.AddOrUpdateValue<string>(IdUrlBase, value);
		//}

		//public static bool UseFakeLocation
		//{
		//	get => AppSettings.GetValueOrDefault<bool>(IdUseFakeLocation, UseFakeLocationDefault);
		//	set => AppSettings.AddOrUpdateValue<bool>(IdUseFakeLocation, value);
		//}

		//public static double Latitude
		//{
		//	get => AppSettings.GetValueOrDefault<double>(IdLatitude, FakeLatitudeDefault);
		//	set => AppSettings.AddOrUpdateValue<double>(IdLatitude, value);
		//}

		//public static double Longitude
		//{
		//	get => AppSettings.GetValueOrDefault<double>(IdLongitude, FakeLongitudeDefault);
		//	set => AppSettings.AddOrUpdateValue<double>(IdLongitude, value);
		//}

		//public static bool AllowGpsLocation
		//{
		//	get => AppSettings.GetValueOrDefault<bool>(IdAllowGpsLocation, AllowGpsLocationDefault);
		//	set => AppSettings.AddOrUpdateValue<bool>(IdAllowGpsLocation, value);
		//}

		private const string SettingsKey = "settings_key";
		private static readonly string SettingsDefault = string.Empty;



		public static string GeneralSettings
		{
			get
			{
				return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(SettingsKey, value);
			}
		}
    }
}