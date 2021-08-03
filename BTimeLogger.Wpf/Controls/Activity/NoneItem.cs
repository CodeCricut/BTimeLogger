using System;

namespace BTimeLogger.Wpf.Controls
{
	public class NoneItem : ActivityViewModel
	{
		public NoneItem() : base(new Activity()
		{
			Children = Array.Empty<Activity>(),
			IsGroup = true,
			Name = "None",
			Parent = null
		})
		{
		}
	}
}
