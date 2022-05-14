using BTimeLogger.Wpf.Model;
using BTimeLogger.Wpf.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace BTimeLogger.Wpf.Controls;

public abstract class PieChartViewModel : BaseViewModel
{
	private readonly IPieSliceViewModelFactory _pieSliceViewModelFactory;

	private string _chartTitle;
	public string ChartTitle
	{
		get => _chartTitle;
		set { Set(ref _chartTitle, value); }
	}

	private bool _isLoading;
	public bool IsLoading
	{
		get { return _isLoading; }
		set
		{
			Set(ref _isLoading, value);
			RaisePropertyChanged(nameof(IsChartPopulated));
			RaisePropertyChanged(nameof(IsChartNotPopulated));
		}
	}

	public bool IsChartPopulated { get => Categories.Count > 0 && !IsLoading; }

	public bool IsChartNotPopulated { get => !IsChartPopulated && !IsLoading; }

	public ObservableCollection<CategoryViewModel> Categories { get; set; } = new ObservableCollection<CategoryViewModel>();

	public ObservableCollection<PieSliceViewModel> PieSlices { get; set; } = new();

	// TODO: Don't hardcode these values; use responsive layout
	private double _chartWidth = 300;
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

	private double _chartHeight = 300;
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

	public AsyncDelegateCommand SelectCategoryCommand { get; }

	public PieChartViewModel(IPieSliceViewModelFactory pieSliceViewModelFactory)
	{
		_pieSliceViewModelFactory = pieSliceViewModelFactory;

		UpdateChartCommand = new AsyncDelegateCommand(_ => UpdateChart());
		SelectCategoryCommand = new AsyncDelegateCommand(SelectCategory);

		Categories.CollectionChanged += (_, args) =>
		{
			RaisePropertyChanged(nameof(IsChartPopulated));
			RaisePropertyChanged(nameof(IsChartNotPopulated));
		};
	}

	protected virtual Task SelectCategory(object selectedCategoryId)
	{
		return Task.CompletedTask;
	}

	public async Task UpdateChart()
	{
		if (!IsLoading)
		{
			await UpdateTitle();
			await UpdateSlices();
		}
	}

	private Task UpdateTitle()
	{
		return Task.Factory.StartNew(async () =>
		{
			IsLoading = true;

			string chartTitle = await GetTitle();

			Application.Current.Dispatcher.Invoke(() =>
			{
				ChartTitle = chartTitle;
				IsLoading = false;
			});
		});
	}

	protected abstract Task<string> GetTitle();

	private async Task UpdateSlices()
	{
		IsLoading = true;
		await Task.Factory.StartNew(async () =>
		{
			IEnumerable<CategoryViewModel> newCategories = (await GetCategories())
			.RemoveCategoriesBelowPercentThreshold()
			.ToList()
			.AddOtherCategory();

			Application.Current.Dispatcher.Invoke(() =>
			{
				Categories.Clear();
				foreach (var category in newCategories)
					Categories.Add(category);

				ResetPieSlices();

				AddPieSlices();

				IsLoading = false;
			});
		});
	}

	private void ResetPieSlices()
	{
		PieSlices.Clear();
	}

	protected abstract Task<IEnumerable<CategoryViewModel>> GetCategories();

	private void AddPieSlices()
	{
		_centerX = ChartWidth / 2;
		_centerY = ChartHeight / 2;
		_radius = ChartHeight / 2;

		_angle = 0;
		_prevAngle = 0;
		foreach (CategoryViewModel category in Categories)
		{
			PieSliceGeometryViewModel sliceGeo = CreatePieSliceGeometry(category);
			PieSliceViewModel pieSlice = _pieSliceViewModelFactory.Create(category, sliceGeo);
			PieSlices.Add(pieSlice);
		}
	}

	private PieSliceGeometryViewModel CreatePieSliceGeometry(CategoryViewModel categoryVm)
	{
		Category category = categoryVm.Category;
		float percentage = category.Percentage;
		if (percentage == 100) percentage = 99.99f; // Prevents geometry errors

		double line1X = _radius * Math.Cos(_angle * Math.PI / 180) + _centerX;
		double line1Y = _radius * Math.Sin(_angle * Math.PI / 180) + _centerY;

		_angle = PercentToAngle(percentage) + _prevAngle;

		double arcX = _radius * Math.Cos(_angle * Math.PI / 180) + _centerX;
		double arcY = _radius * Math.Sin(_angle * Math.PI / 180) + _centerY;

		var line1Segment = new LineSegment(new Point(line1X, line1Y), false);

		double arcWidth = _radius, arcHeight = _radius;
		bool isLargeArc = percentage > 50;
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
			SliceFill = categoryVm.Color,
			SliceGeometryData = pathGeometry,
			OutlineColor = (SolidColorBrush)new BrushConverter().ConvertFrom("White"),
			OutlineThickness = 2,
		};

		_prevAngle = _angle;

		return sliceGeometry;
	}

	private static float PercentToAngle(float percent)
	{
		return percent * 360 / 100;
	}
}
