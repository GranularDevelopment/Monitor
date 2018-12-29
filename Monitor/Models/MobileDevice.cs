using System;
using Newtonsoft.Json;

namespace Monitor.Models
{
    public class MobileDevice
    {
        [JsonProperty(PropertyName = "user_id")]
        public int UserId { get; set; }

        [JsonProperty(PropertyName = "device_id")]
        public string DeviceId{ get; set; }
    }
}
