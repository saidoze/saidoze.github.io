using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using quicky.bakkers.Views.Settings.PlayerSettings;
using quicky.bakkers.Views.Settings.MatchdaySettings;
using quicky.bakkers.BasePages;
using quicky.bakkers.models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace quicky.bakkers.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserContentPage : AuthorizedContentPage
    {
		public UserContentPage ()
		{
			InitializeComponent();
            SetVisibility();
        }

        private void SetVisibility()
        {
            if (_isAuthenticated)
            {
                LoginPanel.IsVisible = false;
                MainPanel.IsVisible = true;
            }
            else
            {
                LoginPanel.IsVisible = true;
                MainPanel.IsVisible = false;
            }
        }

        public async Task OnLoginButtonClicked(object sender, EventArgs e)
        {
            var username = usernameEntry.Text;
            var password = passwordEntry.Text;

            //check credentials
            var user = new User()
            {
                Username = username,
                LastLogin = DateTime.Now,
                ExpiryDate = DateTime.Now.AddHours(1)
            };

            _userService.SaveUser(user);

            messageLabel.Text = "User saved";

            LoginPanel.IsVisible = false;
            MainPanel.IsVisible = true;
        }
        public async Task OnLogoutButtonClicked(object sender, EventArgs e)
        {
            base.Logout();
            LoginPanel.IsVisible = true;
            MainPanel.IsVisible = false;
        }
        public async Task OnPlayerManagementButtonClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new PlayerOverviewContentPage());
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }
        public async void OnMatchdayManagementButtonClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new MatchdayOverviewContentPage());
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }

    }
}