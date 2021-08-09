namespace BTimeLogger.Wpf.Controls
{
	public interface IPieSliceViewModelFactory
	{
		PieSliceViewModel Create(CategoryViewModel categoryVm, PieSliceGeometryViewModel pieSliceGeometryVm);
	}
	class PieSliceViewModelFactory : IPieSliceViewModelFactory
	{
		public PieSliceViewModel Create(CategoryViewModel categoryVm, PieSliceGeometryViewModel pieSliceGeometryVm)
		{
			return new PieSliceViewModel(categoryVm, pieSliceGeometryVm);
		}
	}
}
