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
	public partial class AddMatchdayPage : ContentPage
	{
        private MatchdayService _matchdayService;

        public AddMatchdayPage ()
		{
			InitializeComponent();
            _matchdayService = new MatchdayService();
        }

        public async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var date = dateEntry.Date;
            
            //check if not exists
            var all = await _matchdayService.GetAllItemsAsync();
            if (all.Where(a => a.Date.Year == date.Year && a.Date.Month == date.Month && a.Date.Day == date.Day).Count() > 0)
            {
                var answer = await DisplayAlert("Toevoegen", "Speeldag bestaat al, doorgaan?", "Ja", "Neen");
                if (!answer)
                    return;
            }

            var p = await _matchdayService.SaveMatchday(new Models.Matchday() { Date = date });

            //redirect to previous page
            await Navigation.PopAsync(true);
        }
    }
}