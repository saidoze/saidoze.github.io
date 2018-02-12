using quicky.bakkers.services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace quicky.bakkers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabbedPage : TabbedPage
    {
        private SettingService _settingService;
        private bool _newVersion;

        public MainTabbedPage ()
        {
            InitializeComponent();

            Children.Add(new LeaderBoardContentPage());
            Children.Add(new ResultsContentPage());
            Children.Add(new UserContentPage());

            _settingService = new SettingService();
            //check version
            CheckVersion();
        }

        private void CheckVersion()
        {
            try
            {
                Task.Run(() =>
                {
                    var localversion = Assembly.GetExecutingAssembly().GetName().Version.ToString().Replace(".", "");
                    var settings = _settingService.GetAllItems();

                    if (settings.Count == 0)
                    {
                        var s = (_settingService.SaveSettings(new models.Setting() { AssemblyVersion = localversion })).Result;
                    }
                    else
                    {
                        var newestVersion = settings.First().AssemblyVersion.Replace(".", "");
                        if (Convert.ToInt32(localversion) < Convert.ToInt32(newestVersion))
                            _newVersion = true;
                    }

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DoAfterCheckVersion(_newVersion);
                    });
                });
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async Task DoAfterCheckVersion(bool newVersion)
        {
            if (newVersion)
            {
                var answer = await DisplayAlert("Versie", "Nieuwe versie beschikbaar in de store!", "Openen", "Ok");
                if(answer)
                    Xamarin.Forms.Device.OpenUri(new Uri("market://details?id=com.nuyttens.quicky.bakkers"));
                
                return;
            }
        }
    }
}