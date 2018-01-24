using Newtonsoft.Json;

namespace GranularMonitorSystem
{
	public class DatabaseModel 
	{
		[JsonProperty(PropertyName = "description")]
		public string Description { get; set; }

		[JsonProperty(PropertyName = "statuscode")]
		public string StatusCode { get; set; }

		[JsonProperty(PropertyName = "on")]
		public bool On { get; set; }

	}
}
