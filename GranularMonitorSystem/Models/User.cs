using System;
using Newtonsoft.Json;

namespace GranularMonitorSystem.Model
{
	public class User 
	{
		public int UserId { get; set; } 

		[JsonProperty(PropertyName = "username")]
		public string UserName { get; set; }

		[JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

		[JsonProperty(PropertyName = "isvalid")]
		public bool IsValid{ get; set; }

		[JsonProperty(PropertyName = "token")]
		public string Token { get; set; }
	}
}
