using System;
using System.Collections.Generic;
using System.Text;
using Quicky.SportsApp.Services.Service;
using Xamarin.Forms;

namespace Quicky.SportsApp
{
    public class LoginContentPage : ContentPage
    {
        protected UserService _userService;
        protected bool _isAuthenticated
        {
            get {
                var users = _userService.LoadActiveUsers();
                return (users.Count == 1);
            }
        }
        
        public LoginContentPage()
        {
            _userService = new UserService();
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
