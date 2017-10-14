using System;
using Newtonsoft.Json;

namespace GranularMonitorSystem.Model.Models.Server
{
    public class ServerMemoryModel
    {
        [JsonProperty(PropertyName = "memory")]
        public double Memory { get; set; }

        [JsonProperty(PropertyName = "timestamp")] 
        public DateTime Timestamp { get; set; }

    }
}
