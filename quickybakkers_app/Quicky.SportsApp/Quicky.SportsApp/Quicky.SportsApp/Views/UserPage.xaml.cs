using Quicky.SportsApp.Models;
using Quicky.SportsApp.Services.Service;
using Quicky.SportsApp.Views.Settings.MatchdaySettings;
using Quicky.SportsApp.Views.Settings.PlayerSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Quicky.SportsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : LoginContentPage 
    {
        public UserPage() : base()
        {
            InitializeComponent();
            //SetVisibility();
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

        public async Task OnLogoutButtonClicked()
        {
            base.Logout();
            LoginPanel.IsVisible = true;
            MainPanel.IsVisible = false;
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

        public async Task OnPlayerManagementButtonClicked()
        {
            await Navigation.PushAsync(new PlayerManagementPage());
        }
        public async Task OnMatchdayManagementButtonClicked()
        {
            await Navigation.PushAsync(new MatchdayManagementPage());
        }
    }
}