# BTimeLogger.Wpf

**Description**: `BTimeLogger.Wpf` is TODO

**Project status**: feature-incomplete, not fully tested, test failures, and no known bugs

**Author**: Andrew Richerson

## Dependencies

TODO

-   [Microsoft Dependency Injection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection) - dependency injection library for .NET

Exact dependency details can be seen in `BTimeLogger.Wpf.csproj`.

## Software Architecture

### Program Entry Point and Configuration

The `App.xaml.cs` provides a hook into the startup process of the WPF application. It encapsulates startup logic such as configuration and
building the DI container.

App configuration settings can be found in `Configuration/AppConfiguration.cs`.

### MVVM WpfCore

The `BTimeLogger.Wpf` app is built on top of `WpfCore` and utilizes the patterns offered by it such as MVVM, the event aggregator pattern, and the window manager. More details can be found in the `documentation` directory of the `WpfCore` project.

### View Management

Some implementation details pertaining to window/view management are omitted from the `WpfCore` project.

These classes are implemented within the `Services/ViewManagmenet` directory of this project.

Namely, `ViewManager` provides a concrete implementation of `IViewManager` for the management of windows. It is registered with the DI container and will be injected to any class that requests an `IViewManager`.

`ViewFinder` is a helper class for `ViewManager` which finds the associated view in the assembly for a given view model. It works by looking for views implementing the `IHaveViewModel<TViewModel>` interface (as defined by `WpfCore`).

### Services

Services implemented by this project can be found in `Services`. The main services include:

-   `AppData/AppDataService` - service for working with files in the app data directory (where persistent data can be stored)
-   `AppData/ReportLocationsPrincipal` - service for maintaining a persistent store of paths to CSV reports (used to maintain a copy of recently opened CSV reports)
-   `ViewManagement/ViewManager` - implementation of `IViewManagers`; described above
-   `ViewManagement/ViewFinder` - implementation of `IViewFinder`; described above
-   `CsvChangeTracker` - keeps track of if changes have been made to the CSV report
-   `StatisticCategoryConverter` - converter between `GroupStatistic` domain model type to WPF `Category` model type (used for pie charts)
-   `SkinManager` - manager for the app's skin/theme

The `Configuration/DependencyInjection` class registers services implemented within this project to the DI container.

### Views and Controls

### App Skin

### Mediator and App Messages

## Testing & Test Results

Unit tests for library can be found in the `BTimeLogger.Csv.Tests` project.

Currently, the code is not fully tested and not all tests are passing.
