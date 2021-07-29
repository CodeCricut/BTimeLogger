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
		private static Color _baseColor = Color.FromRgb(72, 89, 218);

		public IEnumerable<Category> Convert(GroupStatistic groupStat)
		{
			List<Category> cats = new();
			foreach (GroupStatistic child in groupStat.Children)
				cats.Add(ConvertChild(child));
			return cats;
		}

		private Category ConvertChild(GroupStatistic child)
		{
			return new Category()
			{
				Color = ColorUtil.GetCloseColor(_baseColor, 150), // TODO: IDK how to generate color honestly. maybe just a hash
				Percentage = (float)child.PercentParent,
				Title = child.ActivityType.Name
			};
		}

	}
}
