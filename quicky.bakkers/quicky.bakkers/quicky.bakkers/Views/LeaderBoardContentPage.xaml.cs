using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using quicky.bakkers.BasePages;
using quicky.bakkers.models;
using quicky.bakkers.services.Services;
using quicky.bakkers.Views.Settings.PlayerSettings;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace quicky.bakkers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeaderBoardContentPage : AuthorizedContentPage
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

        public LeaderBoardContentPage()
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
            if (MainSettings.LastVisitedContentPage == ContentPageEnum.PlayerMatchesOverview)
            {
                MainSettings.LastVisitedContentPage = ContentPageEnum.Leaderboard;
                return;
            }
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
            var task1 = _teamService.GetAllItemsAsync();
            var task2 = _matchService.GetAllItemsAsync();
            var task3 = _playerService.GetAllItemsAsync();
            var task4 = _matchdayService.GetAllItemsAsync();

            Task.WaitAll(task1, task2, task3, task4);

            _teams = task1.Result;
            _matches = task2.Result;
            _players = task3.Result;
            _matchdays = task4.Result;

            //_teams = _teamService.GetAllItems();
            //_matches = _matchService.GetAllItems();
            //_players = _playerService.GetAllItems();
            //_matchdays = _matchdayService.GetAllItems();

            CalculatePlayerResults();
        }

        private void CalculatePlayerResults()
        {
                var cp = new Player();
            try
            {
                var amountOfMatchdays = _matchdays.Where(m => m.Closed).Count();
                var allMatches = (from m in _matches
                                  join md in _matchdays.Where(m => m.Closed) on m.MatchdayKey equals md.Key
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
                    cp = player;
                    if(cp.Name == "Tobias")
                    {
                    }
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
                    var matchesWonCount = matchesWon.Count;
                    var goalsForWhenWon = matchesWonCount * 11; //matchesWon.Sum(m => m.scoreTeam1) + matchesWon.Sum(m => m.scoreTeam2);

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
                    var matchesDrawCount = matchesDraw.Count;
                    var goalsForWhenDraw = matchesDrawCount * 10;

                    //Matches lost
                    var matchesLost = (from a in allMatches
                                       where (a.match.ScoreTeam1 < 10 && ((a.team1.Player1Key == player.Key && a.team1.Player1AllowPoints) || (a.team1.Player2Key == player.Key && a.team1.Player2AllowPoints)))
                                       || (a.match.ScoreTeam2 < 10 && ((a.team2.Player1Key == player.Key && a.team2.Player1AllowPoints) || (a.team2.Player2Key == player.Key && a.team2.Player2AllowPoints)))
                                       select new
                                       {
                                           scoreTeam1 = (a.match.ScoreTeam1 < 10 ? a.match.ScoreTeam1 : 0),
                                           scoreTeam2 = (a.match.ScoreTeam2 < 10 ? a.match.ScoreTeam2 : 0)
                                       }).ToList();
                    var matchesLostCount = matchesLost.Count;
                    var goalsForWhenLost = matchesLost.Sum(m => m.scoreTeam1) + matchesLost.Sum(m => m.scoreTeam2);

                    //presence points
                    var presencePoints = 0;
                    _matchdays.Where(m => m.Closed).ToList().ForEach(m =>
                    {
                        if (m.PresencePlayerKeys.Contains(player.Key))
                            presencePoints = presencePoints + 2;
                    });
                    presult.PresencePoints = presencePoints;

                    presult.Points = (matchesWonCount * 3) + (matchesDrawCount) + presult.PresencePoints;
                    presult.GoalsFor = goalsForWhenWon + goalsForWhenDraw + goalsForWhenLost;
                    presult.GoalsAgainst = (matchesDrawCount * 10) + (matchesLostCount * 11) + (goalsAgainstWhenWon);

                    presult.MatchesPlayed = matchesWonCount + matchesDrawCount + matchesLostCount;
                    presult.MatchesWon = matchesWonCount;
                    presult.MatchesDrawed = matchesDrawCount;
                    presult.MatchesLost = matchesLostCount;

                    var games = amountOfMatchdays * 3;
                    var p = presult.MatchesPlayed;
                    if (p == 0)
                        presult.MatchdaysToCatchUp = new String('*', amountOfMatchdays);
                    else
                    {
                        var amountToCatchUp = ((amountOfMatchdays * 3) - p) / 3;

                        if (amountToCatchUp > 0)
                            presult.MatchdaysToCatchUp = new String('*', amountToCatchUp);
                    }

                    _playerResults.Add(presult);
                }

                _playerResults = _playerResults.OrderByDescending(p => p.Points).ThenByDescending(p => p.GoalsFor).ToList();
                //fix order
                var order = 1;
                _playerResults.ForEach(p =>
                {
                    p.Order = string.Format("{0}.", order);
                    order++;
                });
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }

            //leaderboardList.ItemsSource = _playerResults;
        }

        public async Task OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (e.SelectedItem != null)
                {
                    await Navigation.PushAsync(new PlayerMatchesContentPage(e.SelectedItem as PlayerResult, _teams, _matches, _players, _matchdays));
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}