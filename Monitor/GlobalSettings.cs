using System;

namespace Monitor
{
	public class GlobalSetting
	{
		public const string DevelopmentEndpoint = "http://localhost:8000/api/";
		public const string ProductionEndpoint = "https://granulardevelopment.com/api/";
		public bool  Mock = true;

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

        public string GetMonitor { get; set; }

		public string AddMonitor { get; set; }

		public string GetMonitors {get; set;}

        public string BasketEndpoint { get; set; }

        public string IdentityEndpoint { get; set; }

        public string LocationEndpoint { get; set; }

        public string UserInfoEndpoint { get; set; }

        public string TokenEndpoint { get; set; }

        public string LogoutEndpoint { get; set; }

        public string IdentityCallback { get; set; }

        public string LogoutCallback { get; set; }

        private void UpdateEndpoint(string baseEndpoint)
        {

            //URL_SIGNUP = "http://localhost:5000/auth/api/v1/register";
            //URL_RESET = "http://localhost:5000/auth/api/v1/reset";
            //URL_TOKEN = "http://localhost:8000/api/v1/token";

            //URL_WEBSITE = "http://localhost:8000/api/v1/website";
            //URL_ALERT = "http://localhost:8000/api/v1/alerts";
            ////URL_ALERT = "http://localhost:5000/api/v1/alerts";
            //URL_ADD_MONITOR =  "http://localhost:8000/api/v1/add/monitor"; 
            //URL_GET_MONITORS =  "http://localhost:8000/api/v1/monitors"; 

            Register = $"{baseEndpoint}register";
            AuthToken = $"{baseEndpoint}token";
			AddMonitor = $"{baseEndpoint}add/monitor";
			GetMonitor = $"{baseEndpoint}monitor";
			GetMonitors = $"{baseEndpoint}monitors";
        }
    }
}
