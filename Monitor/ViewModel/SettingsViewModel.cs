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

        public ICommand UpgradeCommand => new Command(async () => await upgradeAsync());

        private async Task upgradeAsync()
        {
            try{
                await NavigationService.NavigateToAsync<PaymentViewModel>();
            }
            catch (Exception e){
                Console.WriteLine(e.ToString());

            }

        }

        public override async Task InitializeAsync(object navigationData)
        {
            User user = await _userService.UserInfoAsync<User>();
            Settings.AccountType = user.AccountType; // i don't like this...
            SubscriptionType = SubscriptionStrings[user.AccountType]; 
            SubscriptionDescription = SubscriptionDescriptionStrings[user.AccountType];
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
                RaisePropertyChanged(() => IsFreeAccount);
            }
        }

        bool _isBasicAccount;
        public bool IsBasicAccount 
        {
            get{return _isBasicAccount;}
            set{
                _isBasicAccount=value;
                RaisePropertyChanged(() => IsBasicAccount);
            }
        }

        bool _isPremiumAccount;
        public bool IsPremiumAccount
        {
            get{return _isPremiumAccount;}
            set{
            _isPremiumAccount=value;
            RaisePropertyChanged(() => IsPremiumAccount);
         }
        }
    }
}
