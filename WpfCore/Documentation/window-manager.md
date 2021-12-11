# Window Manager

The [Window Manager pattern](http://nichesoftware.co.nz/2015/08/23/wpf-window-manager.html) is a pattern used for managing the creation and destruction of windows. It allows you perform these actions on windows without having to reference
the views from the view models.

In this pattern, every view (including both components and windows/dialogs) is associated with a view model. Views can be
opened and closed by telling the view manager to close the view associated with a view model.

## Example

The pattern is best explained with an example:

```csharp
class MainMenuViewModel : BaseViewModel {
    private readonly IViewManager _viewManager;

    public DelegateCommand OpenSettingsCommand { get; }

    public MainMenuViewModel(IViewManager viewManager){
        _viewManager = viewManager;
    }

    private void OpenSettings(object obj = null){
        SettingsViewModel settingsVM = new SettingsViewModel(); // All views must be associated with a view model
        _viewManager.ShowDialog(settingsVM); // Show a dialog without dealing with the dialog instantiation directly
    }
}
```

## IViewManager

The `IViewManager` interface found in `Services` provides the contract for showing and closing views. It could be implemented
in a number of ways specific to the presentation framework, so a concrete implementation must be done in another project.

**A concrete implementation of `IViewManager` should be registerd with the DI container for use in the application.**

## IHaveViewModel<TViewModel>

The `IHaveViewModel<TViewModel>` interface provides a fluent API for specifying what view models are associated with views (windows and dialogs).

Each window's code-behind should implement this interface with the corresponding view model:

```csharp
partial class SettingsWindow : Window, IHaveViewModel<SettingsViewModel> {
    ...
    public void SetViewModel(SettingsViewModel viewModel) => this.ViewModel = viewModel;
}
```

**The concrete implementation of `IViewManager` will likely scan the assembly for views implemeneting this interface.** For example, the view manager might look something like this:

```csharp
public void Show<TViewModel>(TViewModel viewModel) ... {
    Window view = FindViewForViewModel(viewModel);
    ...
}

private Window FindViewForViewModel(TViewModel viewModel) {
    // Scan assembly for Windows implementing IHaveViewModel<TViewModel>
}
```
