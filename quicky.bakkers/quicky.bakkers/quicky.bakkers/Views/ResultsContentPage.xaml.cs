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
        private PlayerService _playerService;
        private MatchService _matchService;
        private TeamService _teamService;
        private List<Match> _matches;
        private List<Player> _players;
        private List<Matchday> _matchdays;
        private List<Team> _teams;

        public ResultsContentPage()
        {
            InitializeComponent();
            BindingContext = this;
            IsBusy = false;

            _matchdayService = new MatchdayService();
            _teamService = new TeamService();
            _matchService = new MatchService();
            _playerService = new PlayerService();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            matchDayPicker.ItemsSource = new List<Matchday>();

            AddButton.Text = (_isAuthenticated ? "Toevoegen" : "");

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

        private async Task LoadData()
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

        public async Task OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (_isAuthenticated)
                {
                    if (e.SelectedItem != null)
                    {
                        await Navigation.PushAsync(new EditMatchContentPage(e.SelectedItem as Match));
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}