using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using quicky.bakkers.services.Services;
using quicky.bakkers.models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace quicky.bakkers.Views.Settings.MatchdaySettings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditMatchdayContentPage : ContentPage
    {
        private MatchdayService _matchdayService;
        private MatchService _matchService;
        private TeamService _teamService;
        private Matchday _matchday;

        public EditMatchdayContentPage(Matchday matchday)
        {
            InitializeComponent();
            BindingContext = this;
            IsBusy = false;

            _matchdayService = new MatchdayService();
            _matchService = new MatchService();
            _teamService = new TeamService();
            _matchday = matchday;
            LoadData();
        }

        private void LoadData()
        {
            dateEntry.Date = _matchday.Date;
            numberEntry.Text = _matchday.Number.ToString();
            //deleteButton.IsEnabled = !_matchday.Closed;
            updateButton.IsEnabled = !_matchday.Closed;
            updateButton.IsEnabled = !_matchday.Closed;
            closeButton.IsVisible = !_matchday.Closed;
            labelAfgesloten.IsVisible = _matchday.Closed;
        }

        public async void OnCloseButtonClicked(object sender, EventArgs e)
        {
            if (!IsBusy)
            {
                var answer = await DisplayAlert("Afsluiten", "Bent u zeker?", "Ja", "Neen");
                if (answer)
                {
                    this.IsBusy = true;
                    Task.Run(() =>
                    {
                        //        //give all players presence points
                        var listOfPlayersPresent = new List<string>();
                        var allMatches = _matchService.GetAllItems();
                        var allTeams = _teamService.GetAllItems();

                        allMatches.Where(m => m.MatchdayKey == _matchday.Key).ToList().ForEach(m =>
                        {
                            var team1 = allTeams.Where(t => t.Key == m.Team1Key).Single();
                            listOfPlayersPresent.Add(team1.Player1Key);
                            listOfPlayersPresent.Add(team1.Player2Key);
                            var team2 = allTeams.Where(t => t.Key == m.Team1Key).Single();
                            listOfPlayersPresent.Add(team2.Player1Key);
                            listOfPlayersPresent.Add(team2.Player2Key);
                        });
                        _matchday.PresencePlayerKeys = listOfPlayersPresent.Distinct().ToList();

                        /*answer = await DisplayAlert("Afsluiten", "Extra spelers met aanwezigheidspunten?", "Ja", "Neen");
                        if (answer)
                        {
                            //select players that may have presence points
                        }*/

                        _matchday.Closed = true;
                        var p = (_matchdayService.SaveMatchday(_matchday)).Result;

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            this.IsBusy = false;

                            //redirect to previous page
                            DoAfterDbWork();
                        });
                    });
                }
            }
        }

        public async void OnUpdateButtonClicked(object sender, EventArgs e)
        {
            try
            {

                var date = dateEntry.Date;
                _matchday.Date = date;

                if (!IsBusy)
                {
                    this.IsBusy = true;
                    Task.Run(() =>
                    {
                        var p = (_matchdayService.SaveMatchday(_matchday)).Result;

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            this.IsBusy = false;

                        //redirect to previous page
                        DoAfterDbWork();
                        });
                    });
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                //TODO check if already matches played for this date
                var answer = await DisplayAlert("Verwijderen", "Bent u zeker?", "Ja", "Neen");

                if (answer)
                {
                    if (!IsBusy)
                    {
                        this.IsBusy = true;
                        Task.Run(() =>
                        {
                            var p = (_matchdayService.DeleteItemByKey(_matchday.Key)).Result;

                            Device.BeginInvokeOnMainThread(() =>
                            {
                                this.IsBusy = false;

                                //redirect to previous page
                                DoAfterDbWork();
                            });
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async Task DoAfterDbWork()
        {
            //if (matchdayAlreadyExists)
            //{
            //    await DisplayAlert("Toevoegen", "Speeldag bestaat al", "Ok");
            //    return;
            //}
            //redirect to previous page
            await Navigation.PopAsync(true);
        }
    }
}