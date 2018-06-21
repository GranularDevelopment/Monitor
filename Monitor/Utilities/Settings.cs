// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;
namespace Monitor
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
		private static string _userName= "";
		private static int _userId=0;
        private static bool _useMocks = true;

		#endregion

		public static string AuthAccessToken
        {
            get{return _accessToken;}
            set{ _accessToken = value;}
		}

		public static string UserName 
        {
            get{return _userName;}
            set{ _userName= value;}
        }

		public static int UserId 
        {
            get{return _userId;}
            set{ _userId = value;}
        }

        public static bool UseMocks 
        {
            get{return _useMocks;}
            set{ _useMocks = value;}
        }
    }
}