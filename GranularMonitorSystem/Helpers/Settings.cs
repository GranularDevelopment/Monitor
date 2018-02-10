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

		private static string _accessToken = "";
        private static bool _useMocks = false;

		#endregion

		public static string AuthAccessToken
        {
            get{return _accessToken;}
            set{ _accessToken = value;}
		}

        public static bool UseMocks 
        {
            get{return _useMocks;}
            set{ _useMocks = value;}
        }
    }
}