using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using quicky.bakkers.services.Services;

namespace quicky.bakkers.BasePages
{
    public class AuthorizedContentPage : ContentPage
    {
        protected UserService _userService;
        protected bool _isAuthenticated
        {
            get
            {
                var users = _userService.LoadActiveUsers();
                return (users.Count == 1);
            }
        }

        public AuthorizedContentPage()
        {
            _userService = new UserService();
            CheckAuthentication();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CheckAuthentication();
        }

        protected virtual void CheckAuthentication()
        {
            var users = _userService.LoadActiveUsers();
            if (users.Count > 1)
                _userService.TruncateTable();
        }

        protected virtual void Logout()
        {
            _userService.TruncateTable();
        }
    }
}
