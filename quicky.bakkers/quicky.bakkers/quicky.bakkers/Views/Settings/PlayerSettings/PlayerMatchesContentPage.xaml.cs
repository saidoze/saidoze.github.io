using quicky.bakkers.models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace quicky.bakkers.Views.Settings.PlayerSettings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerMatchesContentPage : ContentPage
    {
        private string _playerKey;
        private List<Team> _teams;
        private List<Matchday> _matchdays;
        private List<Match> _matches;
        private List<Player> _players;
        private List<PlayerMatchResult> _playerMatchResults = new List<PlayerMatchResult>();

        public PlayerMatchesContentPage(PlayerResult playerResult,
            List<Team> teams, List<Match> matches, List<Player> players, List<Matchday> matchdays)
        {
            InitializeComponent();
            BindingContext = this;
            this.IsBusy = false;
            _playerKey = playerResult.PlayerKey;

            _teams = teams;
            _matches = matches;
            _players = players;
            _matchdays = matchdays;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MainSettings.LastVisitedContentPage = ContentPageEnum.PlayerMatchesOverview;
            playerMatchesList.ItemsSource = new List<PlayerMatchResult>();

            if (!IsBusy)
            {
                this.IsBusy = true;
                Task.Run(() =>
                {
                    LoadData();
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        playerMatchesList.ItemsSource = _playerMatchResults.OrderByDescending(pmr => pmr.MatchdayNumber).ToList();
                        this.IsBusy = false;
                    });
                });
            }
        }

        private void LoadData()
        {
            CalculatePlayerMatches();
        }

        private void CalculatePlayerMatches()
        {
            try
            {
                var matchesWon = 0;
                var matchesDraw = 0;
                var matchesLost = 0;
                var matchesGoalsFor = 0;
                var matchesGoalsAgainst = 0;

                foreach (var matchday in _matchdays)
                {
                    var matches = _matches.Where(m => m.MatchdayKey == matchday.Key).ToList();

                    var matchesForPlayer = matches.Where(m => _teams.Where(t => t.Key == m.Team1Key).First().Player1Key == _playerKey
                            || _teams.Where(t => t.Key == m.Team1Key).First().Player2Key == _playerKey
                            || _teams.Where(t => t.Key == m.Team2Key).First().Player1Key == _playerKey
                            || _teams.Where(t => t.Key == m.Team2Key).First().Player2Key == _playerKey).ToList();

                    foreach (var match in matchesForPlayer)
                    {
                        var pmr = new PlayerMatchResult();
                        pmr.MatchdayNumber = matchday.Number;
                        pmr.Score = match.Result;
                        var team1player1 = _players.Where(p => p.Key == _teams.Where(t => t.Key == match.Team1Key).First().Player1Key).FirstOrDefault();
                        var team1player2 = _players.Where(p => p.Key == _teams.Where(t => t.Key == match.Team1Key).First().Player2Key).FirstOrDefault();
                        var team2player1 = _players.Where(p => p.Key == _teams.Where(t => t.Key == match.Team2Key).First().Player1Key).FirstOrDefault();
                        var team2player2 = _players.Where(p => p.Key == _teams.Where(t => t.Key == match.Team2Key).First().Player2Key).FirstOrDefault();

                        pmr.Team1Player1 = team1player1.Name;
                        pmr.Team1Player2 = team1player2.Name;
                        pmr.Team2Player1 = team2player1.Name;
                        pmr.Team2Player2 = team2player2.Name;

                        pmr.Team1Player1 += (_teams.Where(t => t.Key == match.Team1Key).First().Player1AllowPoints ? "" : " *");
                        pmr.Team1Player2 += (_teams.Where(t => t.Key == match.Team1Key).First().Player2AllowPoints ? "" : " *");
                        pmr.Team2Player1 += (_teams.Where(t => t.Key == match.Team2Key).First().Player1AllowPoints ? "" : " *");
                        pmr.Team2Player2 += (_teams.Where(t => t.Key == match.Team2Key).First().Player2AllowPoints ? "" : " *");

                        pmr.AmITeam1Player1 = team1player1.Key == _playerKey;
                        pmr.AmITeam1Player2 = team1player2.Key == _playerKey;
                        pmr.AmITeam2Player1 = team2player1.Key == _playerKey;
                        pmr.AmITeam2Player2 = team2player2.Key == _playerKey;

                        //calculate totals
                        if(((pmr.AmITeam1Player1 || pmr.AmITeam1Player2) && match.ScoreTeam1 == 11)
                            || ((pmr.AmITeam2Player1 || pmr.AmITeam2Player2) && match.ScoreTeam2 == 11))
                        {
                            matchesWon++;
                            matchesGoalsFor += 11;
                            matchesGoalsAgainst += ((pmr.AmITeam1Player1 || pmr.AmITeam1Player2) ? match.ScoreTeam2 : match.ScoreTeam1);
                        }
                        if (((pmr.AmITeam1Player1 || pmr.AmITeam1Player2) && match.ScoreTeam1 == 10)
                            || ((pmr.AmITeam2Player1 || pmr.AmITeam2Player2) && match.ScoreTeam2 == 10))
                        {
                            matchesDraw++;
                            matchesGoalsFor += 10;
                            matchesGoalsAgainst += 10;
                        }
                        if(((pmr.AmITeam1Player1 || pmr.AmITeam1Player2) && match.ScoreTeam1 < 10)
                            || ((pmr.AmITeam2Player1 || pmr.AmITeam2Player2) && match.ScoreTeam2 < 10))
                        {
                            matchesLost++;
                            matchesGoalsFor += ((pmr.AmITeam1Player1 || pmr.AmITeam1Player2) ? match.ScoreTeam1 : match.ScoreTeam2);
                            matchesGoalsAgainst += 11;
                        }
                        
                        _playerMatchResults.Add(pmr);
                    }

                    if (matchesForPlayer.Count == 0)
                        _playerMatchResults.Add(new PlayerMatchResult()
                        {
                            MatchdayNumber = matchday.Number,
                            Score = "AFWEZIG"
                        });
                }
                
                AmountWonLabel.Text = matchesWon.ToString();
                AmountDrawLabel.Text = matchesDraw.ToString();
                AmountLostLabel.Text = matchesLost.ToString();
                AmountGoalsForLabel.Text = matchesGoalsFor.ToString();
                AmountGoalsAgainstLabel.Text = matchesGoalsAgainst.ToString();
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
