using BTimeLogger.Wpf.Model;
using BTimeLogger.Wpf.Util;
using System;
using System.Windows.Media;

namespace BTimeLogger.Wpf.Controls;

public class CategoryViewModel : BaseViewModel
{
	public Category Category { get; }

	public string Title
	{
		get => Category.Title;
		set
		{
			if (!Category.Title.Equals(value))
			{
				Category.Title = value;
				RaisePropertyChanged();
			}
		}
	}

	public string Percentage
	{
		get => Category.Percentage.ToString("n2");
	}

	public SolidColorBrush Color
	{
		get => Category.Color.ToSolidColorBrush();
		set
		{
			if (!Category.Color.ToSolidColorBrush().Equals(value))
			{
				Category.Color = value.Color;
				RaisePropertyChanged();
			}
		}
	}

	public string Note
	{
		get => Category.Note; set
		{
			if (!Category.Note.Equals(value))
			{
				Category.Note = value;
				RaisePropertyChanged();
			}
		}
	}

	public string Id => Category.Id;

	public CategoryViewModel(Category category)
	{
		Category = category ?? throw new ArgumentNullException(nameof(category));
	}
}
