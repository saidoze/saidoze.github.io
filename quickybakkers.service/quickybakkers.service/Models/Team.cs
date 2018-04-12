using System;

namespace Quickybakkers.Service.Models
{
    public class Team
    {
        public int Id { get; set; }
        public int Speler1Id { get; set; }
        public int Speler2Id { get; set; }
        public int Speler1PuntenToelaten { get; set; }
        public int Speler2PuntenToelaten { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
