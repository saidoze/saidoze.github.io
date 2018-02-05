using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using quicky.bakkers.models;
using quicky.bakkers.services.Services;
using quicky.bakkers.Views.Settings.MatchSettings;
using quicky.bakkers.BasePages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace quicky.bakkers.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResultsContentPage : AuthorizedContentPage
    {
        private MatchdayService _matchdayService;
        private PlayerService _playeryService;
        private MatchService _matchService;
        private TeamService _teamService;
        private List<Match> _matches;
        private List<Player> _players;
        private List<Matchday> _matchdays;
        private List<Team> _teams;

        public ResultsContentPage ()
		{
			InitializeComponent ();
            BindingContext = this;
            IsBusy = false;

            _matchdayService = new MatchdayService();
            _teamService = new TeamService();
            _matchService = new MatchService();
            _playeryService = new PlayerService();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            matchDayPicker.ItemsSource = new List<Matchday>();

            if (!IsBusy)
            {
                this.IsBusy = true;
                Task.Run(() =>
                {
                    LoadData();
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _matches.ForEach(m =>
                        {
                            m.Players = _players;
                            m.Team1 = _teams.Where(t => t.Key == m.Team1Key).FirstOrDefault();
                            m.Team2 = _teams.Where(t => t.Key == m.Team2Key).FirstOrDefault();
                            m.Matchday = _matchdays.Where(ma => ma.Key == m.MatchdayKey).FirstOrDefault();
                        });

                        matchDayPicker.ItemsSource = _matchdays;
                        LoadDefaultMatchday();
                        AddButton.Text = (_isAuthenticated ? "Toevoegen" : "");
                        this.IsBusy = false;
                    });
                });
            }
        }

        private void LoadDefaultMatchday()
        {
            var defaultMatchday = _matchdays.Where(m => m.Date < DateTime.Now.AddDays(1)).OrderByDescending(m => m.Date).FirstOrDefault();
            matchDayPicker.SelectedItem = defaultMatchday;
        }

        private void LoadData()
        {
            _matchdays = _matchdayService.GetAllItems();
            _teams = _teamService.GetAllItems();
            _matches = _matchService.GetAllItems();
            _players = _playeryService.GetAllItems();
        }

        private void MatchDayPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
                LoadResults((picker.SelectedItem as Matchday).Key);
            else
                LoadResults(null);
        }

        /// <summary>
        /// Filter on results of certain matchday 
        /// </summary>
        /// <param name="key"></param>
        private void LoadResults(string matchdayKey)
        {
            //TODO order by something?
            if (!string.IsNullOrEmpty(matchdayKey))
                resultList.ItemsSource = _matches.Where(m => m.MatchdayKey == matchdayKey);
            else
                resultList.ItemsSource = null;
        }

        public async Task OnAddButtonClicked()
        {
            if (_isAuthenticated)
                await Navigation.PushAsync(new AddMatchContentPage());
        }
    }
}