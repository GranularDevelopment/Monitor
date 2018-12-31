using System;
using Newtonsoft.Json;

namespace Monitor.Models
{
    public class Subscription 
    {
       [JsonProperty(PropertyName = "user_id")]
       public int UserId { get; set; }

       [JsonProperty(PropertyName = "purchase_id")]
       public string PurchaseId { get; set; }

       [JsonProperty(PropertyName = "purchase_state")]
       public string PurchaseSate { get; set; }
        
       [JsonProperty(PropertyName = "purchase_token")]
       public int PurchaseToken{ get; set; }

       [JsonProperty(PropertyName = "purchase_date")]
       public DateTime PurchaseDate{ get; set; }

    }
}
