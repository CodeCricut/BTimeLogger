using System.Windows.Media;

namespace BTimeLogger.Wpf.Util
{
	public static class ColorExtensions
	{
		public static SolidColorBrush ToSolidColorBrush(this Color color)
		{
			return new SolidColorBrush(color);
		}

		public static Color ToColor(this SolidColorBrush brush)
		{
			return brush.Color;
		}
	}
}
