﻿using Quicky.SportsApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quicky.SportsApp.Models
{
    public class Match : IFirebaseModelBase<Match>
    {
        public Match ()
        {
            Players = new List<Player>();
        }

        public string Key { get; set; }
        public string MatchdayKey { get; set; }

        public string Team1Key { get; set; }
        public string Team2Key { get; set; }
        public int ScoreTeam1 { get; set; }
        public int ScoreTeam2 { get; set; }

        //helpers
        public List<Player> Players { get; set; }
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }
        public Matchday Matchday { get; set; }
        public Match WithKey(string key)
        {
            this.Key = key;
            return this;
        }
        public string Player1Name { get { return String.Format("{0} {1}", Players.Where(p => p.Key == (Team1?.Player1Key ?? "")).SingleOrDefault()?.Name, (Team1.Player1AllowPoints ? "" : "*")); } }
        public string Player2Name { get { return String.Format("{0} {1}", Players.Where(p => p.Key == (Team1?.Player2Key ?? "")).SingleOrDefault()?.Name, (Team1.Player2AllowPoints ? "" : "*")); } }
        public string Player3Name { get { return String.Format("{0} {1}", Players.Where(p => p.Key == (Team2?.Player1Key ?? "")).SingleOrDefault()?.Name, (Team2.Player1AllowPoints ? "" : "*")); } }
        public string Player4Name { get { return String.Format("{0} {1}", Players.Where(p => p.Key == (Team2?.Player2Key ?? "")).SingleOrDefault()?.Name, (Team2.Player2AllowPoints ? "" : "*")); } }
        public string Result { get { return string.Format("{0} - {1}", ScoreTeam1, ScoreTeam2); } }
    }
}