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
using System.Reflection;

namespace quicky.bakkers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserContentPage : AuthorizedContentPage
    {
        public UserContentPage()
        {
            InitializeComponent();
            SetVisibility();
            try
            {
                versionLabel.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
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
            var username = usernameEntry.Text.ToLower();
            var password = passwordEntry.Text.ToLower();

            if (!(username == "admin" && password == "admin"))
            {
                messageLabel.Text = "Login not correct";
                return;
            }

            //check credentials
            var user = new User()
            {
                Username = username,
                LastLogin = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(1)
            };

            var newUser = _userService.SaveUser(user);
            if (newUser == null)
                messageLabel.Text = "Fout tijdens inloggen";
            else
            {
                messageLabel.Text = "Inloggen gelukt";
                LoginPanel.IsVisible = false;
                MainPanel.IsVisible = true;
            }
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