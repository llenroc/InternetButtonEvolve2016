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
			Title = "RBG LED";
			BackgroundColor = AppColors.BackgroundColor;
			ViewModel = new ChangeLEDColorViewModel(device, variables);
			BindingContext = ViewModel;

			indicator = new ActivityIndicator { HeightRequest = Device.OnPlatform(50, 30, 50) };

			var colorPreview = new BoxView
			{
				HeightRequest = 100
			};

			redSlider = new Slider { StyleId = "redSlider", Minimum = 0, Maximum = 255, Value = 0 };
			greenSlider = new Slider { StyleId = "greenSlider", Minimum = 0, Maximum = 255, Value = 0 };
			blueSlider = new Slider { StyleId = "blueSlider", Minimum = 0, Maximum = 255, Value = 0 };
			push = new StyledButton
			{
				StyleId = "pushRGBvalueButton",
				Text = "PUSH TO PHOTON",
				BackgroundColor = AppColors.Blue,
				CssStyle = "button",
				BorderRadius = 0,
				HeightRequest = AppSettings.ButtonHeight,
				VerticalOptions = LayoutOptions.Fill
			};
			lightShow = new StyledButton
			{
				StyleId = "startLightShowButton",
				Text = "START A LIGHT SHOW",
				BackgroundColor = AppColors.Green,
				CssStyle = "button",
				BorderRadius = 0,
				HeightRequest = AppSettings.ButtonHeight,
				VerticalOptions = LayoutOptions.End
			};

			off = new ToolbarItem { Text = "LEDs Off" };

			StackLayout layout = new StackLayout
			{
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Padding = new Thickness(AppSettings.Margin, 10, AppSettings.Margin, AppSettings.Margin),
				Spacing = 10,
				Children = {
					new StyledLabel { CssStyle = "body", Text = "Color Preview:", HorizontalOptions = LayoutOptions.Start },
					colorPreview,
					new StyledLabel { CssStyle = "body", Text = "R Value", HorizontalOptions = LayoutOptions.Start },
					redSlider,
					new StyledLabel { CssStyle = "body", Text = "G Value", HorizontalOptions = LayoutOptions.Start },
					greenSlider,
					new StyledLabel { CssStyle = "body", Text = "B Value", HorizontalOptions = LayoutOptions.Start },
					blueSlider,
					indicator,
					push,
					lightShow
				}
			};

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

			redSlider.SetBinding(Slider.ValueProperty, "R", BindingMode.TwoWay);
			greenSlider.SetBinding(Slider.ValueProperty, "G", BindingMode.TwoWay);
			blueSlider.SetBinding(Slider.ValueProperty, "B", BindingMode.TwoWay);
			colorPreview.SetBinding(BoxView.BackgroundColorProperty, "ColorBoxColor");
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