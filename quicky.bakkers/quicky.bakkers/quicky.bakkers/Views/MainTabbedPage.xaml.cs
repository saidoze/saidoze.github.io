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

            var leaderboardContentPage = new LeaderBoardContentPage();
            if(Device.RuntimePlatform == Device.iOS)
                leaderboardContentPage.Icon = "list.png";

            var resultsContentPage = new ResultsContentPage();
            if (Device.RuntimePlatform == Device.iOS)
                resultsContentPage.Icon = "line_chart.png";

            var userContentPage = new UserContentPage();
            if (Device.RuntimePlatform == Device.iOS)
                userContentPage.Icon = "settings.png";

            Children.Add(leaderboardContentPage);
            Children.Add(resultsContentPage);
            Children.Add(userContentPage);

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
                        var s = (_settingService.SaveSettings(new models.Setting() {
                            AssemblyVersionAndroid = localversion,
                            AssemblyVersionIos = localversion
                        })).Result;
                    }
                    else
                    {
                        var version_ios = settings.First().AssemblyVersionIos.Replace(".", "");
                        var version_android = settings.First().AssemblyVersionAndroid.Replace(".", "");

                        if ((Device.RuntimePlatform == Device.iOS) && (Convert.ToInt32(localversion) < Convert.ToInt32(version_ios)))
                            _newVersion = true;
                        if ((Device.RuntimePlatform == Device.Android) && (Convert.ToInt32(localversion) < Convert.ToInt32(version_android)))
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
                if (answer) {
                    if(Device.RuntimePlatform == Device.iOS)
                        Xamarin.Forms.Device.OpenUri(new Uri("itms://itunes.apple.com/us/app/quickybakkers/id1351261448?l=nl&ls=1&mt=8"));
                    //http opens in safari browser
                    else
                        Xamarin.Forms.Device.OpenUri(new Uri("market://details?id=com.nuyttens.quicky.bakkers"));
                }
                return;
            }
        }
    }
}