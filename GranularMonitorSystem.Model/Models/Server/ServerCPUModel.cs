using System;
using Newtonsoft.Json;

namespace GranularMonitorSystem.Model.Models.Server
{
    public class ServerCPUModel 
    {
        [JsonProperty(PropertyName = "cpu")]
        public double CPU { get; set; }

        [JsonProperty(PropertyName = "timestamp")] 
         public DateTime Timestamp { get; set; }
    }

}
