using System;
using Newtonsoft.Json;

namespace GranularMonitorSystem.Model.Models.Alert
{
    public class AlertModel 
    {
        [JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "description")]
		public string Description { get; set; }

		[JsonProperty(PropertyName = "statuscode")]
		public string StatusCode { get; set; }

	}
}
