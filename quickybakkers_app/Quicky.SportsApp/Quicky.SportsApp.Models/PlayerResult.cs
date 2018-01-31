using System;

namespace Quicky.SportsApp.Models
{
    public class PlayerResult 
    {
        public string PlayerKey { get; set; }

        public string Order { get; set; }
        public int MatchesPlayed { get; set; }
        public int MatchesWon { get; set; }
        public int MatchesDrawed { get; set; }
        public int MatchesLost { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int PresencePoints { get; set; }
        public int Points { get; set; }

        //helper
        public string PlayerName { get; set; }
    }
}
