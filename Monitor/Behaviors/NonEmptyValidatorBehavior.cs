using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Monitor.Behaviors
{
    public class NonEmptyValidatorBehavior: Behavior<Entry>
	{


        // Creating BindableProperties with Limited write access: http://iosapi.xamarin.com/index.aspx?link=M%3AXamarin.Forms.BindableObject.SetValue(Xamarin.Forms.BindablePropertyKey%2CSystem.Object) 
    
        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(NonEmptyValidatorBehavior), false);
    
        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;
    
        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { base.SetValue(IsValidPropertyKey, value); }
        }
    
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
        }
    
        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = !string.IsNullOrEmpty(e.NewTextValue) && !string.IsNullOrWhiteSpace(e.NewTextValue);
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
        }
    
        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
        }
    
    }
}
