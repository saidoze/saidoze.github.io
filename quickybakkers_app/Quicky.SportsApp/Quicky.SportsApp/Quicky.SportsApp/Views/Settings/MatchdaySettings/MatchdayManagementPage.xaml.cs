using Quicky.SportsApp.Models;
using Quicky.SportsApp.Services.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Quicky.SportsApp.Views.Settings.MatchdaySettings
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MatchdayManagementPage : LoginContentPage
    {
        private MatchdayService _matchdayService;
        private List<Matchday> _matchdays;

        public MatchdayManagementPage()
        {
            InitializeComponent();
            _matchdayService = new MatchdayService();
            BindingContext = this;
            IsBusy = false;
            AddButton.Text = (_isAuthenticated ? "Toevoegen" : "");
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
                        list.ItemsSource = new ObservableCollection<Matchday>(_matchdays);
                        this.IsBusy = false;
                    });
                });
            }
        }

        public Command RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    //IsRefreshing = true;
                    LoadData();
                    //IsRefreshing = false;
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

        public async Task OnAddButtonClicked()
        {
            if (!_isAuthenticated)
                return;
            await Navigation.PushAsync(new AddMatchdayPage());
        }

        public async Task OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new EditMatchdayPage(e.SelectedItem as Matchday));
            }
        }
    }
}