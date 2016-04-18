using System;
using Xamarin.Forms;

namespace EvolveApp.Views.Controls
{
	public class DashboardWidget : RelativeLayout
	{
		public StyledLabel WidgetTitle { get; internal set; }
		public Label WidgetCount { get; internal set; }

		public DashboardWidget()
		{
			WidgetTitle = new StyledLabel { CssStyle = "body" };
			WidgetCount = new Label { TextColor = Color.FromHex("#778687"), FontFamily = "SegoeUI-Light" };

			Func<RelativeLayout, double> getWidgetCountWidth = (p) => WidgetCount.GetSizeRequest(this.Width, this.Height).Request.Width;

			Children.Add(WidgetTitle,
				xConstraint: Constraint.Constant(5),
				yConstraint: Constraint.Constant(5)
			);
			Children.Add(WidgetCount,
				xConstraint: Constraint.RelativeToParent(p => (p.Width - getWidgetCountWidth(p)) / 2),
				yConstraint: Device.OnPlatform(
					Constraint.RelativeToView(WidgetTitle, (p, v) => v.Y + v.Height + 10),
					Constraint.RelativeToView(WidgetTitle, (p, v) => v.Y + v.Height),
					Constraint.RelativeToView(WidgetTitle, (p, v) => v.Y + v.Height)
				   )
			);
			BackgroundColor = AppColors.BackgroundColor;
		}

		bool isInitialized;
		protected override void LayoutChildren(double x, double y, double width, double height)
		{
			if (!isInitialized)
			{
				WidgetCount.FontSize = Math.Round(height / 2, 0);
				isInitialized = true;
			}
			base.LayoutChildren(x, y, width, height);
		}
	}
}

