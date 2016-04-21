using System;
namespace EvolveApp.Helpers
{
	public static class InternetButtonHelper
	{
		public static string Olive = "3d002d000447343233323032";
		public static string Charlie = "28002b000347343233323032";
		public static string Kirby = "3f0020000447343232363230";
		public static string Whiskey = "20003f000547343232363230";
		public static string Anarkali = "";

		public static bool CheckDeviceId(string id)
		{
            var checkForRandomChars = id.IndexOf('/');

			if (id == Olive || id == Charlie || id == Kirby || id == Whiskey || id == Anarkali)
				return true;
			return false;
		}

		public static string GetAppDescription(string app)
		{
			switch (app)
			{
				case "simonsays":
					return "This app is a throwback to the classic Simon Says. The mobile interaction provides a controller on your phone to send moves to the device or use the buttons firectly on the device for moves. ";
					break;
				case "FOLLOWMELED":
					return "Press the buttons on the device to see how the LED lights will follow the closest path to the button you pressed. This app does not have a mobile interaction. Try Simon Says or RGB LED Picker for a mobile interaction.";
				case "SHAKE LED":
					return "Shake the device to see the LED lights change color based on the accelerometer readings. This app does not have a mobile interaction. Try Simon Says or RGB LED Picker for a mobile interaction.";
				case "RGB LED PICKER":
					return "Set the R/G/B values in the mobile interaction and push them to the device. The mobile interaction provides a preview of the RGB color and the device will display the RGB intensities and final color from preview.";
			}

			return "none";
		}
	}
}