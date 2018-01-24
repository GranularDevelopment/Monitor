using System;
using Newtonsoft.Json;

namespace GranularMonitorSystem
{
    public class ServerContainer
    {
        [JsonProperty(PropertyName = "on")]
        public bool On { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "statuscode")]
        public string StatusCode { get; set; }
    }
}
