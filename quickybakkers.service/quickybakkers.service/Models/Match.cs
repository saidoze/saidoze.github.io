using System;

namespace Quickybakkers.Service.Models
{
    public class Match
    {
        public int Id { get; set; }
        public int Team1Id { get; set; }
        public int Team2Id { get; set; }
        public int SpeeldagId { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public DateTime LastUpdateDate { get; set; }

        public int Team1Speler1Id { get; set; }
        public int Team1Speler2Id { get; set; }
        public int Team2Speler1Id { get; set; }
        public int Team2Speler2Id { get; set; }
        public bool Team1Speler1PuntenToelaten { get; set; }
        public bool Team1Speler2PuntenToelaten { get; set; }
        public bool Team2Speler1PuntenToelaten { get; set; }
        public bool Team2Speler2PuntenToelaten { get; set; }
    }
}
