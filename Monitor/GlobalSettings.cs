using System;

namespace Monitor
{
	public class GlobalSetting
	{
		public const string DevelopmentEndpoint = "http://localhost:5000/";
		public const string ProductionEndpoint = "https://granulardevelopment.com/";
		public bool  Mock = Settings.UseMocks;

		private string _baseEndpoint;
        private static readonly GlobalSetting _instance = new GlobalSetting();

        public GlobalSetting()
        {
            AuthToken = "INSERT AUTHENTICATION TOKEN";
			BaseEndpoint = Mock == true ? DevelopmentEndpoint : ProductionEndpoint ;
        }

        public static GlobalSetting Instance
        {
            get { return _instance; }
        }

        public string BaseEndpoint
        {
            get { return _baseEndpoint; }
            set
            {
                _baseEndpoint = value;
                UpdateEndpoint(_baseEndpoint);
            }
        }

		public int UserId { get; set; }

		public string UserName { get; set; }

		public string AuthToken{ get; set; }

        public string Register{ get; set; }

        public string UpgradeAccount{ get; set; }

        public string UserInfo{ get; set; }

        public string Reset{ get; set; }

        public string GetMonitor { get; set; }

		public string AddMonitor { get; set; }

		public string GetMonitors {get; set;}

        public string EditMonitor {get; set;}

        public string DeleteMonitor {get; set;}

        public string IdentityEndpoint { get; set; }

        public string UserInfoEndpoint { get; set; }

        public string TokenEndpoint { get; set; }

        public string LogoutEndpoint { get; set; }

        public string IdentityCallback { get; set; }

        public string LogoutCallback { get; set; }

        private void UpdateEndpoint(string baseEndpoint)
        {
            Register = $"{baseEndpoint}auth/api/v1/register";
            Reset= $"{baseEndpoint}auth/api/v1/reset";
            UpgradeAccount= $"{baseEndpoint}monitor_api/v1/upgrade_account";
            UserInfo = $"{baseEndpoint}monitor_api/v1/user_info";
            TokenEndpoint = $"{baseEndpoint}monitor_api/v1/token";
			AddMonitor = $"{baseEndpoint}monitor_api/v1/add/monitor";
            GetMonitor = $"{baseEndpoint}monitor_api/v1/monitor"; 
            GetMonitors = $"{baseEndpoint}monitor_api/v1/monitors";
            EditMonitor = $"{baseEndpoint}monitor_api/v1/edit/monitor";
            DeleteMonitor = $"{baseEndpoint}monitor_api/v1/delete/monitor";
        }
    }
}
