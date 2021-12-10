# WpfCore

**Description**: `WpfCore` is a class library that contains classes common to creating WPF apps that use the MVVM architecture. It is contained within its own project for modularity and so it may be used in any WPF app.

**Project status**: feature-complete, not fully tested, test failures, and no known bugs

**Author**: Andrew Richerson

## Dependencies

-   [Microsoft Dependency Injection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection) - dependency injection library for .NET

Exact dependency details can be seen in `WpfCore.csproj`.

## Software Architecture

### Model-View-ViewModel (MVVM)

This library is designed to be the core of a WPF app using [MVVM architecture](https://docs.microsoft.com/en-us/archive/msdn-magazine/2009/february/patterns-wpf-apps-with-the-model-view-viewmodel-design-pattern). The MVVM pattern helps create a WPF application in which concerns like presentation logic and buisness logic are separate.

**Model**:
The model deals with business logic, data persistence, and other behavior not specific to the presentation.

**View**:
The view is only responsible for the presentation of the app---not the presentation logic
or interaction with the model. The view is synced with the ViewModel using data binding.

**ViewModel**:
"ViewModels" represent the data and UI logic of a view. ViewModel state is synced with View state, and the ViewModel needs no reference to the View. This leads to testable ViewModels which don't require UI testing.

ViewModel state is propogated to views via data binding. The view can bind UI events such as button clicks to commands in the view model.

ViewModels contain all logic related to presentation logic and interaction with the model layer; the View never performs any of this logic.

#### Commands

WPF uses [commands](https://docs.microsoft.com/en-us/dotnet/api/system.windows.input.icommand?view=net-6.0) deriving from `ICommand` to specify behavior in the ViewModel to which the View can invoke.

Commands provide the commanding behavior for UI elements such as `Button`s; they let
you define what action should be taken when certain events happen within the UI (like when a button is clicked).

Within the `Commands` directory, you'll find the following classes:

`DelegateCommand` - simple implementation of `ICommand` which allows you use a delegate action to define command behavior

The `ICommand` interface is cumbersome to implement for every command in the application. This class provides the common functionality of commands and allows you to simply pass a
delegate to invoke when the command is executed.

-   `AsyncDelegateCommand` - same as `DelegateCommand` save for the fact that you can define an asynchronous function to call when the command is executed

#### Message Bus

#### Services

Within the `Services` directory, you will find the following services:

The `DependencyInjection` class can be used by projects depending on this one to register available services to a DI Container.

#### View

#### View Model

## Testing & Test Results

Unit tests for library can be found in the `BTimeLogger.Csv.Tests` project.

Currently, the code is not fully tested and not all tests are passing.
