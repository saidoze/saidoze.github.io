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
    }
}
