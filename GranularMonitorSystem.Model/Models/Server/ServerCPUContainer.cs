﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GranularMonitorSystem.Model.Models.Server
{
    public class ServerCPUContainer
    {
        [JsonProperty(PropertyName = "server")]
        public IEnumerable<ServerCPUModel> CpuModels{ get; set; } 
    }
}
