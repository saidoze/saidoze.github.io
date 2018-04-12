using System;

namespace Quickybakkers.Service.Models
{
    public class Speler
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public DateTime LastUpdateDate { get; set; }

        public override string ToString()
        {
            return Naam;
        }
    }
}
