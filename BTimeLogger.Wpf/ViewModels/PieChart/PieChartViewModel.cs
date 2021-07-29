using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using WpfCore.Commands;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels.PieChart
{
	public abstract class PieChartViewModel : BaseViewModel
	{
		private string _chartTitle;

		public string ChartTitle
		{
			get => _chartTitle;
			set { Set(ref _chartTitle, value); }
		}

		public ObservableCollection<CategoryViewModel> Categories { get; set; } = new ObservableCollection<CategoryViewModel>();

		public ObservableCollection<PieSliceGeometryViewModel> SliceGeometries { get; set; } = new ObservableCollection<PieSliceGeometryViewModel>();


		private double _chartWidth = 500;
		public double ChartWidth
		{
			get => _chartWidth;
			set
			{
				if (_chartWidth != value)
				{
					Set(ref _chartWidth, value);
				}
			}
		}

		private double _chartHeight = 500;
		public double ChartHeight
		{
			get => _chartHeight;
			set
			{
				if (_chartHeight != value)
				{
					Set(ref _chartHeight, value);
				}
			}
		}

		private double _legendFontSize = 16;
		public double LegendFontSize
		{
			get => _legendFontSize;
			set { Set(ref _legendFontSize, value); }
		}

		private double _titleFontSize = 30;
		public double TitleFontSize
		{
			get => _titleFontSize;
			set { Set(ref _titleFontSize, value); }
		}



		private double _centerX;
		private double _centerY;
		private double _radius;
		private double _angle;
		private double _prevAngle;

		public AsyncDelegateCommand UpdateChartCommand { get; }

		public PieChartViewModel()
		{
			UpdateChartCommand = new AsyncDelegateCommand(_ => UpdateChart());
		}

		public async Task UpdateChart()
		{
			await UpdateTitle();
			await UpdateSlices();
		}

		private async Task UpdateTitle()
		{
			ChartTitle = await GetTitle();
		}

		protected abstract Task<string> GetTitle();


		private async Task UpdateSlices()
		{
			IEnumerable<CategoryViewModel> newCategories = await GetCategories();
			Categories.Clear();
			foreach (var category in newCategories)
				Categories.Add(category);

			ResetSliceGeometries();

			AddSliceGeometries();
		}

		protected abstract Task<IEnumerable<CategoryViewModel>> GetCategories();

		private void ResetSliceGeometries()
		{
			SliceGeometries.Clear();
		}

		private void AddSliceGeometries()
		{
			_centerX = ChartWidth / 2;
			_centerY = ChartHeight / 2;
			_radius = ChartHeight / 2;

			_angle = 0;
			_prevAngle = 0;
			foreach (CategoryViewModel category in Categories)
				AddSliceGeometry(category);
		}

		private void AddSliceGeometry(CategoryViewModel category)
		{
			double line1X = _radius * Math.Cos(_angle * Math.PI / 180) + _centerX;
			double line1Y = _radius * Math.Sin(_angle * Math.PI / 180) + _centerY;

			_angle = category.Percentage * 360 / 100 + _prevAngle;

			double arcX = _radius * Math.Cos(_angle * Math.PI / 180) + _centerX;
			double arcY = _radius * Math.Sin(_angle * Math.PI / 180) + _centerY;

			var line1Segment = new LineSegment(new Point(line1X, line1Y), false);
			double arcWidth = _radius, arcHeight = _radius;
			bool isLargeArc = category.Percentage > 50;
			var arcSegment = new ArcSegment()
			{
				Size = new Size(arcWidth, arcHeight),
				Point = new Point(arcX, arcY),
				SweepDirection = SweepDirection.Clockwise,
				IsLargeArc = isLargeArc,
			};
			var line2Segment = new LineSegment(new Point(_centerX, _centerY), false);

			var pathFigure = new PathFigure(
				new Point(_centerX, _centerY),
				new List<PathSegment>()
				{
						line1Segment,
						arcSegment,
						line2Segment,
				},
				true);

			var pathFigures = new List<PathFigure>() { pathFigure, };
			var pathGeometry = new PathGeometry(pathFigures);

			PieSliceGeometryViewModel sliceGeometry = new PieSliceGeometryViewModel()
			{
				CenterX = _centerX,
				CenterY = _centerY,
				EndX1 = line1Segment.Point.X,
				EndY1 = line1Segment.Point.Y,
				EndX2 = arcSegment.Point.X,
				EndY2 = arcSegment.Point.Y,
				StartAngle = _prevAngle,
				EndAngle = _angle,
				SliceFill = category.Color,
				SliceGeometryData = pathGeometry,
				OutlineColor = (SolidColorBrush)new BrushConverter().ConvertFrom("White"),
				OutlineThickness = 5,
			};

			SliceGeometries.Add(sliceGeometry);

			_prevAngle = _angle;
		}
	}
}
