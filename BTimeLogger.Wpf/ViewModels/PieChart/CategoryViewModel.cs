using BTimeLogger.Wpf.Model;
using BTimeLogger.Wpf.Util;
using System;
using System.Windows.Media;
using WpfCore.ViewModel;

namespace BTimeLogger.Wpf.ViewModels.PieChart
{
	public class CategoryViewModel : BaseViewModel
	{
		private readonly Category _category;

		public string Title
		{
			get => _category.Title;
			set
			{
				if (!_category.Title.Equals(value))
				{
					_category.Title = value;
					RaisePropertyChanged();
				}
			}
		}

		public float Percentage
		{
			get => _category.Percentage;
			set
			{
				if (_category.Percentage != value)
				{
					_category.Percentage = value;
					RaisePropertyChanged();
				}
			}
		}

		public SolidColorBrush Color
		{
			get => _category.Color.ToSolidColorBrush();
			set
			{
				if (!_category.Color.ToSolidColorBrush().Equals(value))
				{
					_category.Color = value.Color;
					RaisePropertyChanged();
				}
			}
		}

		public CategoryViewModel(Category category)
		{
			_category = category ?? throw new ArgumentNullException(nameof(category));
		}
	}
}
