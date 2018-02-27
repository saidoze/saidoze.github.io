using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using quicky.bakkers.models;
using quicky.bakkers.services.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace quicky.bakkers.Views.Settings.MatchSettings
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditMatchContentPage : ContentPage
	{
        private Match _match;
        private MatchService _matchService;

        public EditMatchContentPage (Match match)
		{
			InitializeComponent ();
            _match = match;
            _matchService = new MatchService();
            LoadData();
        }

        private void LoadData()
        {
            //normally player names will be filled in
            Team1Player1Label.Text = _match.Player1Name;
            Team1Player2Label.Text = _match.Player2Name;
            Team1ScoreLabel.Text = _match.ScoreTeam1.ToString();
            Team2Player1Label.Text = _match.Player3Name;
            Team2Player2Label.Text = _match.Player4Name;
            Team2ScoreLabel.Text = _match.ScoreTeam2.ToString();
        }

        public async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                //TODO check if matchday has been closed
                var answer = await DisplayAlert("Verwijderen", "Bent u zeker?", "Ja", "Neen");

                if (answer)
                {
                    if (!IsBusy)
                    {
                        this.IsBusy = true;
                        Task.Run(() =>
                        {
                            var p = (_matchService.DeleteItemByKey(_match.Key)).Result;

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
            await Navigation.PopAsync(true);
        }
    }
}