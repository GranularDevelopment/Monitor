using Newtonsoft.Json;

namespace GranularMonitorSystem.Model
{
	public class WebsiteModel   
	{
		[JsonProperty(PropertyName = "statuscode")]
		public string StatusCode { get; set; }

		[JsonProperty(PropertyName = "description")]
		public string Description { get; set; }

	}
}
