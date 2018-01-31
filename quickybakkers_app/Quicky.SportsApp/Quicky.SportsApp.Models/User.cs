using Quicky.SportsApp.Models.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicky.SportsApp.Models
{
    public class User
    {
        [PrimaryKey]
        public string Username { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime ExpiryDate { get; set; }
        public UsergroupEnum usergroupEnumc { get; set; }
    }
}
