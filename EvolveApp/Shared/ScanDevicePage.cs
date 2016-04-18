using System;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using EvolveApp.Views.Pages;
using Particle;
using System.Threading.Tasks;
using EvolveApp.ViewModels;
using EvolveApp.Pages;

namespace EvolveApp
{
	public class ScanDevicePage : ContentPage
	{
		bool resultReceivedLock;
		ScanDeviceViewModel ViewModel;

		public ScanDevicePage()
		{
			NavigationPage.SetHasNavigationBar(this, false);

			ViewModel = new ScanDeviceViewModel();
			BindingContext = ViewModel;

			var indicator = new ActivityIndicator();
			var scanBarcodeButton = new StyledButton
			{
				Text = "START SCANNING",
				CssStyle = "button",
				BackgroundColor = AppColors.Blue,
				BorderRadius = 0,
				WidthRequest = 150
			};

			Title = "EvolveApp";
			BackgroundColor = AppColors.BackgroundColor;
			Content = new StackLayout
			{
				VerticalOptions = LayoutOptions.Center,
				Padding = new Thickness(30),
				Children = {
					new StyledLabel {
						HorizontalTextAlignment = TextAlignment.Center,
						Text = "Particle Internet Button",
						CssStyle = "h1"
					},
					new StyledLabel {
						HorizontalTextAlignment = TextAlignment.Center,
						Text = "Take Control!",
						CssStyle = "h2"
					},
					new BoxView{
						HeightRequest = 20
					},
					new StyledLabel {
						HorizontalTextAlignment = TextAlignment.Center,
						Text = "Just scan the QR barcode of any device to take control.",
						CssStyle = "body"
					},
					new BoxView{
						HeightRequest = 10
					},
					scanBarcodeButton,
					indicator
				}
			};

#if __IOS__
			scanBarcodeButton.FontFamily = "SegoeUI-Light";
			scanBarcodeButton.FontSize = 16;
			scanBarcodeButton.TextColor = Color.FromHex("#ffffff");
#endif

			scanBarcodeButton.Clicked += async (object sender, EventArgs e) =>
			{
				ViewModel.SetLock();

				await ViewModel.GetDevice("380028000847343337373738");
				//USE THIS AREA TO PUSH WHATEVER PAGE YOU WANT TO EDIT
				await Navigation.PushAsync(new DeviceLandingPage(ViewModel.Device));

				ViewModel.ClearLock();
				//var scanPage = new ZXingScannerPage();

				//scanPage.OnScanResult += async (result) =>
				//  {
				//   scanPage.IsScanning = false;

				//   Device.BeginInvokeOnMainThread(async () =>
				//   {
				//	   await Navigation.PopModalAsync();
				//	   await ViewModel.GetDevice("380028000847343337373738");
				//	   await Navigation.PushAsync(new DeviceLandingPage(ViewModel.Device));


				//	   ViewModel.ClearLock();
				//   });
				//  };

				//await Navigation.PushModalAsync(scanPage);
			};

			indicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
			scanBarcodeButton.SetBinding(Button.IsEnabledProperty, "ButtonLock");
		}
	}
}