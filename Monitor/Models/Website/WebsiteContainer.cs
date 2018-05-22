using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Monitor
{
	public class WebsiteContainer 
	{
		[JsonProperty(PropertyName = "website")]
		public List<WebsiteModel> website { get; set; }

		[JsonProperty(PropertyName = "on")]
		public bool On { get; set; }

		[JsonProperty(PropertyName = "description")]
		public string Description { get; set; }

		[JsonProperty(PropertyName = "statuscode")]
		public string StatusCode { get; set; }
	}
}
