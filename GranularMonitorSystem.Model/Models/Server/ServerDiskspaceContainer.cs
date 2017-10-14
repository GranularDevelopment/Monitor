using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GranularMonitorSystem.Model.Models.Server
{
    public class ServerDiskspaceContainer
    {
        [JsonProperty(PropertyName = "server")]
        public List<ServerDiskspaceModel> DiskspaceModel { get; set; }
    }
}
