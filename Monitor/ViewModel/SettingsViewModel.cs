using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Monitor.Localization;
using Monitor.Model;
using Monitor.Services.Identity;
using Plugin.InAppBilling;
using Plugin.InAppBilling.Abstractions;
using Xamarin.Forms;

namespace Monitor
{
    public class SettingsViewModel: ViewModelBase
    {

        public readonly IIdentityService _userService;

        Dictionary<int, string> SubscriptionStrings = new Dictionary<int, string>{
            {1, "FREE"},
            {2, "BASIC"},
            {3, "PREMIUM"},
        };

        Dictionary<int, string> SubscriptionDescriptionStrings = new Dictionary<int, string>{
            {1, AppResources.SubscriptionFree},
            {2, AppResources.SubscriptionBasic},
            {3, AppResources.SubscriptionPremium},
        };

        public SettingsViewModel( IIdentityService userService)
        {
            _userService = userService;
        }

        public ICommand UpgradeBasicCommand => new Command(async () =>  await upgradeBasicAsync());
        public ICommand UpgradePremiumCommand => new Command(async () =>  await upgradePremiumAsync());

        public override async Task InitializeAsync(object navigationData)
        {
            User user = await _userService.UserInfoAsync<User>();
            SubscriptionType = SubscriptionStrings[Settings.AccountType]; 
            SubscriptionDescription = SubscriptionDescriptionStrings[Settings.AccountType];
        }

        void displayAccountTypes(){
            IsFreeAccount = (Settings.AccountType== 1)? true: false;
            IsBasicAccount = (Settings.AccountType== 2)? true: false;
            IsPremiumAccount = (Settings.AccountType== 3)? true: false;

            if(IsFreeAccount){
                IsBasicAccount= false; 
                IsPremiumAccount= false; 
            }else if(IsBasicAccount) {
                IsFreeAccount= false; 
                IsPremiumAccount= false; 
            }
            else if (IsPremiumAccount){
                IsFreeAccount= false; 
                IsPremiumAccount= false; 
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

        public async Task<bool> PurchaseItem(string productId, string payload ="123")
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
                var purchase = new object();//await billing.PurchaseAsync(productId, ItemType.Subscription, payload);

                //possibility that a null came through.
                if(purchase == null)
                {
                    //did not purchase
                }
                else
                {
                    User user = new User{
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

        string _subscriptionType;
        public string SubscriptionType
        {
            get{return _subscriptionType; }
            set{ 
                _subscriptionType = value;
                RaisePropertyChanged(() => SubscriptionType);
            }
        }

        string _subscriptionDescription;
        public string SubscriptionDescription
        {
            get{return _subscriptionDescription;}
            set{
                _subscriptionDescription=value;
                RaisePropertyChanged(() => SubscriptionDescription);
            }
        }

        bool _isFreeAccount;
        public bool IsFreeAccount 
        {
            get{return _isFreeAccount;}
            set{
                _isFreeAccount=value;
                RaisePropertyChanged(() => _isFreeAccount);
            }
        }

        bool _isBasicAccount;
        public bool IsBasicAccount 
        {
            get{return _isBasicAccount;}
            set{
                _isBasicAccount=value;
                RaisePropertyChanged(() => _isBasicAccount);
            }
        }

        bool _isPremiumAccount;
        public bool IsPremiumAccount
        {
            get{return _isPremiumAccount;}
            set{
            _isPremiumAccount=value;
            RaisePropertyChanged(() => _isPremiumAccount);
        }
    }
}
