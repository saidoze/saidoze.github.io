﻿using System;

namespace quicky.bakkers.models
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
        public string MatchdaysToCatchUp { get; set; }
        public string PlayerName { get; set; }
        public string PlayerNameWithCatchups => string.Format("{0} {1}", PlayerName, MatchdaysToCatchUp);
    }
}
