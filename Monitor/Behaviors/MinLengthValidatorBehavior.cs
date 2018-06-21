using System;
using Xamarin.Forms;

namespace Monitor.Behaviors
{
	public class MinLengthValidatorBehavior:Behavior<Entry>
    {
		public static readonly BindableProperty MinLenghtProperty = BindableProperty.Create("MinLength", typeof(int), typeof(MinLengthValidatorBehavior),0);

        public int MinLength
		{
			get{return (int)GetValue(MinLenghtProperty);}
			set{SetValue(MinLenghtProperty, value);} 
		}

		protected override void OnAttachedTo(Entry bindable)
		{
			base.OnAttachedTo(bindable);
			bindable.TextChanged += bindable_TextChanged;
		}

		private void bindable_TextChanged(object sender, TextChangedEventArgs e)
		{
			if(e.NewTextValue.Length > MinLength)
			{
				((Entry)sender).Text = e.NewTextValue;
			}  
		}

		protected override void OnDetachingFrom(Entry bindable)
		{
			base.OnDetachingFrom(bindable);
			bindable.TextChanged -= bindable_TextChanged;
		}
	}
}
