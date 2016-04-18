using System;
using System.Threading.Tasks;

using Particle;
using Particle.Helpers;
using Xamarin.Forms;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EvolveApp.ViewModels
{
	public class SimonSaysViewModel : BaseViewModel
	{
		public ParticleDevice InternetButton { get; internal set; }
		public bool gameRunning = false;
		public string playerEntry = "";
		public string simonMoves;
		bool buttonLock;

		public SimonSaysViewModel(ParticleDevice device)
		{
			InternetButton = device;
		}

		public bool ShowClearButton
		{
			get
			{
				if (L1 == Color.Transparent) return false;
				return true;
			}
		}

		#region Lights binding properties

		Color l1color = Color.Transparent;
		Color l2color = Color.Transparent;
		Color l3color = Color.Transparent;
		Color l4color = Color.Transparent;
		Color l5color = Color.Transparent;
		Color l6color = Color.Transparent;
		Color l7color = Color.Transparent;
		Color l8color = Color.Transparent;
		Color l9color = Color.Transparent;
		Color l10color = Color.Transparent;

		public Color L1
		{
			get { return l1color; }
			set
			{
				if (l1color == value)
					return;
				l1color = value;
				OnPropertyChanged("L1");
			}
		}
		public Color L2
		{
			get { return l2color; }
			set
			{
				if (l2color == value)
					return;
				l2color = value;
				OnPropertyChanged("L2");
			}
		}
		public Color L3
		{
			get { return l3color; }
			set
			{
				if (l3color == value)
					return;
				l3color = value;
				OnPropertyChanged("L3");
			}
		}
		public Color L4
		{
			get { return l4color; }
			set
			{
				if (l4color == value)
					return;
				l4color = value;
				OnPropertyChanged("L4");
			}
		}
		public Color L5
		{
			get { return l5color; }
			set
			{
				if (l5color == value)
					return;
				l5color = value;
				OnPropertyChanged("L5");
			}
		}
		public Color L6
		{
			get { return l6color; }
			set
			{
				if (l6color == value)
					return;
				l6color = value;
				OnPropertyChanged("L6");
			}
		}
		public Color L7
		{
			get { return l7color; }
			set
			{
				if (l7color == value)
					return;
				l7color = value;
				OnPropertyChanged("L7");
			}
		}
		public Color L8
		{
			get { return l8color; }
			set
			{
				if (l8color == value)
					return;
				l8color = value;
				OnPropertyChanged("L8");
			}
		}
		public Color L9
		{
			get { return l9color; }
			set
			{
				if (l9color == value)
					return;
				l9color = value;
				OnPropertyChanged("L9");
			}
		}
		public Color L10
		{
			get { return l10color; }
			set
			{
				if (l10color == value)
					return;
				l10color = value;
				OnPropertyChanged("L10");
			}
		}

		#endregion

		#region ActionButtion Implementation 

		private Command actionCommand;
		public Command ActionCommand
		{
			get
			{
				return actionCommand ?? (actionCommand = new Command(async () => await PerformAction()));
			}
		}

		public string ActionText
		{
			get
			{
				if (gameRunning) return "Submit Move";
				return "Start Game";
			}
		}

		public Color ActionColor
		{
			get
			{
				if (gameRunning) return AppColors.Blue;
				return AppColors.Green;
			}
		}

		async Task PerformAction()
		{
			if (!gameRunning)
			{
				//Start game
				gameRunning = true;
				OnPropertyChanged("ActionText");
				OnPropertyChanged("ActionColor");
				await StartGame();
			}
			else {
				var result = await InternetButton.CallFunctionAsync("buttonPress", playerEntry);

				//if (result == "1")
				//{
				//	await ParticleCloud.SharedInstance.PublishEventWithNameAsync("SimonSays", $"{{ \"g\":\"{ gameId }\",\"a\":\"correctmove\", \"u\":\"{ App.User }\",\"v\":\"{ playerEntry }\" }}", true, 60);
				//}

				ClearPlayerEntry();
			}
		}

		#endregion

		#region Opacity Properties

		double redOpacity = 0.5;
		double greenOpacity = 0.5;
		double blueOpacity = 0.5;
		double yellowOpacity = 0.5;

		public double RedOpacity
		{
			get
			{
				return redOpacity;
			}
			set
			{
				redOpacity = value;
				OnPropertyChanged("RedOpacity");
			}
		}

		public double GreenOpacity
		{
			get
			{
				return greenOpacity;
			}
			set
			{
				greenOpacity = value;
				OnPropertyChanged("GreenOpacity");
			}
		}

		public double BlueOpacity
		{
			get
			{
				return blueOpacity;
			}
			set
			{
				blueOpacity = value;
				OnPropertyChanged("BlueOpacity");
			}
		}

		public double YellowOpacity
		{
			get
			{
				return yellowOpacity;
			}
			set
			{
				yellowOpacity = value;
				OnPropertyChanged("YellowOpacity");
			}
		}

		#endregion

		#region Light Box Implementation

		public void ClearPlayerEntry()
		{
			playerEntry = "";
			L1 = Color.Transparent;
			L2 = Color.Transparent;
			L3 = Color.Transparent;
			L4 = Color.Transparent;
			L5 = Color.Transparent;
			L6 = Color.Transparent;
			L7 = Color.Transparent;
			L8 = Color.Transparent;
			L9 = Color.Transparent;
			L10 = Color.Transparent;
			OnPropertyChanged("ShowClearButton");
		}

		void SetLightColor(Color color)
		{
			switch (playerEntry.Length)
			{
				case 1:
					L1 = color;
					OnPropertyChanged("ShowClearButton");
					break;
				case 2:
					L2 = color;
					break;
				case 3:
					L3 = color;
					break;
				case 4:
					L4 = color;
					break;
				case 5:
					L5 = color;
					break;
				case 6:
					L6 = color;
					break;
				case 7:
					L7 = color;
					break;
				case 8:
					L8 = color;
					break;
				case 9:
					L9 = color;
					break;
				case 10:
					L10 = color;
					break;
			}
		}

		#endregion

		#region IoT Interactions

		Guid gameCheckGuid;
		string gameId = "";

		public async Task StartGame()
		{
			playerEntry = "";
			gameCheckGuid = await InternetButton.SubscribeToEventsWithPrefixAsync("SimonSays", GameHandler);
			await InternetButton.CallFunctionAsync("startSimon");
			gameRunning = true;

			await Task.Delay(500);

			var simonParticle = await InternetButton.GetVariableAsync("simon");
			simonMoves = simonParticle.Result.ToString();

			Random rand = new Random();
			for (var i = 0; i < 10; i++)
			{
				gameId += rand.Next(0, 9);
			}

			//await ParticleCloud.SharedInstance.PublishEventWithNameAsync("SimonSays", $"{{ \"g\":\"{ gameId }\",\"a\":\"startsimon\", \"u\":\"{ App.User }\",\"v\":\"mobile\" }}", true, 60);
			System.Diagnostics.Debug.WriteLine(simonMoves);
		}

		public async Task Winner()
		{
			playerEntry = "Winner";
			OnPropertyChanged("DetailText");

			await EndGame();
		}

		public async Task EndGame()
		{
			gameRunning = false;

			ClearPlayerEntry();
			OnPropertyChanged("ActionText");
			OnPropertyChanged("ActionColor");
			await InternetButton.UnsubscribeToEventsWithIdAsync(gameCheckGuid);
			gameId = "";
		}

		public async Task PlayerPressButtonAsync(string color)
		{
			if (buttonLock)
				return;

			buttonLock = true;

			playerEntry += color;
			OnPropertyChanged("DetailText");

			switch (color)
			{
				case "r":
					RedOpacity = 1;
					break;
				case "g":
					GreenOpacity = 1;
					break;
				case "b":
					BlueOpacity = 1;
					break;
				case "y":
					YellowOpacity = 1;
					break;
			}

			await Task.Delay(250);

			Color colorToDisplay = Color.Transparent;

			switch (color)
			{
				case "r":
					RedOpacity = 0.5;
					colorToDisplay = Color.Red;
					break;
				case "g":
					GreenOpacity = 0.5;
					colorToDisplay = Color.Green;
					break;
				case "b":
					BlueOpacity = 0.5;
					colorToDisplay = Color.Blue;
					break;
				case "y":
					YellowOpacity = 0.5;
					colorToDisplay = Color.Yellow;
					break;
			}

			SetLightColor(colorToDisplay);

			buttonLock = false;
		}

		async void GameHandler(object sender, ParticleEventArgs e)
		{
			var data = JsonConvert.DeserializeObject<SimonSaysActivity>(e.EventData.Data);

			if (data.Activity == SimonSaysActivity.EndSimon)
			{
				if (data.Value == "winner")
				{
					await Winner();
					Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
					{
						Application.Current.MainPage.DisplayAlert("Congrats!!", "You won!!", "OK");
					});
				}
				else
				{
					await EndGame();
					Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
					{
						Application.Current.MainPage.DisplayAlert("Oh No!!", "Things must be going down hill, you got the last one wrong", "OK");
					});
				}
			}

			System.Diagnostics.Debug.WriteLine($"{e.EventData.Event}: {e.EventData.Data}\n{e.EventData.DeviceId}");
		}

		#endregion
	}
}