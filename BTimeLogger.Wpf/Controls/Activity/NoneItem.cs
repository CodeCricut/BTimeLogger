namespace BTimeLogger.Wpf.Controls;

public class NoneItem : ActivityViewModel
{
	public NoneItem() : base(new Activity()
	{
		IsGroup = true,
		Name = "None",
		Parent = null
	})
	{
	}
}
