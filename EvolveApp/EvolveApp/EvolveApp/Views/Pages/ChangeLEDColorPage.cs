using System;
using System.Collections.Generic;

using Xamarin.Forms;

using Particle;
using EvolveApp.ViewModels;

namespace EvolveApp
{
	public class ChangeLEDColorPage : ContentPage
	{
		Slider redSlider, greenSlider, blueSlider;
		StyledButton push, lightShow;
		BoxView colorBox;
		ToolbarItem off;
		ActivityIndicator indicator;
		ParticleDevice particleDevice;

		ChangeLEDColorViewModel ViewModel;

		public ChangeLEDColorPage(ParticleDevice device, Dictionary<string, string> variables)
		{
			indicator = new ActivityIndicator { HeightRequest = 50 };

			ViewModel = new ChangeLEDColorViewModel(device, variables);
			BindingContext = ViewModel;

			redSlider = new Slider
			{
				Minimum = 0,
				Maximum = 255,
				Value = 0,
			};
			greenSlider = new Slider
			{
				Minimum = 0,
				Maximum = 255,
				Value = 0,
			};
			blueSlider = new Slider
			{
				Minimum = 0,
				Maximum = 255,
				Value = 0,
			};
			push = new StyledButton
			{
				Text = "PUSH TO PHOTON",
				BackgroundColor = AppColors.Blue,
				CssStyle = "button",
				BorderRadius = 0,
				HeightRequest = 70
			};
			lightShow = new StyledButton
			{
				Text = "START A LIGHT SHOW",
				BackgroundColor = AppColors.Green,
				CssStyle = "button",
				BorderRadius = 0,
				HeightRequest = 70
			};

			off = new ToolbarItem { Text = "LEDs Off" };

			StackLayout layout = new StackLayout
			{
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Padding = new Thickness(20, 0, 20, 0)
			};
			layout.Children.Add(new StyledLabel { CssStyle = "body", Text = "R Value", HorizontalOptions = LayoutOptions.Start });
			layout.Children.Add(redSlider);
			layout.Children.Add(new StyledLabel { CssStyle = "body", Text = "G Value", HorizontalOptions = LayoutOptions.Start });
			layout.Children.Add(greenSlider);
			layout.Children.Add(new StyledLabel { CssStyle = "body", Text = "B Value", HorizontalOptions = LayoutOptions.Start });
			layout.Children.Add(blueSlider);
			layout.Children.Add(indicator);
			layout.Children.Add(push);
			layout.Children.Add(new BoxView { HeightRequest = 10 });
			layout.Children.Add(lightShow);

			if (Device.OS == TargetPlatform.iOS)
			{
				push.FontFamily = "SegoeUI-Light";
				push.FontSize = 16;
				push.TextColor = Color.FromHex("#ffffff");

				lightShow.FontFamily = "SegoeUI-Light";
				lightShow.FontSize = 16;
				lightShow.TextColor = Color.FromHex("#ffffff");
			}

			Content = layout;
			Padding = new Thickness(10, 0, 10, 0);

			redSlider.SetBinding(Slider.ValueProperty, "R", BindingMode.TwoWay);
			greenSlider.SetBinding(Slider.ValueProperty, "G", BindingMode.TwoWay);
			blueSlider.SetBinding(Slider.ValueProperty, "B", BindingMode.TwoWay);
			this.SetBinding(ContentPage.BackgroundColorProperty, "ColorBoxColor");
			indicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
			push.SetBinding(Button.CommandProperty, "PushColorCommand");
			lightShow.SetBinding(Button.CommandProperty, "LightShowCommand");
			off.SetBinding(ToolbarItem.CommandProperty, "LedsOffCommand");

			ToolbarItems.Add(off);

			particleDevice = device;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			ViewModel.SetNewColor();
		}
	}
}