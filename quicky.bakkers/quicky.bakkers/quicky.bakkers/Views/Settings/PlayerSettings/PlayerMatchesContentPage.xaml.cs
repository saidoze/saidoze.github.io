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
                        pmr.Team1Player1 = _players.Where(p => p.Key == _teams.Where(t => t.Key == match.Team1Key).First().Player1Key).First().Name;
                        pmr.Team1Player2 = _players.Where(p => p.Key == _teams.Where(t => t.Key == match.Team1Key).First().Player2Key).First().Name;
                        pmr.Team2Player1 = _players.Where(p => p.Key == _teams.Where(t => t.Key == match.Team2Key).First().Player1Key).First().Name;
                        pmr.Team2Player2 = _players.Where(p => p.Key == _teams.Where(t => t.Key == match.Team2Key).First().Player2Key).First().Name;

                        pmr.Team1Player1 += (_teams.Where(t => t.Key == match.Team1Key).First().Player1AllowPoints ? "" : " *");
                        pmr.Team1Player2 += (_teams.Where(t => t.Key == match.Team1Key).First().Player2AllowPoints ? "" : " *");
                        pmr.Team2Player1 += (_teams.Where(t => t.Key == match.Team2Key).First().Player1AllowPoints ? "" : " *");
                        pmr.Team2Player2 += (_teams.Where(t => t.Key == match.Team2Key).First().Player2AllowPoints ? "" : " *");

                        _playerMatchResults.Add(pmr);
                    }

                    if (matchesForPlayer.Count == 0)
                        _playerMatchResults.Add(new PlayerMatchResult()
                        {
                            MatchdayNumber = matchday.Number,
                            Score = "AFWEZIG"
                        });
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
