using System;
using Newtonsoft.Json;

namespace Monitor.Model
{
	public class User 
	{
        [JsonProperty(PropertyName = "user_id")]
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

        [JsonProperty(PropertyName = "account_id")]
        public int AccountType { get; set; }

        [JsonProperty(PropertyName = "purchase_token")]
        public string PurchaseToken{ get; set; }
    }
}
