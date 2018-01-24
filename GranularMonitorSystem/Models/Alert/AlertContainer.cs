using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GranularMonitorSystem
{
    public class AlertContainer
    {
        [JsonProperty(PropertyName = "alert")]
        public IEnumerable<AlertModel> alert { get; set; }
    }
}
