using Quicky.SportsApp.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Quicky.SportsApp.Views.Settings.PlayerSettings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPlayerPage : ContentPage
    {
        private PlayerService _playerService;

        public AddPlayerPage()
        {
            InitializeComponent();
            _playerService = new PlayerService();
        }

        public async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var name = nameEntry.Text;
            //check if not exists
            var all = await _playerService.GetAllItemsAsync();
            if(all.Where(a => a.Name == name).Count() > 0)
            {
                var answer = await DisplayAlert("Toevoegen", "Naam bestaat al, doorgaan?", "Ja", "Neen");
                if (!answer)
                    return;
            }

            var p = await _playerService.SavePlayer(new Models.Player() { Name = name });
            
            //redirect to previous page
            await Navigation.PopAsync(true);
        }
    }
}