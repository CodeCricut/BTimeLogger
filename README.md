# BTimeLogger

#### A time tracking app

**Description**: BTimeLogger is a desktop app which allows users to track and analyze how they spend their time on a daily basis.

**Project status**: feature-incomplete, not fully tested, test failures, and known bugs

**Author**: Andrew Richerson

## Features

-   Track time spent doing different activities
-   View list of past intervals
-   View a pie chart to analyze how you spend your time
-   Filter by activity group, type, and time range
-   Save to .csv for compatibility with programs like Microsoft Excel
-   Sleek design with light and dark themes

## Demo

[![A video demo of the app](Resources\video-demo-thumb.png)](https://vimeo.com/709151098)

## Cloning/downloading the project

To download the executable, download the latest `.zip` asset in the [GitHub releases page](https://github.com/CodeCricut/BTimeLogger/releases). Unzip the file and you should find a file named `setup.exe` which you can run to install the project to a Windows machine.

Alternatively, you may also clone and build the project yourself.

To clone the repository, run the following commands in the directory you would like to clone the project: `git clone https://github.com/CodeCricut/BTimeLogger`.

## Running

To run the app without building the release version, run the following command in the `BTimeLogger` directory:

`dotnet run --project BTimeLogger.Wpf`

To run the release version of the app, publish the app with `dotnet publish --configuration release` and run the executable found at `BTimeLogger.Wpf/bin/Release/net5.0-windows/BTimeLogger.Wpf.exe` (the exact directory may vary depending on your system).

## Dependencies

The main dependencies of the solution include

-   [Microsoft Dependency Injection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection) - dependency injection library for .NET
-   [Scrutor](https://github.com/khellang/Scrutor) - DI extensions; used to register all depenendencies of a type (for example all view model factories) to the DI container
-   [MediatR](https://github.com/jbogard/MediatR) - used to implement global messaging within the WPF app

Additional dependencies for the project can be found in the `.csproj` files of each project.

## Software Architecture

### Layers

BTimeLogger is split up into projects which act as functional layers:

1. Domain/Data layer (`BTimeLogger.Domain`)

This is the core layer of the application which depends upon no other layer. It contains the
model types used by the application (such as `Activity` and `Interval`), and services to interact with those models (such as `IntervalRepository`).

Additional documentation can be found in the `Documentation/design.md` file of the project.

2. CSV layer layer (`BTimeLogger.Csv`)

This layer contains the models and services used to read and write application data to and from CSV files. It provides a very simple/abstract way for other tiers to read and write data without having to deal with the intricacies of data conversion and file manipulation.

Additional documentation can be found in the `Documentation/design.md` file of the project.

3. Presentation/WPF layer (`BTimeLogger.Wpf` and `WpfCore`)

The presentation layer houses all the models, services, and views necessary for the WPF desktop app. It rests upon the previously mentioned layers for data management and CSV manipulation.

The `BTimeLogger.Wpf` app contains the application starting point (`App.OnStartup`).

The `WpfCore` is a class library that contains classes common to creating WPF apps. It is contained within its own project for modularity and so it may be used in other WPF apps.

Additional documentation can be found in the `Documentation` directories of both projects.

### Services and Dependency Injection

[Dependency injection](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection) is used throughout the solution to achieve Inversion of Control between classes and their dependencies. Classes are dependent upon high-level abstractions rather than concrete implementation which results in more modular, testable, and maintainable functional units.

The `BTimeLogger.Domain`, `BTimeLogger.Csv`, and `WpfCore` projects contain a `DependencyInjection` class which registers all services of the project to a DI container. These projects can also request services from the layers they depend upon (ex. `BTimeLogger.Wpf` can have `BTimeLogger.Domain` dependencies injected into class constructors).

The `BTimeLogger.Wpf` project creates a DI container in the `App` class and registers all services for the app using the aforementioned `DependencyInjection` classes.

## Testing & Test Results

### Manual System Tests

Manual system tests of the WPF application have been performed (note that these
tests are not yet documented).

The manual system tests do not achieve 100% coverage and not all tests are passing.

### Unit Tests

The `WpfCore`, `BTimeLogger.Domain`, and `BTimeLogger.Csv` projects have NUnit tests projects associated with them. More details can be found in the design documents of each project.

The automated unit tests do not achieve 100% coverage and not all tests are passing.
