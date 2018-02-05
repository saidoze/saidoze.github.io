using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace quicky.bakkers
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			//MainPage = new quicky.bakkers.Views.MainTabbedPage();
            MainPage = new NavigationPage(new quicky.bakkers.Views.MainTabbedPage());
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
