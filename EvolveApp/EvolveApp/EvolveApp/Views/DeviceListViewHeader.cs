using System;

using Xamarin.Forms;
using Styles.XForms.Core;

namespace EvolveApp.Views
{
	public class DeviceListViewHeader : Grid
	{
		public DeviceListViewHeader()
		{
			var deviceName = new StyledLabel
			{
				CssStyle = "h2left",
				Text = "Device Name"
			};
			var lastHeard = new StyledLabel
			{
				CssStyle = "h2left",
				Text = "Last Heard",
				VerticalOptions = LayoutOptions.Center,
			};

			ColumnDefinitions = new ColumnDefinitionCollection {
				new ColumnDefinition { Width = new GridLength (2, GridUnitType.Star) },
				new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) }
			};

			Children.Add(deviceName, 0, 0);
			Children.Add(lastHeard, 1, 0);
			Padding = new Thickness(10, 0, 5, 0);
		}
	}
}