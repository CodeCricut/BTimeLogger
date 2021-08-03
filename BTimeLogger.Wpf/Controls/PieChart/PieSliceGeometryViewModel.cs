using System.Windows.Media;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.Controls
{
	public class PieSliceGeometryViewModel : BaseViewModel
	{
		public Brush SliceFill { get; set; }
		public Geometry SliceGeometryData { get; set; }

		public double StartAngle { get; set; }
		public double EndAngle { get; set; }

		public double CenterX { get; set; }
		public double CenterY { get; set; }

		public double EndX1 { get; set; }
		public double EndY1 { get; set; }

		public double EndX2 { get; set; }
		public double EndY2 { get; set; }

		public Brush OutlineColor { get; set; }
		public double OutlineThickness { get; set; }

		public PieSliceGeometryViewModel()
		{

		}
	}
}
