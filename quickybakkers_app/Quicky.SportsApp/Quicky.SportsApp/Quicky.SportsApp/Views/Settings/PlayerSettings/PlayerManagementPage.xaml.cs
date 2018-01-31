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

namespace Quicky.SportsApp.Views.Settings.PlayerSettings
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlayerManagementPage : ContentPage
    {
        private PlayerService _playerService;
        private List<Player> _players;

        public PlayerManagementPage()
		{
			InitializeComponent();
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
            await Navigation.PushAsync(new AddPlayerPage());
        }
        
        public async Task OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new EditPlayerPage(e.SelectedItem as Player));
            }
        }
    }
}