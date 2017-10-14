using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GranularMonitorSystem.Model.Models.Alert
{
    public class AlertContainer
    {
        [JsonProperty(PropertyName = "alert")]
        public IEnumerable<AlertModel> alert { get; set; }
    }
}
