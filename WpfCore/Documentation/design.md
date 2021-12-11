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

More details can be found in `mvvm.md`.

### Window Manager

A [window manager](http://nichesoftware.co.nz/2015/08/23/wpf-window-manager.html) is available to handle the details of window creation and destruction, and to decouple views
from view models.

More details can be found in `window-manager.md`.

### Message Bus (Event Aggregator)

The [Event Aggregator pattern](http://www.nichesoftware.co.nz/2015/08/16/wpf-event-aggregates.html) is a pattern used to handle communication between disparate parts of the application without those parts having references to each other. Instead, a
central "message bus" or "event aggregator" is referenced by both and brokers messages.

The interface and default implementation of an event aggregator can be found within the `MessageBus` directory.

### Services

The `DependencyInjection` class can be used by projects depending on this one to register available services to a DI Container.

## Testing & Test Results

Unit tests for library can be found in the `BTimeLogger.Csv.Tests` project.

Currently, the code is not fully tested and not all tests are passing.
