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

        public EditMatchContentPage (Match match)
		{
			InitializeComponent ();
            _match = match;
		}

        public async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                //TODO check if already matches played for this date
                //var answer = await DisplayAlert("Verwijderen", "Bent u zeker?", "Ja", "Neen");

                //if (answer)
                //{
                //    if (!IsBusy)
                //    {
                //        this.IsBusy = true;
                //        Task.Run(() =>
                //        {
                //            var p = (_playerService.DeleteItemByKey(_player.Key)).Result;

                //            Device.BeginInvokeOnMainThread(() =>
                //            {
                //                this.IsBusy = false;

                //                //redirect to previous page
                //                DoAfterDbWork();
                //            });
                //        });
                //    }
                //}
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}