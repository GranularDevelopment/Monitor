using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Monitor.Supervisor
{
    public class MonitorContainer
    {
		[JsonProperty(PropertyName = "monitor")]
        public IEnumerable<MonitorModel> monitor{ get; set; }
    }
}
