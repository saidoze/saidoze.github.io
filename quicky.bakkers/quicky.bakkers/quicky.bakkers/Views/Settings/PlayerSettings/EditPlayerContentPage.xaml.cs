using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using quicky.bakkers.models;
using quicky.bakkers.services.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace quicky.bakkers.Views.Settings.PlayerSettings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPlayerContentPage : ContentPage
    {
        private PlayerService _playerService;
        private Player _player;

        public EditPlayerContentPage(Player player)
        {
            InitializeComponent();
            BindingContext = this;
            _playerService = new PlayerService();
            _player = player;
            LoadData();
        }

        private void LoadData()
        {
            nameEntry.Text = _player.Name;
        }

        public async void OnUpdateButtonClicked(object sender, EventArgs e)
        {
            var name = nameEntry.Text;
            _player.Name = name;

            try
            {
                if (!IsBusy)
                {
                    this.IsBusy = true;
                    Task.Run(() =>
                    {
                        var p = (_playerService.SavePlayer(_player)).Result;

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
                            var p = (_playerService.DeleteItemByKey(_player.Key)).Result;

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
            //redirect to previous page
            await Navigation.PopAsync(true);
        }
    }
}