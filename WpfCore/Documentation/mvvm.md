# Model-View-ViewModel

This library is designed to be the core of a WPF app using [MVVM architecture](https://docs.microsoft.com/en-us/archive/msdn-magazine/2009/february/patterns-wpf-apps-with-the-model-view-viewmodel-design-pattern). The MVVM pattern separates
concerns such as business logic and presentation logic.

## Model

The model deals with business logic, data persistence, and other behavior not specific to the presentation. The model should be completely decoupled from the view and not rely on it in any way.

## View

The view is only responsible for the presentation of the app---not the presentation logic
or interaction with the model. The view is synced with the ViewModel using data binding.

## ViewModel

ViewModels contain presentation logic and mediate interaction between the view and the model. The View never performs any of this presentation logic or interaction with the model directly.

ViewModel state is synced with View state via data binding, and the ViewModel needs no reference to the View. This leads to testable ViewModels which don't require UI testing.

# Data Binding and Commands

## Binding Views to ViewModels

ViewModel state is propogated to views via data binding. The view can bind UI events such as button clicks to commands in the view model.

To achieve data binding between views and view models, view models must implement
`INotifyPropertyChanged`. The `BaseViewModel` class provides the default implementation of
this interface and should be the base type of all view models in the application.

The typical view model deriving from this class will look something like this:

```csharp
class NoteViewModel : BaseViewModel {
    private string _text;
    public string Text {
        get => _text;
        set => Set(ref _text, value); // To propogate changes to the view, use Set when changing view model properties
    }
}
```

The view's code-behind has a property for the view model to which the view can bind to (the dependency property will be explained later):

```csharp
partial class Note : UserControl {
    public NoteViewMode ViewModel {
        get => (NoteViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }
    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register("ViewModel", typeof(NoteViewModel), typeof(Note), new PropertyMetadata(null));

    public Note() { InitializeComponent(); }
}
```

The view can then bind to the view model by setting the data context of the outermost element nested within `UserControl` (note that the data context of the `UserControl` should not be changed).

```xml
<!--Note.xaml-->
<UserControl x:Class="Note"
    ...>
    <Grid DataContext="{Binding RelativeSource={RelativeSource Mode=FindAncestor, AncestorType={x:Type Note}}}">
        <TextBox Text="{Binding ViewModel.Text, Mode=TwoWay}" />
...
```

### Injecting ViewModels into Views

ViewModels can be injected into user controls like `Note` via dependency properties. The view which contains the user control could pass in the note view model like so:

```xml
<!--View containing the Note view-->
...
<Note ViewModel="{Binding ViewModel.NoteViewModel}" />
```

Note that the `NoteViewModel` here is a property of the view model that the view containing the note is bound to.

## Adding view behavior with Commands

WPF uses [commands](https://docs.microsoft.com/en-us/dotnet/api/system.windows.input.icommand?view=net-6.0) deriving from `ICommand` to specify behavior in the ViewModel to which the View can invoke.

Commands provide the commanding behavior for UI elements such as `Button`s; they let
you define what action should be taken when certain events happen within the UI (like when a button is clicked).

Within the `Commands` directory, you'll find the following classes:

`DelegateCommand` - simple implementation of `ICommand` which allows you use a delegate action to define command behavior

The `ICommand` interface is cumbersome to implement for every command in the application. This class provides the common functionality of commands and allows you to simply pass a
delegate to invoke when the command is executed.

-   `AsyncDelegateCommand` - same as `DelegateCommand` save for the fact that you can define an asynchronous function to call when the command is executed

The typical use of a command would look like this:

```xml
<!--Note.xaml-->
...
<Button Command={Binding ViewModel.SaveCommand} Content="Save" />
...
```

```csharp
class NoteViewModel : BaseViewModel {
    public DelegateCommand SaveCommand { get; }

    public NoteViewModel(){
        SaveCommand = new DelegateCommand(Save);
    }

    private void Save(object obj = null) { ... }
}
```
