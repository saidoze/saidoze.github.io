using quicky.bakkers.models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace quicky.bakkers.services.Providers
{
    public interface IUserProvider
    {
        Task<User> LoadUserAsync();
        Task<User> SaveUserAsync(User user);
    }
}
