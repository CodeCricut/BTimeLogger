using System.Windows.Input;

namespace BTimeLogger.Wpf.Controls;

public class PieSliceViewModel : BaseViewModel
{
	public CategoryViewModel Category { get; }
	public PieSliceGeometryViewModel SliceGeometry { get; }

	public ICommand SelectSliceCommand { get; }

	public PieSliceViewModel(
		CategoryViewModel categoryViewModel,
		PieSliceGeometryViewModel sliceGeometry)
	{
		Category = categoryViewModel;
		SliceGeometry = sliceGeometry;
	}
}
