using BTimeLogger.Csv.Helpers;
using BTimeLogger.Domain;
using BTimeLogger.Wpf.Model;
using BTimeLogger.Wpf.Util;
using System.Collections.Generic;
using System.Windows.Media;

namespace BTimeLogger.Wpf.Services
{
	public interface IGroupStatisticCategoriesConverter
	{
		IEnumerable<Category> Convert(GroupStatistic groupStat);
	}

	class GroupStatisticCategoriesConverter : IGroupStatisticCategoriesConverter
	{
		public static Color BaseColor { get; } = Color.FromRgb(72, 89, 218);

		public IEnumerable<Category> Convert(GroupStatistic groupStat)
		{
			List<Category> cats = new();
			foreach (Statistic child in groupStat.Children)
				cats.Add(ConvertStatisticChild(child));
			return cats;
		}

		private Category ConvertStatisticChild(Statistic child)
		{
			return new Category()
			{
				Color = ColorUtil.GetCloseColor(BaseColor, 150),
				Percentage = (float)child.PercentOfTrackedTimeInTimespan,
				Title = child.Activity.Name,
				Note = child.Duration.ToCsvFormat(),
				Id = child.Activity.Code.ToString()
			};
		}

	}
}
