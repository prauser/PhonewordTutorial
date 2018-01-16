using System;
using Xamarin.Forms;

namespace PhonewordXamarinForms
{
	public partial class MainPage : ContentPage
	{
        string translatedNumber;

		public MainPage()
		{
			InitializeComponent();
		}

        void OnTranslate(object sender, EventArgs e)
        {
            translatedNumber = Core.PhoneWordTranslator.ToNumber(phoneNumberText.Text);
            if (!string.IsNullOrWhiteSpace(translatedNumber))
            {
                callButton.IsEnabled = true;
                callButton.Text = "Call " + translatedNumber;
                App.PhoneNumbers.Add(translatedNumber);
                callHistoryButton.IsEnabled = true;
            }
            else
            {
                callButton.IsEnabled = false;
                callButton.Text = "Call";
            }
        }

        async void OnCall(object sender, EventArgs e)
        {
            if (await this.DisplayAlert (
                "Dial a Number",
                "Would you liked to call " + translatedNumber + "?",
                "Yes",
                "No"))
            {
                var dialer = DependencyService.Get<IDialer>();
                if (dialer != null)
                {   
                    dialer.Dial(translatedNumber);
                }
            }
        }

        async void OnCallHistory(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CallHistoryPage());
        }


	}
}
