using System;
using EvolveApp;
using Foundation;
using TextStyles.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer (typeof (StyledButton), typeof (EvolveApp.iOS.StyledButtonRenderer))]
namespace EvolveApp.iOS
{
	public class StyledButtonRenderer : ButtonRenderer
	{
		StyledButton _styledElement;

		public StyledButtonRenderer ()
		{
		}

		protected override void OnElementChanged (ElementChangedEventArgs<Xamarin.Forms.Button> e)
		{
			base.OnElementChanged (e);

			_styledElement = Element as StyledButton;
			var cssStyle = _styledElement.CssStyle;

			if (Control != null) {
				TextStyle.Style<UILabel> (Control.TitleLabel, cssStyle);
			}
		}
	}
}