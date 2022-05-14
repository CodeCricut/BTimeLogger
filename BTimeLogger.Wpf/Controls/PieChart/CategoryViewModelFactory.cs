using BTimeLogger.Wpf.Model;
using System.Windows.Media;

namespace BTimeLogger.Wpf.Controls;

public interface ICategoryViewModelFactory
{
	CategoryViewModel Create(string title, float percentage, Color color);
	CategoryViewModel Create(Category category);
}

public class CategoryViewModelFactory : ICategoryViewModelFactory
{
	public CategoryViewModel Create(string title, float percentage, Color color)
	{
		Category category = new Category() { Title = title, Percentage = percentage, Color = color };
		return Create(category);
	}

	public CategoryViewModel Create(Category category)
	{
		return new CategoryViewModel(category);
	}
}
