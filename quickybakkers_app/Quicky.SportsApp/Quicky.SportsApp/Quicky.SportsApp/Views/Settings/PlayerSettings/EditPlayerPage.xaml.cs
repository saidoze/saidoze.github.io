using Quicky.SportsApp.Models;
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
	public partial class EditPlayerPage : ContentPage
    {
        private PlayerService _playerService;
        private Player _player;

        public EditPlayerPage (Player player)
		{
			InitializeComponent ();
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
            var p = await _playerService.SavePlayer(_player);

            //redirect to previous page
            await Navigation.PopAsync(true);
        }

        public async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                //TODO check if matches are played with this player

                var answer = await DisplayAlert("Verwijderen", "Bent u zeker?", "Ja", "Neen");

                if (answer)
                {
                    var p = await _playerService.DeleteItemByKey(_player.Key);

                    //redirect to previous page
                    await Navigation.PopAsync(true);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}