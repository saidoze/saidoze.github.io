using quicky.bakkers.services.Services;
using quicky.bakkers.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace quicky.bakkers.Views.Settings.MatchdaySettings
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MatchdayOverviewContentPage : ContentPage
    {
        private MatchdayService _matchdayService;
        private List<Matchday> _matchdays;

        public MatchdayOverviewContentPage ()
		{
			InitializeComponent ();
            BindingContext = this;
            this.IsBusy = false;

            _matchdayService = new MatchdayService();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!IsBusy)
            {
                this.IsBusy = true;
                Task.Run(() =>
                {
                    LoadData();
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        datalist.ItemsSource = new ObservableCollection<Matchday>(_matchdays.OrderBy(m => m.Number));
                        this.IsBusy = false;
                    });
                });
            }
        }

        private void LoadData()
        {
            _matchdays = _matchdayService.GetAllItems();

            //fix numbering
            var number = 1;
            _matchdays.OrderBy(m => m.Date).ToList().ForEach(m => {
                m.Number = number;
                number++;
            });
        }


        public async Task OnAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new AddMatchdayContentPage(_matchdays.Count));
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }
        public async Task OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (e.SelectedItem != null)
                {
                    await Navigation.PushAsync(new EditMatchdayContentPage(e.SelectedItem as Matchday));
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }

    }
}