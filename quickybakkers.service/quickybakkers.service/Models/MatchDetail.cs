using System;

namespace Quickybakkers.Service.Models
{
    public class MatchDetail
    {
        public int MatchId { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public int SpeeldagId { get; set; }
        public int SpeeldagGesloten { get; set; }
        public string GroupHash { get; set; }

        public int Team1Speler1Id { get; set; }
        public string Team1Speler1Naam { get; set; }
        public int Team1Speler1Ptn { get; set; }
        public int Team1Speler1Win { get; set; }
        public int Team1Speler1Gelijk { get; set; }
        public int Team1Speler1Verloren { get; set; }

        public int Team1Speler2Id { get; set; }
        public string Team1Speler2Naam { get; set; }
        public int Team1Speler2Ptn { get; set; }
        public int Team1Speler2Win { get; set; }
        public int Team1Speler2Gelijk { get; set; }
        public int Team1Speler2Verloren { get; set; }

        public int Team2Speler1Id { get; set; }
        public string Team2Speler1Naam { get; set; }
        public int Team2Speler1Ptn { get; set; }
        public int Team2Speler1Win { get; set; }
        public int Team2Speler1Gelijk { get; set; }
        public int Team2Speler1Verloren { get; set; }

        public int Team2Speler2Id { get; set; }
        public string Team2Speler2Naam { get; set; }
        public int Team2Speler2Ptn { get; set; }
        public int Team2Speler2Win { get; set; }
        public int Team2Speler2Gelijk { get; set; }
        public int Team2Speler2Verloren { get; set; }
    }
}
