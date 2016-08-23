using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using EvolveApp;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Styles.iOS.Text;
using Styles.XForms.Core;

[assembly: ExportRenderer(typeof(StyledLabel), typeof(EvolveApp.iOS.StyledLabelRenderer))]
namespace EvolveApp.iOS
{
	public class StyledLabelRenderer : LabelRenderer
	{
		StyledLabel _styledElement;

		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);

			_styledElement = _styledElement ?? (Element as StyledLabel);

			if (Control != null)
			{
				TextStyle.Main.Style<UILabel>(Control, _styledElement.CssStyle);
			}
		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == "Text")
			{
				TextStyle.Main.Style<UILabel>(Control, _styledElement.CssStyle);
			}
		}
	}
}