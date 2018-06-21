using System;
using Newtonsoft.Json;

namespace Monitor
{
    public class MonitorModel 
	{
		[JsonProperty(PropertyName = "name")]
        public string Name { get; set; } 
	
        [JsonProperty(PropertyName = "interval")] 
        public int Interval { get; set; } 

		[JsonProperty(PropertyName = "url")] 
        public string URL { get; set; } 

		[JsonProperty(PropertyName = "user_id")]
        public int UserId { get; set; }

		[JsonProperty(PropertyName = "alert_description")]
        public string Description{ get; set; }

		[JsonProperty(PropertyName = "timestamp")]
        public DateTime TimeStamp { get; set; }

		[JsonProperty(PropertyName = "on")]
        public bool On{ get; set; }

		[JsonProperty(PropertyName = "statuscode")]
        public string StatusCode{ get; set; }
    }
}
