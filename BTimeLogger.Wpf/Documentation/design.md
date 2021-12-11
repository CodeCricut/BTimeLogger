# BTimeLogger.Wpf

**Description**: `BTimeLogger.Wpf` is a WPF desktop app, and is the main project of the solution. It contains
the entry point of the application and the logic necessary to bootstrap the app, including configuration and resolving dependencies.

**Project status**: feature-incomplete, not fully tested, test failures, and no known bugs

**Author**: Andrew Richerson

## Dependencies

-   [Scrutor](https://github.com/khellang/Scrutor) - DI extensions; used to register all depenendencies of a type (for example all view model factories) to the DI container
-   [MediatR](https://github.com/jbogard/MediatR) - used to implement the mediator pattern for global messaging
-   [Microsoft Dependency Injection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection) - dependency injection library for .NET

Exact dependency details can be seen in `BTimeLogger.Wpf.csproj`.

## Software Architecture

### Program Entry Point and Configuration

The `App.xaml.cs` provides a hook into the startup process of the WPF application. It encapsulates startup logic such as configuration and
building the DI container.

App configuration details can be found in `Configuration/AppConfiguration.cs` and `appsettings.json`.

### MVVM and WpfCore

The `BTimeLogger.Wpf` app is built on top of `WpfCore` and utilizes the patterns offered by it such as MVVM, the event aggregator pattern, and the window manager pattern. More details can be found in the `documentation` directory of the `WpfCore` project.

### Windows and Controls

Views do not contain the presentation or business logic associated with them. Instead, they are bound to a ViewModel which performs this logic. More details can be found in `mvvm.md` in the `WpfCore` project.

All windows are associated with a view model and implement `IHaveViewModel<TViewModel>`. More details can be found in `window-manager.md` in the `WpfCore` project.

### View Management

Some implementation details pertaining to window/view management are omitted from the `WpfCore` project.

These classes are implemented within the `Services/ViewManagment` directory of this project.

Namely, `ViewManager` provides a concrete implementation of `IViewManager` for the management of windows. It is registered with the DI container and will be injected to any class that requests an `IViewManager`.

`ViewFinder` is a helper class for `ViewManager` which finds the associated view in the assembly for a given view model. It works by looking for views implementing the `IHaveViewModel<TViewModel>` interface (as defined by `WpfCore`).

### Services

Services implemented by this project can be found in the `Services` directory. The main services include:

-   `AppData/AppDataService` - service for working with files in the app data directory (where persistent data can be stored)
-   `AppData/ReportLocationsPrincipal` - service for maintaining a persistent store of paths to CSV reports (used to maintain a copy of recently opened CSV reports)
-   `ViewManagement/ViewManager` - implementation of `IViewManagers`; described above
-   `ViewManagement/ViewFinder` - implementation of `IViewFinder`; described above
-   `CsvChangeTracker` - keeps track of if changes have been made to the CSV report
-   `StatisticCategoryConverter` - converter between `GroupStatistic` domain model type to WPF `Category` model type (used for pie charts)
-   `SkinManager` - manager for the app's skin/theme

The `Configuration/DependencyInjection` class registers services implemented within this project to the DI container.

### Mediator and App Messages

The [Mediator pattern](https://en.wikipedia.org/wiki/Mediator_pattern) is used by the application for global message handling. The app uses the popular [MediatR library](https://github.com/jbogard/MediatR) implementation of the pattern.

Messages and message handlers are found in the `Mediator` directory. Request handlers can
have dependencies injected into them, allowing them to be very flexible and decoupled:

```csharp
// Mediator/Save.cs

// Messages are simple classes implementing IRequest or IRequest<TResponse>
public class Save : IRequest { }

// Handlers are self-contained classes used to handle messages
public class SaveHandler : IRequestHandler<Save> {
    ...
    // Any registered service can be injected into handlers
    public SaveHandler(IViewManager viewManager, ... ) { ... }

    public async Task<Unit> Handle(Save request, CancellationToken){
        // Handle the message, possibly using services
    }
}
```

A `IMediator` object can be injected anywhere, such as ViewModels, to send messages:

```csharp
// TitleBarMenuViewModel
...

// A mediator object can be injected to classes where messages need to be sent from
public TitleBarMenuViewModel(IMediator mediator, ...) { ... }

private Task Save(object obj = null){
    // Sent messages will automatically be handled
    return _mediator.Send(new Save());
}
```

## Testing & Test Results

The project currently has no automated tests and has only been manually tested. Not all manual tests are passing.
