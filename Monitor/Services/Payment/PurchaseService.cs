using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Monitor.Payment
{
    public abstract  class PurchaseService  
    {

        public abstract Task<Purchase[]> GetPrices(params string[] ids);

        public async Task Buy(Purchase purchase){
            //var receipt = await  BuyNatve(purchase);

            //Debug.WriteLine("Native purchase successful: " + receipt.Id);

            //var appleReciept = receipt as AppleReceipt;
            //if(appleReciept != null)
            //{
            //    await  _client.Verify(appleReciept);
            //    return;
            //}

            //var googleReceipt = receipt as GoogleReceipt;
            //if(googleReceipt != null)
            //{
                //await _client.Verify(googleReceipt);
                //return;
            //}
        }

        //protected abstract Task<Receipt> BuyNative(Purchase purchase);

    }
}

