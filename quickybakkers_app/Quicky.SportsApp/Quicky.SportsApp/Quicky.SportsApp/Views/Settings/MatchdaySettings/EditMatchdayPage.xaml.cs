using Quicky.SportsApp.Models;
using Quicky.SportsApp.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Quicky.SportsApp.Views.Settings.MatchdaySettings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditMatchdayPage : ContentPage
    {
        private MatchdayService _matchdayService;
        private MatchService _matchService;
        private TeamService _teamService;
        private Matchday _matchday;

        public EditMatchdayPage(Matchday matchday)
        {
            InitializeComponent();
            BindingContext = this;
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
            deleteButton.IsEnabled = !_matchday.Closed;
        }

        public async void OnCloseButtonClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Afsluiten", "Bent u zeker?", "Ja", "Neen");
            if (answer)
            {
                //give all players presence points
                var listOfPlayersPresent = new List<string>();
                var allMatches = _matchService.GetAllItems();
                var allTeams = _teamService.GetAllItems();

                allMatches.Where(m => m.MatchdayKey == _matchday.Key).ToList().ForEach(m => {
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
                var p = await _matchdayService.SaveMatchday(_matchday);

                //redirect to previous page
                await Navigation.PopAsync(true);
            }
        }

        public async void OnUpdateButtonClicked(object sender, EventArgs e)
        {
            var date = dateEntry.Date;
            _matchday.Date = date;
            var p = await _matchdayService.SaveMatchday(_matchday);

            //redirect to previous page
            await Navigation.PopAsync(true);
        }

        public async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                //TODO check if already matches played for this date

                var answer = await DisplayAlert("Verwijderen", "Bent u zeker?", "Ja", "Neen");

                if (answer)
                {
                    var p = await _matchdayService.DeleteItemByKey(_matchday.Key);

                    //redirect to previous page
                    await Navigation.PopAsync(true);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}