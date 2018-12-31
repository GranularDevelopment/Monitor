using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Monitor.Model;
using Monitor.Services.Identity;
using Plugin.InAppBilling;
using Plugin.InAppBilling.Abstractions;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;
using Monitor.Payment;
using Monitor.Services;
using System.Linq;

namespace Monitor
{
    public class PaymentViewModel : ViewModelBase
    {
        public readonly IIdentityService _userService;
        public readonly IInAppBilling _billing;


        public PaymentViewModel(IIdentityService userService)
        {
            _userService = userService;
        }
        public ICommand BasicCommand=> new Command(async (sender)=> await UpgradeBasicCommandAsync());
        public ICommand PremiumCommand => new Command(async (sender) => await UpgradePremiumCommandAsync());

        public async override Task InitializeAsync(object navigationData)
        {
             var prices = await _billing.GetProductInfoAsync(ItemType.Subscription, Settings.BasiciOS, Settings.PremiumiOS);
        }

        private async Task UpgradeBasicCommandAsync()
        {
            if (Settings.UseMocks)
            {
                await FakePurchase(Settings.BasiciOS);

            }
            else
            {

                await PurchaseItem(Settings.BasiciOS);
            }

        }

        private async Task UpgradePremiumCommandAsync()
        {
            if (Settings.UseMocks)
            {
                await FakePurchase(Settings.PremiumiOS);

            }
            else
            {
                await PurchaseItem(Settings.PremiumiOS);
            }
        }

        public async Task<bool> PurchaseItem(string productId, string payload = "")
        {
            try
            {
                var connected = await CrossInAppBilling.Current.ConnectAsync();
                if (!connected)
                {
                    //we are offline or can't connect, don't try to purchase
                    await DialogService.ShowAlertAsync("Please check your connection", "Can't connect", "OK");
                    return false;
                }

                //check purchases
                var purchase = await CrossInAppBilling.Current.PurchaseAsync(productId, ItemType.Subscription, payload, new VerifyPurchases());

                //possibility that a null came through.
                if (purchase == null)
                {
                    //did not purchase
                }
                else
                {
                   

                    var result = purchase.State;
                    var statut = (int)result;
                    var puhase  = purchase.Id;
                    var t = purchase.PurchaseToken;
                    var date = purchase.TransactionDateUtc;

                    User user = new User
                    {
                        UserName = Settings.UserName,
                        AccountType = getAccountType(productId),
                        PurchaseToken = purchase.PurchaseToken
                    };

                    await _userService.UpgradeAccountAsync(user);
                    await NavigationService.PopAsync();

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
                //await _billing.DisconnectAsync();
                await CrossInAppBilling.Current.DisconnectAsync();
            }

            return true;
        }

        public async Task<PurchaseState> CheckAppPurchaseStatus(string productId)
        {
            var result = PurchaseState.Unknown;
            try
            {
                var connected = await CrossInAppBilling.Current.ConnectAsync();

                if (!connected)
                {
                    //Couldn't connect
                    //result = false;
                }

                //check purchases

                var purchases = await CrossInAppBilling.Current.GetPurchasesAsync(ItemType.Subscription);//, new VerifyPurchases());

                if (purchases?.Any(p => p.ProductId == productId) ?? false)
                {
                    //Purchase restored
                    var purchase = purchases.Where(p => p.ProductId == productId).First();
                    result = purchase.State;
                    //Settings.PurchaseId = purchase.Id;
                    //Settings.PurchaseToken = purchase.PurchaseToken;
                }
                else
                {
                    //no purchases found

                }
            }
            catch (Exception ex)
            {
                //Something has gone wrong
            }
            finally
            {
                await CrossInAppBilling.Current.DisconnectAsync();
            }
            return result;
        }

        async Task FakePurchase(string productId)
        {
            User user = new User
            {
                UserName = Settings.UserName,
                AccountType = getAccountType(productId),
                PurchaseToken = Guid.NewGuid().ToString()
            };

            var result = PurchaseState.Purchased;

            await _userService.UpgradeAccountAsync(user);
        }

        private int getAccountType(string productId)
        {
            if ( Settings.BasiciOS == productId )
            {
                return 2;
            }
            else if (Settings.PremiumiOS == productId)
            {
                return 3;
            }
            else
            {
                return 1;
            }

        }

    }
}
