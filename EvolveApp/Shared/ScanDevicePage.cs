using System;
using System.Threading.Tasks;

using Particle;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

using EvolveApp.Pages;
using EvolveApp.Helpers;
using EvolveApp.ViewModels;
using EvolveApp.Views.Pages;

namespace EvolveApp
{
	public class ScanDevicePage : ContentPage
	{
		public ScanDevicePage()
		{
			NavigationPage.SetHasNavigationBar(this, false);

			Title = "EvolveApp";
			BackgroundColor = AppColors.BackgroundColor;
			var viewModel = new ScanDeviceViewModel();
			BindingContext = viewModel;

			var layout = new RelativeLayout();

			var titleLabel = new StyledLabel
			{
				Text = "Particle Internet Button",
				CssStyle = "h1",
			};
			var subtitleLabel = new StyledLabel { Text = "Take Control!", CssStyle = "h2" };
			var descriptionLabel = new StyledLabel { Text = "Just scan the QR barcode of any device to take control.", CssStyle = "body" };
			var indicator = new ActivityIndicator();
			var scanBarcodeButton = new StyledButton
			{
				Text = "START SCANNING",
				CssStyle = "button",
				BackgroundColor = AppColors.Blue,
				BorderRadius = 0,
				HeightRequest = AppSettings.ButtonHeight
			};

			layout.Children.Add(titleLabel,
				xConstraint: Constraint.Constant(AppSettings.Margin),
				yConstraint: Constraint.Constant(AppSettings.Margin * 3),
				widthConstraint: Constraint.RelativeToParent(p => p.Width - AppSettings.Margin * 2),
				heightConstraint: Constraint.Constant(100)
			);
			layout.Children.Add(subtitleLabel,
				xConstraint: Constraint.Constant(AppSettings.Margin),
				yConstraint: Constraint.RelativeToView(titleLabel, (p, v) => v.Height + v.Y + AppSettings.ItemPadding),
				widthConstraint: Constraint.RelativeToParent(p => p.Width - AppSettings.Margin * 2)
			);
			layout.Children.Add(descriptionLabel,
				xConstraint: Constraint.Constant(AppSettings.Margin),
				yConstraint: Constraint.RelativeToView(subtitleLabel, (p, v) => v.Height + v.Y + AppSettings.Margin),
				widthConstraint: Constraint.RelativeToParent(p => p.Width - AppSettings.Margin * 2)
			);
			layout.Children.Add(indicator,
				xConstraint: Constraint.Constant(AppSettings.Margin),
				yConstraint: Constraint.RelativeToView(descriptionLabel, (p, v) => v.Y + v.Height),
				widthConstraint: Constraint.RelativeToParent(p => p.Width - AppSettings.Margin * 2),
				heightConstraint: Constraint.RelativeToView(descriptionLabel, (p, v) => p.Height - v.Y - v.Height - AppSettings.Margin - AppSettings.ButtonHeight)
			);
			layout.Children.Add(scanBarcodeButton,
				xConstraint: Constraint.Constant(AppSettings.Margin),
				yConstraint: Constraint.RelativeToParent(p => p.Height - AppSettings.Margin - AppSettings.ButtonHeight),
				widthConstraint: Constraint.RelativeToParent(p => p.Width - AppSettings.Margin * 2),
				heightConstraint: Constraint.Constant(50)
			);

			Content = layout;

#if __IOS__
			scanBarcodeButton.TextColor = Color.FromHex("#ffffff");
#endif

			scanBarcodeButton.Clicked += async (object sender, EventArgs e) =>
			{
				viewModel.SetLock();

				//await viewModel.GetDevice(InternetButtonHelper.Kirby);
				//await Navigation.PushAsync(new DeviceLandingPage(viewModel.Device));

				var scanPage = new ZXingScannerPage();

				scanPage.OnScanResult += (result) =>
				{
					scanPage.IsScanning = false;

					Device.BeginInvokeOnMainThread(async () =>
					{
						await Navigation.PopModalAsync();
						System.Diagnostics.Debug.WriteLine($"Result: {result.Text}");
						var isValidDevice = InternetButtonHelper.CheckDeviceId(result.Text);
						if (isValidDevice)
						{
							await viewModel.GetDevice(result.Text);
							//await viewModel.GetDevice(InternetButtonHelper.Kirby);
							await Navigation.PushAsync(new DeviceLandingPage(viewModel.Device));
						}
						else
							DisplayAlert("Error", "The barcode scanner had an error. Please try scanning the barcode again", "Ok");

						viewModel.ClearLock();
					});
				};

				//await Navigation.PushModalAsync(scanPage);
				await viewModel.GetDevice(InternetButtonHelper.Whiskey);
				await Navigation.PushAsync(new DeviceLandingPage(viewModel.Device));
			};

			indicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
			if (Device.OS != TargetPlatform.iOS && Device.OS != TargetPlatform.Android)
				indicator.SetBinding(ActivityIndicator.IsVisibleProperty, "IsBusy");

#if __IOS__
			scanBarcodeButton.SetBinding(Button.IsEnabledProperty, "ButtonLock");
#endif
#if __ANDROID__
			scanBarcodeButton.SetBinding(Button.IsEnabledProperty, "ButtonLock");
#endif
		}
	}
}