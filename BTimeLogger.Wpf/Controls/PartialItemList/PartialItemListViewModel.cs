using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace BTimeLogger.Wpf.Controls;

public abstract class PartialItemListViewModel<T> : BaseViewModel
{
	#region VM Props
	public ObservableCollection<T> LoadedItems { get; init; } = new ObservableCollection<T>();

	public int TotalLoaded => LoadedItems?.Count ?? 0;

	public abstract int TotalItems { get; }

	public string LoadedOutOfTotalItems => $"{TotalLoaded} / {TotalItems}";

	public abstract bool HasMoreItems { get; }

	private bool _loading;
	public bool Loading
	{
		get => _loading;
		set
		{
			Set(ref _loading, value);
			RaisePropertyChanged(nameof(IsEmpty));
		}
	}

	public bool IsEmpty { get => TotalItems <= 0 && !Loading; }
	#endregion

	public AsyncDelegateCommand LoadMoreItemsCommand { get; init; }
	public AsyncDelegateCommand ResetItemsCommand { get; init; }

	public PartialItemListViewModel()
	{
		LoadedItems.CollectionChanged += LoadedItemsChanged;
		LoadMoreItemsCommand = new AsyncDelegateCommand(LoadMoreItemsAsync, CanLoadMoreItems);
		ResetItemsCommand = new AsyncDelegateCommand(ResetItemsAsync);
	}

	public bool CanLoadMoreItems(object param = null) => HasMoreItems && !Loading;
	private async Task LoadMoreItemsAsync(object param = null)
	{
		Loading = true;
		IEnumerable<T> itemsLoaded = await GetCurrentItemsAsync();
		Loading = false;

		foreach (T item in itemsLoaded) LoadedItems.Add(item);
	}

	public event EventHandler ItemsReset;
	public async Task ResetItemsAsync(object param = null)
	{
		ItemsReset?.Invoke(this, new EventArgs());

		Loading = true;

		LoadedItems.Clear();
		await ResetToStartingItemsAsync();

		IEnumerable<T> itemsLoaded = await GetCurrentItemsAsync();
		Loading = false;

		foreach (T item in itemsLoaded) LoadedItems.Add(item);
	}

	protected abstract Task<IEnumerable<T>> GetCurrentItemsAsync();

	protected abstract Task ResetToStartingItemsAsync();

	private void LoadedItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		RaisePropertyChanged(ALL_PROPS_CHANGED);
		LoadMoreItemsCommand.RaiseCanExecuteChanged();
	}

}
