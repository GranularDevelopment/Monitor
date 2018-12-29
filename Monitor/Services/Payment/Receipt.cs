using System;

namespace Monitor.Payment
{
    public class Receipt
    {
        public string Id { get; set; }

        public string BundleId { get; set;}

        public string TransactionId { get; set; }
    }

    public class AppleReceipt : Receipt
    {
        public byte[] Data { get; set;}
    }

    public class GoogleReceipt : Receipt
    {
        public string DeveloperPayload {get; set;}

        public string PurchaseToken { get; set;}
    }
}
