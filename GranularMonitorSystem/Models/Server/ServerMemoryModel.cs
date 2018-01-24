using System;
using Newtonsoft.Json;

namespace GranularMonitorSystem
{
    public class ServerMemoryModel
    {
        [JsonProperty(PropertyName = "memory")]
        public double Memory { get; set; }

        [JsonProperty(PropertyName = "timestamp")] 
        public DateTime Timestamp { get; set; }

    }
}
