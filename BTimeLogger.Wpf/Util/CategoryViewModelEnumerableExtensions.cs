using BTimeLogger.Wpf.Controls;
using BTimeLogger.Wpf.Model;
using BTimeLogger.Wpf.Services;
using System.Collections.Generic;
using System.Linq;

namespace BTimeLogger.Wpf.Util
{
	public static class CategoryViewModelEnumerableExtensions
	{
		public static IEnumerable<CategoryViewModel> RemoveCategoriesBelowPercentThreshold(this IEnumerable<CategoryViewModel> categories, double minPercent = 1)
		{
			if (categories.Count() <= 0) return categories.ToList();

			return categories.Where(catVM => catVM.Category.Percentage >= minPercent).ToList();
		}

		public static List<CategoryViewModel> AddOtherCategory(this List<CategoryViewModel> categories, string otherCatTitle = "Other")
		{
			if (categories.Count() <= 0) return categories;

			float remainingPercentage = 100 - categories.Select(catVM => catVM.Category.Percentage).Aggregate((p1, p2) => p1 + p2);

			CategoryViewModel otherCategory = new(new Category()
			{
				Color = GroupStatisticCategoriesConverter.BaseColor,
				Percentage = remainingPercentage,
				Title = otherCatTitle
			});
			categories.Add(otherCategory);

			return categories;
		}
	}
}
