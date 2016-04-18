using System;
using System.Threading.Tasks;
using Particle;
namespace EvolveApp.ViewModels
{
	public class ScanDeviceViewModel : BaseViewModel
	{
		public ParticleDevice Device { get; internal set; }

		public async Task GetDevice(string id)
		{
			IsBusy = true;

			Device = await ParticleCloud.SharedInstance.GetDeviceAsync(id);

			IsBusy = false;
		}

		public void SetLock()
		{
			IsBusy = true;
			OnPropertyChanged("ButtonLock");
		}

		public void ClearLock()
		{
			IsBusy = false;
			OnPropertyChanged("ButtonLock");
		}

		public bool ButtonLock
		{
			get { return !IsBusy; }
		}
	}
}