using System;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace EvolveApp
{
	public class ScanDevicePage : ContentPage
	{
		public ScanDevicePage()
		{
			var scanPage = new ZXingScannerPage();

			scanPage.OnScanResult += (result) =>
			{
				// Stop scanning
				scanPage.IsScanning = false;

				// Pop the page and show the result
				Device.BeginInvokeOnMainThread(() =>
				{
					Navigation.PopAsync();
					DisplayAlert("Scanned Barcode", result.Text, "OK");
				});
			};

			var scanBarcodeButton = new Button { Text = "Scan new device" };

			scanBarcodeButton.Clicked += (object sender, EventArgs e) =>
			{
				await Navigation.PushModalAsync(scanPage);
			};

			var takeOver = new Label();

			Content = new StackLayout
			{
				HorizontalOptions = LayoutOptions.Center,
				Children = { button }
			};

		}
	}
}