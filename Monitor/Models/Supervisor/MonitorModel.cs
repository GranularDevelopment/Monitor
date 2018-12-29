using System;
using Newtonsoft.Json;

namespace Monitor
{
    public class MonitorModel 
	{
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; } 

		[JsonProperty(PropertyName = "name")]
        public string Name { get; set; } 
	
		[JsonProperty(PropertyName = "url")] 
        public string URL { get; set; } 

		[JsonProperty(PropertyName = "user_id")]
        public int UserId { get; set; }

		[JsonProperty(PropertyName = "alert_description")]
        public string Description{ get; set; }

		[JsonProperty(PropertyName = "timestamp")]
        public DateTime TimeStamp { get; set; }

		//[JsonProperty(PropertyName = "all_alerts")]
        //public bool Alerts{ get; set; }

        [JsonProperty(PropertyName = "sms_alert")]
        public bool SMSAlert{ get; set; }

        [JsonProperty(PropertyName = "email_alert")]
        public bool EmailAlert{ get; set; }

        [JsonProperty(PropertyName = "push_notification_alert")]
        public bool PushAlert{ get; set; }

		[JsonProperty(PropertyName = "statuscode")]
        public string StatusCode{ get; set; }

        //[JsonProperty(PropertyName = "statuscode")]
        //public string AlertCode { get; set; }
    }
}
