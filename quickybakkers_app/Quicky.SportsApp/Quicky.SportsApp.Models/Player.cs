using Quicky.SportsApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicky.SportsApp.Models
{
    public class Player : IFirebaseModelBase<Player>
    {
        public string Name { get; set; }
        public string Key { get; set; }
        
        public Player WithKey(string key)
        {
            this.Key = key;
            return this;
        }
    }
}
