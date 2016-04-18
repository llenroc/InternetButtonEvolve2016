using Particle;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace EvolveApp.ViewModels
{
	public class DeviceLandingPageViewModel : BaseViewModel
	{
		public ParticleDevice Device { get; internal set; }

		public DeviceLandingPageViewModel(ParticleDevice device)
		{
			Device = device;
		}

		string currentApp;
		public string CurrentApp
		{
			get
			{
				var success = variables.TryGetValue("currentApp", out currentApp);

				if (success && currentApp != "string")
					return $"CURRENT APP: {currentApp.ToUpper()}";
				return "CURRENT APP: DISCONNECTED";
			}
		}

		public string VariableCount
		{
			get { return $"{variables.Count}"; }
		}

		public string FunctionCount
		{
			get { return $"{Device.Functions.Count}"; }
		}

		public bool DeviceConnected
		{
			get
			{
				return !Device.Connected;
				if (Device.Connected)
					return false;
				return true;
			}
		}

		Command refreshDeviceCommand;
		public Command RefreshDeviceCommand
		{
			get
			{
				return refreshDeviceCommand ?? (refreshDeviceCommand = new Command(async () => await RefreshDeviceAsync()));
			}
		}


		public bool InteractButtonLock
		{
			get
			{
				if (IsBusy)
					return false;
				if (CurrentApp.ToLower().Contains("followmeled") || CurrentApp.ToLower().Contains("shake led"))
					return false;
				return true;
			}
		}

		public bool FlashButtonLock
		{
			get { return !IsBusy; }
		}

		public void SetLock(bool locked = true)
		{
			IsBusy = locked;
			OnPropertyChanged("InteractButtonLock");
			OnPropertyChanged("FlashButtonLock");
		}

		public Dictionary<string, string> variables = new Dictionary<string, string>();

		public async Task RefreshDeviceAsync()
		{
			if (IsBusy)
				return;

			SetLock();

			Device = await ParticleCloud.SharedInstance.GetDeviceAsync(Device.Id);
			variables.Clear();

			if (Device.Connected)
			{
				foreach (var variable in Device.Variables)
				{
					var variableValue = await Device.GetVariableAsync(variable.Key);

					if (!string.IsNullOrEmpty(variableValue.Result.ToString()))
					{
						variables.Add(variable.Key, variableValue.Result.ToString());

						if (variable.Key == "currentApp")
							OnPropertyChanged("CurrentApp");
					}
				}
			}

			OnPropertyChanged("VariableCount");
			OnPropertyChanged("FunctionCount");
			OnPropertyChanged("DeviceConnected");

			SetLock(false);
		}

		public async Task<bool> TryFlashFileAsync(string fileSelected)
		{
			if (IsBusy)
				return false;

			SetLock();
			bool response = false;

			var assembly = typeof(DeviceLandingPageViewModel).GetTypeInfo().Assembly;
			Stream stream = null;
			string filename = "";

			switch (fileSelected)
			{
				case "RGB LED":
					filename = "rgbled.bin";
					break;
				case "Simon Says":
					filename = "simonsays.bin";
					break;
				case "Follow me LED":
					filename = "followme.bin";
					break;
				case "Shake LED":
					filename = "shakeled.bin";
					break;
			}

			stream = assembly.GetManifestResourceStream($"EvolveApp.Binaries.{filename}");
			if (stream == null)
				return false;

			using (var reader = new System.IO.BinaryReader(stream))
			{
				response = await Device.FlashFilesAsync(reader.ReadBytes(((int)stream.Length)), filename);
			}

			response = await WaitForFlashCompleteAsync();

			if (response)
			{
				SetPrivateBusy(false);
				await RefreshDeviceAsync();
			}

			SetLock(false);

			return response;
		}

		async Task<bool> WaitForFlashCompleteAsync()
		{
			bool flashComplete = false;
			int counter = 0;

			while (!flashComplete)
			{
				await Task.Delay(3000);
				var device = await ParticleCloud.SharedInstance.GetDeviceAsync(Device.Id);

				if (device != null)
				{
					if (device.Connected)
						flashComplete = true;
					else
						return false;
				}

				counter++;

				if (counter >= 10)
					return false;
			}

			return true;
		}
	}
}