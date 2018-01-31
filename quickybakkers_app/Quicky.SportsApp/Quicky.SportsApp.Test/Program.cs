using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quicky.SportsApp.Models;
using Quicky.SportsApp.Services.Service;
using Quicky.SportsApp.Test.Firebase;

namespace Quicky.SportsApp.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //PlayerTest();
            //MatchdayTest();
            CalculatePlayerResults();
        }

        private static void CalculatePlayerResults()
        {
            var _matchService = new MatchService();
            var _teamService = new TeamService();
            var _playerService = new PlayerService();
            var _teams = _teamService.GetAllItems();
            var _matches = _matchService.GetAllItems();
            var _players = _playerService.GetAllItems();

            var playerResults = new List<PlayerResult>();

            var allMatches = (from m in _matches
                              join t1 in _teams on m.Team1Key equals t1.Key
                              join t2 in _teams on m.Team2Key equals t2.Key
                              select new
                              {
                                  match = m,
                                  team1 = t1,
                                  team2 = t2
                              }).ToList();

            //create playerresults for every player
            foreach (var player in _players)
            {
                var presult = new PlayerResult();
                presult.PlayerKey = player.Key;
                presult.PlayerName = player.Name;

                //Matches won
                var matchesWon = (from a in allMatches
                                  where (a.match.ScoreTeam1 == 11 && (a.team1.Player1Key == player.Key || a.team1.Player2Key == player.Key))
                                  || (a.match.ScoreTeam2 == 11 && (a.team2.Player1Key == player.Key || a.team2.Player2Key == player.Key))
                                  select new
                                  {
                                      scoreTeam1 = (a.match.ScoreTeam1 == 11 ? a.match.ScoreTeam1 : 0),
                                      scoreTeam2 = (a.match.ScoreTeam2 == 11 ? a.match.ScoreTeam2 : 0)
                                  }).ToList();
                var goalsForWhenWon = matchesWon.Count * 11; //matchesWon.Sum(m => m.scoreTeam1) + matchesWon.Sum(m => m.scoreTeam2);

                //Matches won, but get other teams goals
                var matchesWonOtherTeamGoals = (from a in allMatches
                                  where (a.match.ScoreTeam1 == 11 && (a.team1.Player1Key == player.Key || a.team1.Player2Key == player.Key))
                                  || (a.match.ScoreTeam2 == 11 && (a.team2.Player1Key == player.Key || a.team2.Player2Key == player.Key))
                                  select new
                                  {
                                      scoreTeam1 = (a.match.ScoreTeam1 == 11 ? a.match.ScoreTeam2 : 0),
                                      scoreTeam2 = (a.match.ScoreTeam2 == 11 ? a.match.ScoreTeam1 : 0)
                                  }).ToList();
                var goalsAgainstWhenWon = matchesWonOtherTeamGoals.Sum(m => m.scoreTeam1) + matchesWonOtherTeamGoals.Sum(m => m.scoreTeam2);

                //Matches drawn
                var matchesDraw = (from a in allMatches
                                   where (a.match.ScoreTeam1 == 10 && (a.team1.Player1Key == player.Key || a.team1.Player2Key == player.Key))
                                   || (a.match.ScoreTeam2 == 10 && (a.team2.Player1Key == player.Key || a.team2.Player2Key == player.Key))
                                   select new
                                   {
                                       scoreTeam1 = (a.match.ScoreTeam1 == 10 ? a.match.ScoreTeam1 : 0),
                                       scoreTeam2 = (a.match.ScoreTeam2 == 10 ? a.match.ScoreTeam2 : 0)
                                   }).ToList();
                var goalsForWhenDraw = matchesDraw.Count * 10;

                //Matches lost
                var matchesLost = (from a in allMatches
                                   where (a.match.ScoreTeam1 < 10 && (a.team1.Player1Key == player.Key || a.team1.Player2Key == player.Key))
                                   || (a.match.ScoreTeam2 < 10 && (a.team2.Player1Key == player.Key || a.team2.Player2Key == player.Key))
                                   select new
                                   {
                                       scoreTeam1 = (a.match.ScoreTeam1 < 10 ? a.match.ScoreTeam1 : 0),
                                       scoreTeam2 = (a.match.ScoreTeam2 < 10 ? a.match.ScoreTeam2 : 0)
                                   }).ToList();
                var goalsForWhenLost = matchesLost.Sum(m => m.scoreTeam1) + matchesLost.Sum(m => m.scoreTeam2);

                var points = (matchesWon.Count * 3) + (matchesDraw.Count);
                var goalsFor = goalsForWhenWon + goalsForWhenDraw + goalsForWhenLost;
                var goalsAgainst = (matchesDraw.Count * 10) + (matchesLost.Count * 11) + (goalsAgainstWhenWon);
                
                playerResults.Add(presult);
            }

            //leaderboardList.
        }

        private static void MatchdayTest()
        {
            var service = new MatchdayService();
            var allItems = service.GetAllItems();

            var newItem = service.SaveItem(new Matchday()
            {
                Date = new DateTime(2017, 1, 11),
                Closed = false,
                Number = 1
            }).Result;

            newItem.Closed = true;
            newItem = service.SaveItem(newItem).Result;

            service.DeleteItemByKey(newItem.Key).Wait();
        }

        private static void PlayerTest()
        {
            var service = new PlayerService();
            var allItems = service.GetAllItems();

            var newItem = service.SaveItem(new Player()
            {
                Name = "Tobias"
            }).Result;

            newItem.Name = "Tobias2";
            newItem = service.SaveItem(newItem).Result;

            service.DeleteItemByKey(newItem.Key).Wait();
        }
    }

}
