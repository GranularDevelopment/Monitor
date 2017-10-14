using System;
using Newtonsoft.Json;

namespace GranularMonitorSystem.Model.Models.Server
{
    public class ServerDiskspaceModel
    {
        [JsonProperty(PropertyName = "diskspace")]
        public double Diskused { get; set; }

        [JsonProperty(PropertyName = "timestamp")] 
        public DateTime Timestamp { get; set; }
        
    }
}