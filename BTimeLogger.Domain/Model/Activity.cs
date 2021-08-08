using System.Collections.Generic;

namespace BTimeLogger
{
	public partial class Activity
	{
		public string Name { get; set; } = string.Empty;
		public bool IsGroup { get; set; }
		public Activity Parent { get; set; }
		public bool HasParent { get => Parent != null; }
		public List<Activity> Children { get; set; } = new();

		public ActivityCode Code { get => ActivityCode.CreateCode(this); }
	}
}
