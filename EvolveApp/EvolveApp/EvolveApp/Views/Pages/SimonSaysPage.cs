using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Particle;
using EvolveApp.ViewModels;
namespace EvolveApp.Pages
{
	public class SimonSaysPage : ContentPage
	{
		Button red, blue, green, yellow;
		ContentView l1, l2, l3, l4, l5, l6, l7, l8, l9, l10;
		SimonSaysViewModel ViewModel;

		public SimonSaysPage(ParticleDevice device)
		{
			ViewModel = new SimonSaysViewModel(device);
			BindingContext = ViewModel;
			BackgroundColor = AppColors.BackgroundColor;
			Title = "Simon Says";

			red = new Button { Opacity = 0.5, BackgroundColor = Color.Red, BorderRadius = 0 };
			blue = new Button { Opacity = 0.5, BackgroundColor = Color.Blue, BorderRadius = 0 };
			green = new Button { Opacity = 0.5, BackgroundColor = Color.Green, BorderRadius = 0 };
			yellow = new Button { Opacity = 0.5, BackgroundColor = Color.Yellow, BorderRadius = 0 };

			l1 = new ContentView { HorizontalOptions = LayoutOptions.FillAndExpand };
			l2 = new ContentView { HorizontalOptions = LayoutOptions.FillAndExpand };
			l3 = new ContentView { HorizontalOptions = LayoutOptions.FillAndExpand };
			l4 = new ContentView { HorizontalOptions = LayoutOptions.FillAndExpand };
			l5 = new ContentView { HorizontalOptions = LayoutOptions.FillAndExpand };
			l6 = new ContentView { HorizontalOptions = LayoutOptions.FillAndExpand };
			l7 = new ContentView { HorizontalOptions = LayoutOptions.FillAndExpand };
			l8 = new ContentView { HorizontalOptions = LayoutOptions.FillAndExpand };
			l9 = new ContentView { HorizontalOptions = LayoutOptions.FillAndExpand };
			l10 = new ContentView { HorizontalOptions = LayoutOptions.FillAndExpand };

			StackLayout lightStack = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				Children = { l1, l2, l3, l4, l5, l6, l7, l8, l9, l10 },
				Padding = new Thickness(5, 0, 0, 5),
				BackgroundColor = Color.Gray
			};

			var clearSubmission = new Button
			{
				Text = "X",
				FontSize = Device.OnPlatform(10, 8, 10),
				TextColor = Color.Black,
				FontAttributes = FontAttributes.Bold,
				BorderRadius = 10,
				BackgroundColor = Color.White,
				BorderColor = Color.Black,
				BorderWidth = 1
			};
			Button actionButton = new Button { BorderRadius = 0, TextColor = Color.White };

			var layout = new RelativeLayout();

			layout.Children.Add(red,
				xConstraint: Constraint.Constant(10),
				yConstraint: Constraint.Constant(10),
				widthConstraint: Constraint.RelativeToParent((p) => (p.Width / 2) - 15),
				heightConstraint: Constraint.RelativeToParent((p) => (p.Width / 2) - 15)
			);
			layout.Children.Add(green,
				xConstraint: Constraint.RelativeToParent((p) => (p.Width / 2) + 5),
				yConstraint: Constraint.Constant(10),
				widthConstraint: Constraint.RelativeToParent((p) => (p.Width / 2) - 15),
				heightConstraint: Constraint.RelativeToParent((p) => (p.Width / 2) - 15)
			);
			layout.Children.Add(blue,
				xConstraint: Constraint.Constant(10),
				yConstraint: Constraint.RelativeToParent((p) => (p.Width / 2)),
				widthConstraint: Constraint.RelativeToParent((p) => (p.Width / 2) - 15),
				heightConstraint: Constraint.RelativeToParent((p) => (p.Width / 2) - 15)
			);
			layout.Children.Add(yellow,
				xConstraint: Constraint.RelativeToParent((p) => (p.Width / 2) + 5),
				yConstraint: Constraint.RelativeToParent((p) => (p.Width / 2)),
				widthConstraint: Constraint.RelativeToParent((p) => (p.Width / 2) - 15),
				heightConstraint: Constraint.RelativeToParent((p) => (p.Width / 2) - 15)
			);
			layout.Children.Add(lightStack,
				xConstraint: Constraint.Constant(10),
				yConstraint: Constraint.RelativeToView(yellow, (p, v) => v.Height + v.Y + 20),
				widthConstraint: Constraint.RelativeToParent((p) => p.Width - 20),
				heightConstraint: Constraint.Constant(20)
			);
			layout.Children.Add(clearSubmission,
				xConstraint: Constraint.RelativeToView(lightStack, (p, v) => Device.OnPlatform(
																				v.Width - 5,
																				v.Width - 10,
																				v.Width - 5
																			)),
				yConstraint: Constraint.RelativeToView(lightStack, (p, v) => Device.OnPlatform(
																				v.Y - 10,
																				v.Y - 15,
																				v.Y - 15
																			)
				),
				widthConstraint: Constraint.Constant(Device.OnPlatform(20, 30, 30)),
				heightConstraint: Constraint.Constant(Device.OnPlatform(20, 30, 30))
			);
			layout.Children.Add(actionButton,
				xConstraint: Constraint.Constant(10),
				yConstraint: Constraint.RelativeToView(lightStack, (p, v) => v.Height + v.Y + 10),
				widthConstraint: Constraint.RelativeToParent((p) => p.Width - 20),
				heightConstraint: Constraint.RelativeToView(lightStack, (p, v) => p.Height - v.Y - v.Height - 20)
			);

			Content = layout;

			red.Clicked += async (object sender, EventArgs e) =>
			{
				await ViewModel.PlayerPressButtonAsync("r");
			};
			blue.Clicked += async (object sender, EventArgs e) =>
			{
				await ViewModel.PlayerPressButtonAsync("b");
			};
			green.Clicked += async (object sender, EventArgs e) =>
			{
				await ViewModel.PlayerPressButtonAsync("g");
			};
			yellow.Clicked += async (object sender, EventArgs e) =>
			{
				await ViewModel.PlayerPressButtonAsync("y");
			};
			clearSubmission.Clicked += (object sender, EventArgs e) =>
			{
				ViewModel.ClearPlayerEntry();
			};

			red.SetBinding(Button.OpacityProperty, "RedOpacity");
			green.SetBinding(Button.OpacityProperty, "GreenOpacity");
			blue.SetBinding(Button.OpacityProperty, "BlueOpacity");
			yellow.SetBinding(Button.OpacityProperty, "YellowOpacity");

			l1.SetBinding(ContentView.BackgroundColorProperty, "L1");
			l2.SetBinding(ContentView.BackgroundColorProperty, "L2");
			l3.SetBinding(ContentView.BackgroundColorProperty, "L3");
			l4.SetBinding(ContentView.BackgroundColorProperty, "L4");
			l5.SetBinding(ContentView.BackgroundColorProperty, "L5");
			l6.SetBinding(ContentView.BackgroundColorProperty, "L6");
			l7.SetBinding(ContentView.BackgroundColorProperty, "L7");
			l8.SetBinding(ContentView.BackgroundColorProperty, "L8");
			l9.SetBinding(ContentView.BackgroundColorProperty, "L9");
			l10.SetBinding(ContentView.BackgroundColorProperty, "L10");

			clearSubmission.SetBinding(Button.IsVisibleProperty, "ShowClearButton");
			actionButton.SetBinding(Button.BackgroundColorProperty, "ActionColor");
			actionButton.SetBinding(Button.TextProperty, "ActionText");
			actionButton.SetBinding(Button.CommandProperty, "ActionCommand");
		}
	}
}