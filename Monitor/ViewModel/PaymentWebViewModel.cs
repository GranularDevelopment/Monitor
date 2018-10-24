using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Monitor.Model;
using Monitor.Services.Identity;
using Plugin.InAppBilling;
using Plugin.InAppBilling.Abstractions;
using Xamarin.Forms;

namespace Monitor
{
    public class PaymentWebViewModel:ViewModelBase
    {

        public readonly IIdentityService _userService;

        public PaymentWebViewModel(IIdentityService userService)
        {
            _userService = userService;
        }

        public ICommand NavigatingCommand => new Command(async (sender)=> await NavigatingCommandAsync(sender));
        public ICommand NavigatedCommand => new Command(async (sender) => await NavigatedCommandAsync(sender));


        private async Task NavigatingCommandAsync(object sender)
        {
            var upgradeType = sender.ToString();

            if(upgradeType.Contains("basic"))
            {
                await upgradeBasicAsync();
            }

            if (upgradeType.Contains("premium"))
            {
                await upgradePremiumAsync();
            }
        }

        private async Task upgradeBasicAsync()
        {
            await PurchaseItem("10monitor");

        }

        private async Task upgradePremiumAsync()
        {
            await PurchaseItem("50monitor");

        }

        public async Task<bool> PurchaseItem(string productId, string payload = "123")
        {
            var billing = CrossInAppBilling.Current;
            try
            {
                var connected = await billing.ConnectAsync(ItemType.Subscription);
                if (!connected)
                {
                    //we are offline or can't connect, don't try to purchase
                    return false;
                }

                //check purchases
                var purchase = await billing.PurchaseAsync(productId, ItemType.Subscription, payload);

                //possibility that a null came through.
                if (purchase == null)
                {
                    //did not purchase
                }
                else
                {
                    User user = new User
                    {
                        UserName = Settings.UserName
                    };

                    await _userService.UpgradeAccountAsync(user);
                    //purchased!
                }
            }
            catch (InAppBillingPurchaseException purchaseEx)
            {
                //Billing Exception handle this based on the type
                Debug.WriteLine("Error: " + purchaseEx);
            }
            catch (Exception ex)
            {
                //Something else has gone wrong, log it
                Debug.WriteLine("Issue connecting: " + ex);
            }
            finally
            {
                await billing.DisconnectAsync();
            }

            return true;
        }

        private async Task NavigatedCommandAsync(object sender)
        {
        }
    }
}
