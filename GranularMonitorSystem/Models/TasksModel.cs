using System;
using Newtonsoft.Json;

namespace GranularMonitorSystem.Model
{
	public class TasksModel 
	{
		[JsonProperty(PropertyName = "description")]
		public string Description { get; set; }

		[JsonProperty(PropertyName = "indicator")]
		public string Indicator{ get; set; }

		[JsonProperty(PropertyName = "interval")]
		public string Interval { get; set; }

		[JsonProperty(PropertyName = "on")]
		public bool On { get; set; }

		[JsonProperty(PropertyName = "statuscode")]
		public int StatusCode { get; set; }

	}
}
