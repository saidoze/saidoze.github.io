using System;

namespace quicky.bakkers.models
{
    public class PlayerMatchResult
    {
        public int MatchdayNumber { get; set; }
        public string Score { get; set; }
        public string Team1Player1 { get; set; }
        public string Team1Player2 { get; set; }
        public string Team2Player1 { get; set; }
        public string Team2Player2 { get; set; }

        //helper
        public bool AmITeam1Player1 { get; set; }
        public bool AmITeam1Player2 { get; set; }
        public bool AmITeam2Player1 { get; set; }
        public bool AmITeam2Player2 { get; set; }
    }
}
