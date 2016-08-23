using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using EvolveApp.Pages;
using Particle;
using EvolveApp.Views.Pages;

namespace EvolveApp
{
	public class App : Application
	{
		public static string Token = "7770f37e1e767f9eb4a72ca81a933b4026957e02";
		public static string RefreshToken = "9b5bf63ceca02eb5c1513b3848595e90ad9e8fd9";
		public DateTime Expiration = DateTime.Parse("07/11/2016");
		public static string User = "michael.watson@xamarin.com";

		public static string CSS
		{
			get
			{
				// Load the CSS
				var assembly = typeof(App).GetTypeInfo().Assembly;
                var stream = Stream.Null;
                if(Device.OS == TargetPlatform.Android)
                    stream = assembly.GetManifestResourceStream("EvolveApp.Droid.Style.css");
                else if(Device.OS == TargetPlatform.iOS)
                    assembly.GetManifestResourceStream("EvolveApp.iOS.Style.css");

                string css = string.Empty;

                using (var reader = new System.IO.StreamReader(stream))
                {
                    css = reader.ReadToEnd();
                }

                return css;
			}
		}

		public App()
		{
			var page = new LoginPage();
			NavigationPage.SetHasNavigationBar(page, false);
			var navPage = new NavigationPage(page);

			MainPage = navPage;

            if (Device.OS == TargetPlatform.iOS)
            {
                navPage.BarBackgroundColor = AppColors.Blue;
                navPage.BarTextColor = Color.White;
            }

            navPage.Navigation.InsertPageBefore(new MyDevicesPage(), page);
		}

		protected override void OnStart()
		{
			// Handle when your app starts
			//ParticleCloud.AccessToken = new ParticleAccessToken(Token, RefreshToken, Expiration);
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
			//ParticleCloud.AccessToken = new ParticleAccessToken(Token, RefreshToken, Expiration);
		}
	}
}
