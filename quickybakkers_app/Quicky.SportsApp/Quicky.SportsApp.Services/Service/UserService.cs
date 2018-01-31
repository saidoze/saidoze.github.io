using Quicky.SportsApp.Models;
using Quicky.SportsApp.Services.Providers;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicky.SportsApp.Services.Service
{
    public class UserService// : IUserProvider
    {
        //https://github.com/praeclarum/sqlite-net
        private SQLiteConnection _syncConnection;

        public UserService()
        {
            _syncConnection = new SQLiteConnection(Constants.DatabaseFile);
            var createTableResult = _syncConnection.CreateTable<User>();
        }

        public User SaveUser(User user)
        {
            try
            {
                var result = _syncConnection.Insert(user);
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<User> LoadActiveUsers()
        {
            try
            {
                var query = _syncConnection.Table<User>().Where(u => u.ExpiryDate > DateTime.Now);
                var result = query.ToList();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool TruncateTable()
        {
            try
            {
                var result = _syncConnection.DeleteAll<User>();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
