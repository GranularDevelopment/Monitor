using System;
namespace Monitor.Payment
{
    public class Purchase
    {
        public string Id {get; set;}

        public string CurrencyCode { get; set; }

        public string LocalizedPrice { get; set; }

        public string ProductId {get; set;}

        public string Price {get; set;}

        public string Name { get; set; }

        public object NativeObject {get; set;}
    }
}
