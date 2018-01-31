using Quicky.SportsApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quicky.SportsApp.Services.Providers
{
    public interface IUserProvider
    {
        Task<User> LoadUserAsync();
        Task<User> SaveUserAsync(User user);
    }
}
