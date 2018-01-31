using Quicky.SportsApp.Models;
using Quicky.SportsApp.Services.Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Quicky.SportsApp
{
    public partial class LeaderBoardPage : ContentPage
    {
        private MatchdayService _matchdayService;
        private MatchService _matchService;
        private TeamService _teamService;
        private PlayerService _playerService;
        private List<Team> _teams;
        private List<Match> _matches;
        private List<Player> _players;
        private List<Matchday> _matchdays;
        private List<PlayerResult> _playerResults;

        public LeaderBoardPage()
        {
            InitializeComponent();
            BindingContext = this;
            this.IsBusy = false;
            _teamService = new TeamService();
            _matchService = new MatchService();
            _playerService = new PlayerService();
            _matchdayService = new MatchdayService();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            leaderboardList.ItemsSource = new List<PlayerResult>();

            if (!IsBusy)
            {
                this.IsBusy = true;
                Task.Run(() =>
                {
                    LoadData();
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        leaderboardList.ItemsSource = _playerResults;
                        this.IsBusy = false;
                    });
                });
            }
        }
        
        private void LoadData()
        {
            _teams = _teamService.GetAllItems();
            _matches = _matchService.GetAllItems();
            _players = _playerService.GetAllItems();
            _matchdays = _matchdayService.GetAllItems();

            CalculatePlayerResults();
        }

        public async Task OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            /*if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new EditPlayerPage(e.SelectedItem as Player));
            }*/
        }
        
        private void CalculatePlayerResults()
        {
            var allMatches = (from m in _matches
                              join t1 in _teams on m.Team1Key equals t1.Key
                              join t2 in _teams on m.Team2Key equals t2.Key
                              select new
                              {
                                  match = m,
                                  team1 = t1,
                                  team2 = t2
                              }).ToList();

            _playerResults = new List<PlayerResult>();
            //create playerresults for every player
            foreach (var player in _players)
            {
                var presult = new PlayerResult();
                presult.PlayerKey = player.Key;
                presult.PlayerName = player.Name;

                //Matches won
                var matchesWon = (from a in allMatches
                                  where (a.match.ScoreTeam1 == 11 && ((a.team1.Player1Key == player.Key && a.team1.Player1AllowPoints) || (a.team1.Player2Key == player.Key && a.team1.Player2AllowPoints)))
                                  || (a.match.ScoreTeam2 == 11 && ((a.team2.Player1Key == player.Key && a.team2.Player1AllowPoints) || (a.team2.Player2Key == player.Key && a.team2.Player2AllowPoints)))
                                  select new
                                  {
                                      scoreTeam1 = (a.match.ScoreTeam1 == 11 ? a.match.ScoreTeam1 : 0),
                                      scoreTeam2 = (a.match.ScoreTeam2 == 11 ? a.match.ScoreTeam2 : 0)
                                  }).ToList();
                var goalsForWhenWon = matchesWon.Count * 11; //matchesWon.Sum(m => m.scoreTeam1) + matchesWon.Sum(m => m.scoreTeam2);

                //Matches won, but get other teams goals
                var matchesWonOtherTeamGoals = (from a in allMatches
                                                where (a.match.ScoreTeam1 == 11 && ((a.team1.Player1Key == player.Key && a.team1.Player1AllowPoints) || (a.team1.Player2Key == player.Key && a.team1.Player2AllowPoints)))
                                                || (a.match.ScoreTeam2 == 11 && ((a.team2.Player1Key == player.Key && a.team2.Player1AllowPoints) || (a.team2.Player2Key == player.Key && a.team2.Player2AllowPoints)))
                                                select new
                                                {
                                                    scoreTeam1 = (a.match.ScoreTeam1 == 11 ? a.match.ScoreTeam2 : 0),
                                                    scoreTeam2 = (a.match.ScoreTeam2 == 11 ? a.match.ScoreTeam1 : 0)
                                                }).ToList();
                var goalsAgainstWhenWon = matchesWonOtherTeamGoals.Sum(m => m.scoreTeam1) + matchesWonOtherTeamGoals.Sum(m => m.scoreTeam2);

                //Matches drawn
                var matchesDraw = (from a in allMatches
                                   where (a.match.ScoreTeam1 == 10 && ((a.team1.Player1Key == player.Key && a.team1.Player1AllowPoints) || (a.team1.Player2Key == player.Key && a.team1.Player2AllowPoints)))
                                   || (a.match.ScoreTeam2 == 10 && ((a.team2.Player1Key == player.Key && a.team2.Player1AllowPoints) || (a.team2.Player2Key == player.Key && a.team2.Player2AllowPoints)))
                                   select new
                                   {
                                       scoreTeam1 = (a.match.ScoreTeam1 == 10 ? a.match.ScoreTeam1 : 0),
                                       scoreTeam2 = (a.match.ScoreTeam2 == 10 ? a.match.ScoreTeam2 : 0)
                                   }).ToList();
                var goalsForWhenDraw = matchesDraw.Count * 10;

                //Matches lost
                var matchesLost = (from a in allMatches
                                   where (a.match.ScoreTeam1 < 10 && ((a.team1.Player1Key == player.Key && a.team1.Player1AllowPoints) || (a.team1.Player2Key == player.Key && a.team1.Player2AllowPoints)))
                                   || (a.match.ScoreTeam2 < 10 && ((a.team2.Player1Key == player.Key && a.team2.Player1AllowPoints) || (a.team2.Player2Key == player.Key && a.team2.Player2AllowPoints)))
                                   select new
                                   {
                                       scoreTeam1 = (a.match.ScoreTeam1 < 10 ? a.match.ScoreTeam1 : 0),
                                       scoreTeam2 = (a.match.ScoreTeam2 < 10 ? a.match.ScoreTeam2 : 0)
                                   }).ToList();
                var goalsForWhenLost = matchesLost.Sum(m => m.scoreTeam1) + matchesLost.Sum(m => m.scoreTeam2);

                //presence points
                var presencePoints = 0;
                _matchdays.Where(m => m.Closed).ToList().ForEach(m =>
                {
                    if (m.PresencePlayerKeys.Contains(player.Key))
                        presencePoints = presencePoints + 2;
                });
                presult.PresencePoints = presencePoints;

                presult.Points = (matchesWon.Count * 3) + (matchesDraw.Count) + presult.PresencePoints;
                presult.GoalsFor = goalsForWhenWon + goalsForWhenDraw + goalsForWhenLost;
                presult.GoalsAgainst = (matchesDraw.Count * 10) + (matchesLost.Count * 11) + (goalsAgainstWhenWon);

                presult.MatchesPlayed = matchesWon.Count + matchesDraw.Count + matchesLost.Count;
                presult.MatchesWon = matchesWon.Count;
                presult.MatchesDrawed = matchesDraw.Count;
                presult.MatchesLost = matchesLost.Count;

                _playerResults.Add(presult);
            }

            _playerResults = _playerResults.OrderByDescending(p => p.Points).OrderByDescending(p => p.GoalsFor).ToList();
            //fix order
            var order = 1;
            _playerResults.ForEach(p =>
            {
                p.Order = string.Format("{0}.", order);
                order++;
            });

            //leaderboardList.ItemsSource = _playerResults;
        }

    }
}