﻿using System;

using Xamarin.Forms;
using Particle;
using EvolveApp.ViewModels;
using EvolveApp.Views.Controls;
using EvolveApp.Pages;

namespace EvolveApp.Views.Pages
{
	public class DeviceLandingPage : ContentPage
	{
		DeviceLandingPageViewModel ViewModel;
		RelativeLayout layout;
		ActivityIndicator indicator;
		Image deviceConnected;
		DashboardWidget variableWidget, functionWidget;

		public DeviceLandingPage(ParticleDevice device)
		{
			Title = "Mission Control";
			ViewModel = new DeviceLandingPageViewModel(device);
			BindingContext = ViewModel;

			layout = new RelativeLayout();
			indicator = new ActivityIndicator();

			var deviceName = new StyledLabel { CssStyle = "h1" };
			deviceConnected = new Image { Source = "notconnected.png" };
			var currentAppLabel = new StyledLabel { CssStyle = "h2" };
			variableWidget = new DashboardWidget();
			functionWidget = new DashboardWidget();
			var refreshDevice = new ToolbarItem { Icon = "ic_cached_white_24dp.png" };
			var interactButton = new StyledButton
			{
				Text = "START INTERACTION",
				BackgroundColor = AppColors.Green,
				CssStyle = "button",
				BorderRadius = 0,
				IsEnabled = false
			};
			var flashButton = new StyledButton
			{
				Text = "FLASH NEW APP",
				BackgroundColor = AppColors.Purple,
				CssStyle = "button",
				BorderRadius = 0,
				IsEnabled = false
			};

			layout.Children.Add(deviceName,
					xConstraint: Constraint.Constant(10),
					yConstraint: Constraint.Constant(10)
				);
			layout.Children.Add(deviceConnected,
				xConstraint: Constraint.RelativeToView(deviceName, (p, v) => v.X + v.Width + 5),
				yConstraint: Constraint.Constant(20),
				widthConstraint: Constraint.RelativeToView(deviceName, (p, v) => v.Height),
				heightConstraint: Constraint.RelativeToView(deviceName, (p, v) => v.Height)
			);
			layout.Children.Add(currentAppLabel,
				xConstraint: Constraint.Constant(20),
				yConstraint: Constraint.RelativeToView(deviceName, (p, v) => v.Y + v.Height + 5)
			);
			layout.Children.Add(variableWidget,
				xConstraint: Constraint.Constant(0),
				yConstraint: Constraint.RelativeToView(currentAppLabel, (p, v) => v.Y + v.Height + 5),
				widthConstraint: Constraint.RelativeToParent(p => p.Width / 2),
				heightConstraint: Constraint.RelativeToParent(p => p.Width / 2)
			);
			layout.Children.Add(functionWidget,
				xConstraint: Constraint.RelativeToParent(p => p.Width / 2),
				yConstraint: Constraint.RelativeToView(currentAppLabel, (p, v) => v.Y + v.Height + 5),
				widthConstraint: Constraint.RelativeToParent(p => p.Width / 2),
				heightConstraint: Constraint.RelativeToParent(p => p.Width / 2)
			);
			layout.Children.Add(interactButton,
				xConstraint: Constraint.Constant(0),
				yConstraint: Constraint.RelativeToView(functionWidget, (p, v) => v.Y + v.Height),
				widthConstraint: Constraint.RelativeToParent(p => p.Width),
				heightConstraint: Constraint.RelativeToView(functionWidget, (p, v) => (p.Height - v.Y - v.Height) / 2)
			);
			layout.Children.Add(flashButton,
				xConstraint: Constraint.Constant(0),
				yConstraint: Constraint.RelativeToView(interactButton, (p, v) => v.Y + v.Height),
				widthConstraint: Constraint.RelativeToParent(p => p.Width),
				heightConstraint: Constraint.RelativeToView(interactButton, (p, v) => (p.Height - v.Y - v.Height))
			);
			layout.Children.Add(indicator,
				xConstraint: Constraint.RelativeToParent(p => p.Width / 4),
				yConstraint: Constraint.RelativeToParent(p => p.Width / 4),
				widthConstraint: Constraint.RelativeToParent(p => p.Width / 2),
				heightConstraint: Constraint.RelativeToParent(p => p.Width / 2)
			);

			variableWidget.WidgetTitle.Text = "Variables";
			functionWidget.WidgetTitle.Text = "Functions";

			if (Device.OS == TargetPlatform.iOS)
			{
				interactButton.FontFamily = "SegoeUI-Light";
				interactButton.FontSize = 16;
				interactButton.TextColor = Color.FromHex("#ffffff");

				flashButton.FontFamily = "SegoeUI-Light";
				flashButton.FontSize = 16;
				flashButton.TextColor = Color.FromHex("#ffffff");
			}

			Content = layout;
			ToolbarItems.Add(refreshDevice);

			indicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
			indicator.SetBinding(ActivityIndicator.IsVisibleProperty, "IsBusy");
			deviceName.SetBinding(Label.TextProperty, "Device.Name");
			currentAppLabel.SetBinding(Label.TextProperty, "CurrentApp");
			deviceConnected.SetBinding(Image.IsVisibleProperty, "DeviceConnected");
			variableWidget.WidgetCount.SetBinding(Label.TextProperty, "VariableCount");
			functionWidget.WidgetCount.SetBinding(Label.TextProperty, "FunctionCount");
			interactButton.SetBinding(Button.IsEnabledProperty, "InteractButtonLock");
			flashButton.SetBinding(Button.IsEnabledProperty, "FlashButtonLock");
			refreshDevice.SetBinding(ToolbarItem.CommandProperty, "RefreshDeviceCommand");

			interactButton.Clicked += async (object sender, EventArgs e) =>
			{
				if (ViewModel.CurrentApp.ToLower().Contains("rgb led picker"))
					await Navigation.PushAsync(new ChangeLEDColorPage(ViewModel.Device, ViewModel.variables));
				else if (ViewModel.CurrentApp.ToLower().Contains("simonsays"))
					await Navigation.PushAsync(new SimonSaysPage(ViewModel.Device));
				else
					DisplayAlert("Sorry...", "There isn't a mobile interaction with this IoT app. Try flashing either the 'Simon Says' or ' RBG LED' app.", "Ok");
			};

			flashButton.Clicked += async (object sender, EventArgs e) =>
						{
							var result = await DisplayActionSheet("Pick File to Flash", "Cancel", null, "RGB LED", "Shake LED", "Simon Says", "Follow me LED");
							if (result != "Cancel")
							{
								var success = await ViewModel.TryFlashFileAsync(result);
								if (!success)
								{
									await DisplayAlert("Error", "The Device connection timed out after 30 seconds. Please re-scan the barcode once the device breaths a solid cyan light", "Ok");
									await Navigation.PopAsync();
								}
							}
						};
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();

			await ViewModel.RefreshDeviceAsync();
		}
	}
}