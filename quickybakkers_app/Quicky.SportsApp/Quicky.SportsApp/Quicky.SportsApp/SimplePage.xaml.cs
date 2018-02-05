using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Quicky.SportsApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SimplePage : ContentPage
	{
        public SimplePage()
        {
            InitializeComponent();

            // Define the binding context
            this.BindingContext = this;
            
            // Set the IsBusy property
            this.IsBusy = false;

            // Login button action
            //this.BtnLogin.Clicked += BtnLogin_Clicked;
        }

        private void BtnLogin_Clicked(object sender, EventArgs e)
        {
            this.IsBusy = true;
            Task.Run(() => {
                /*UserUpdateRequest user = new UserUpdateRequest();
                user.userId = id;
                appClient.UpdateInfo(user);*/
                Thread.Sleep(2500);
                Device.BeginInvokeOnMainThread(() => {
                    this.IsBusy = false;
                    //Navigation.PushAsync(new CheckoutShippingAddressPage(appClient));
                });
            });

        }
    }
}