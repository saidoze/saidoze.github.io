using Quicky.SportsApp.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace Quicky.SportsApp.Models
{
    public class Matchday : IFirebaseModelBase<Matchday>
    {
        public string Key { get; set; }
        public DateTime Date { get; set; }
        public int Number { get; set; }
        public bool Closed { get; set; }
        public List<string> PresencePlayerKeys { get; set; }
        //Helpers
        public string DateAsString { get { return string.Format("{0} ({1})", Number, Date.ToString("dd/MM/yyyy")); } }

        public Matchday WithKey(string key)
        {
            this.Key = key;
            return this;
        }
    }
}
