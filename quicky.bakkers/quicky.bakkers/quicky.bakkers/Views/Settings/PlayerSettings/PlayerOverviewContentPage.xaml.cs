using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using quicky.bakkers.models;
using quicky.bakkers.services.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace quicky.bakkers.Views.Settings.PlayerSettings
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlayerOverviewContentPage : ContentPage
    {
        private PlayerService _playerService;
        private List<Player> _players;

        public PlayerOverviewContentPage ()
		{
			InitializeComponent ();
            BindingContext = this;
            IsBusy = false;
            _playerService = new PlayerService();
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
                        list.ItemsSource = new ObservableCollection<Player>(_players);
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
                    LoadData();
                });
            }
        }

        private void LoadData()
        {
            _players = _playerService.GetAllItems();
        }

        public async Task OnAddButtonClicked()
        {
            try
            {
                await Navigation.PushAsync(new AddPlayerContentPage());
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
                    await Navigation.PushAsync(new EditPlayerContentPage(e.SelectedItem as Player));
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}