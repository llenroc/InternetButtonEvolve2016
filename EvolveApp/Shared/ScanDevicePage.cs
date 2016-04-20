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
				StyleId = "scanDeviceButton",
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
				xConstraint: Constraint.RelativeToParent(p => p.Width / 4),
				yConstraint: Constraint.RelativeToParent(p => p.Width / 4),
				widthConstraint: Constraint.RelativeToParent(p => p.Width / 2),
				heightConstraint: Constraint.RelativeToParent(p => p.Width / 2)
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

				var scanPage = new ZXingScannerPage();

				scanPage.OnScanResult += async (result) =>
				{
					scanPage.IsScanning = false;

					Device.BeginInvokeOnMainThread(async () =>
					{
						await Navigation.PopModalAsync();

						var isValidDevice = InternetButtonHelper.CheckDeviceId(result.Text);
						if (isValidDevice)
						{
							await viewModel.GetDevice(result.Text);
							await Navigation.PushAsync(new DeviceLandingPage(viewModel.Device));
						}
						else
							DisplayAlert("Error", "The barcode scanner had an error. Please try scanning the barcode again", "Ok");

						viewModel.ClearLock();
					});
				};

				await Navigation.PushModalAsync(scanPage);
			};

			indicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
			scanBarcodeButton.SetBinding(Button.IsEnabledProperty, "ButtonLock");

#if DEBUG
			App.XTCBackDoor = this;
#endif
		}

#if DEBUG
		public async Task BackDoor()
		{
			var page = sender as ScanDevicePage;
			var device = await ParticleCloud.SharedInstance.GetDeviceAsync(InternetButtonHelper.Olive);
			await page.Navigation.PushAsync(new DeviceLandingPage(device));
		}
#endif

		protected override bool OnBackButtonPressed()
		{
			return false;
		}
	}
}