using quicky.bakkers.models.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quicky.bakkers.models
{
    public class User
    {
        [PrimaryKey]
        public string Username { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime ExpiryDate { get; set; }
        public UsergroupEnum usergroupEnum { get; set; }
    }
}
