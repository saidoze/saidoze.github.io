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
	public partial class AddMatchdayContentPage : ContentPage
    {
        private bool _matchdayAlreadyExists;
        private MatchdayService _matchdayService;
        private int _amountOfMatchdays;

        public AddMatchdayContentPage (int amountOfMatchdays)
		{
			InitializeComponent ();
            BindingContext = this;
            IsBusy = false;
            _matchdayService = new MatchdayService();
            _amountOfMatchdays = amountOfMatchdays;
        }

        public async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var date = dateEntry.Date;

            if (!IsBusy)
            {
                this.IsBusy = true;
                Task.Run(() =>
                {
                    //check if not exists
                    var all = (_matchdayService.GetAllItemsAsync()).Result;
                    _matchdayAlreadyExists = (all.Where(a => a.Date.Year == date.Year && a.Date.Month == date.Month && a.Date.Day == date.Day).Count() > 0);

                    if (!_matchdayAlreadyExists)
                    {
                        var dummy = (_matchdayService.SaveMatchday(new Matchday() { Date = date, Number = _amountOfMatchdays + 1 })).Result;
                    }

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        this.IsBusy = false;
                        DoAfterSave(_matchdayAlreadyExists);
                    });
                });
            }
        }

        private async Task DoAfterSave(bool matchdayAlreadyExists)
        {
            if (matchdayAlreadyExists)
            {
                await DisplayAlert("Toevoegen", "Speeldag bestaat al", "Ok");
                return;
            }
            //redirect to previous page
            await Navigation.PopAsync(true);
        }
    }
}