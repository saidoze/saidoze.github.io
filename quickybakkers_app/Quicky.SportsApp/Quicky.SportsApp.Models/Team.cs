using Quicky.SportsApp.Models.Interfaces;
using System;

namespace Quicky.SportsApp.Models
{
    public class Team : IFirebaseModelBase<Team>
    {
        public string Key { get; set; }
        public string Player1Key { get; set; }
        public bool Player1AllowPoints { get; set; }
        public string Player2Key { get; set; }
        public bool Player2AllowPoints { get; set; }
        //helpers
        public Team WithKey(string key)
        {
            this.Key = key;
            return this;
        }
    }
}
