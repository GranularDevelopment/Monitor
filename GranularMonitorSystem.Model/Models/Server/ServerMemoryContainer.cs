using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GranularMonitorSystem.Model.Models.Server
{
    public class ServerMemoryContainer
    {
        [JsonProperty(PropertyName = "server")]
        public List<ServerMemoryModel>  MemoryModels{ get; set; }
    }
}
