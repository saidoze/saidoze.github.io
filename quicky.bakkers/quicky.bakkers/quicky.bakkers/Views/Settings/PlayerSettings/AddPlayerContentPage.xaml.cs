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
    public partial class AddPlayerContentPage : ContentPage
    {
        private PlayerService _playerService;
        private bool _nameAlreadyExist = false;

        public AddPlayerContentPage()
        {
            InitializeComponent();
            BindingContext = this;
            IsBusy = false;
            _playerService = new PlayerService();
        }

        public async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var name = nameEntry.Text;
            //check if not exists
            var all = await _playerService.GetAllItemsAsync();
            if (all.Where(a => a.Name == name).Count() > 0)
            {
                var answer = await DisplayAlert("Toevoegen", "Naam bestaat al, doorgaan?", "Ja", "Neen");
                if (!answer)
                    return;
            }


            if (!IsBusy)
            {
                this.IsBusy = true;
                Task.Run(() =>
                {
                    var p = (_playerService.SavePlayer(new Player() { Name = name })).Result;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        this.IsBusy = false;

                        //redirect to previous page
                        DoAfterDbWork();
                    });
                });
            }
        }

        private async Task DoAfterDbWork()
        {
            if (_nameAlreadyExist)
            {
                await DisplayAlert("Toevoegen", "Naam bestaat al", "Ok");
                return;
            }

            //redirect to previous page
            await Navigation.PopAsync(true);
        }
    }
}