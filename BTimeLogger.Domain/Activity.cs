namespace BTimeLogger
{
	public class Activity
	{
		public string Name { get; set; }
		public bool IsGroup { get; set; }
		public Activity Parent { get; set; }
		public bool HasParent { get => Parent != null; }
		public Activity[] Children { get; set; }
	}
}
