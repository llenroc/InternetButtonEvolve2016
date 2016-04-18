using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

using TextStyles.iOS;

namespace EvolveApp.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
			ZXing.Net.Mobile.Forms.iOS.Platform.Init();
			EnumerateFonts();

			// Initalise TextStyle
			TextStyle.Instance.SetCSS(App.CSS);

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}

		void EnumerateFonts()
		{
			var familyNames = UIFont.FamilyNames;
			foreach (var familyName in familyNames)
			{
				Console.WriteLine("Family: {0}\n", familyName);

				var fontNames = UIFont.FontNamesForFamilyName(familyName);
				foreach (var fontName in fontNames)
				{
					Console.WriteLine("\tFont: {0}\n", fontName);
				}
			};
		}
	}
}