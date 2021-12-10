# BTimeLogger

#### A time tracking app

**Description**: BTimeLogger is a desktop app which allows users to track and analyze how they spend their time on a daily basis.

**Project status**: feature-incomplete, not fully tested, test failures, and known bugs

**Authors**:

-   Andrew Richerson <aricherson2@huskers.unl.edu>

## Features

### Create new activities

Users have the ability to create new "activities," which are just labels for the real-life activities users can track (ex. "Reading" or "Exercising").
Activities can be grouped, so a user might choose to make a group like "School" and put activities such as "Math" and "Reading" in it.

![Image demonstration of the create new activity feature](/Resources/Create_New_Activity_Demo.png)

### Create new intervals

Once an activity is made, users can create "intervals" which are just the timespans in which a user spent doing a certain activity. For example,
the user might enter a interval saying they did the "Reading" activity for an hour.

![Image demonstration of the create new interval feature](/Resources/Create_New_Interval_Demo.png)

### View past intervals

Users can view the list of tracked intervals, and filter by activity type and date.

![Image demonstration of the intervals feature](/Resources/Intervals_Demo.png)

### View interval statistics

Users can generate and view statistics about how they spend their time. For any interval or group, the user can view:

-   total time spent
-   percentage of total time spent on an activity or group
-   percentage of group time spent on an activity

Users are given the ability to filter the data by activity type and data.

![Image demonstration of the statistics feature](/Resources/Statistics_Demo.png)

### Save data to a .csv file

Data can be saved to a `.csv` file:

![Image demonstration of the save feature](/Resources/Save_As_Demo.png)

Users can open existing reports:

![Image demonstration of the open report feature](/Resources/Open_Report_Demo.png)

A list of recent reports is saved for easy navigation:

![Image demonstration of the open recent reports feature](/Resources/Home_Recents_Demo.png)

## Cloning/downloading the project

To clone the repository, run the following commands in the directory you would like to clone the project: `git clone https://github.com/CodeCricut/BTimeLogger`.

## Running

To run the app without building the release version, run the following command in the `BTimeLogger` directory:

`dotnet run --project BTimeLogger.Wpf`

To run the release version of the app, publish the app with `dotnet publish --configuration release` and run the executable found at `BTimeLogger.Wpf/bin/Release/net5.0-windows/BTimeLogger.Wpf.exe` (the exact directory may vary depending on your system).

## Dependencies

The main dependencies of the solution include

-   [Microsoft Dependency Injection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection) - dependency injection library for .NET
-   [Scrutor](https://github.com/khellang/Scrutor) - DI extensions; used to register all depenendencies of a type (for example all view model factories) to the DI container
-   [MediatR](https://github.com/jbogard/MediatR) - used to implement global messaging using CQRS within the WPF app

Additional dependencies for the project can be found in the `.csproj` files of each project.

## Software Architecture

### Layers

BTimeLogger is split up into projects which act as functional layers:

1. Domain/Data layer (`BTimeLogger.Domain`)

This is the core layer of the application which depends upon no other layer. It contains the
model types used by the application (such as `Activity` and `Interval`), and services to interact with those models (such as `IntervalRepository`).

2. CSV layer layer (`BTimeLogger.Csv`)

This layer contains the models and services used to read and write application data to and from CSV files. It provides a very simple/abstract way for other tiers to read and write data without having to deal with the intricacies of data conversion and file manipulation.

3. Presentation/WPF layer (`BTimeLogger.Wpf` and `WpfCore`)

The presentation layer houses all the models, services, and views necessary for the WPF desktop app. It rests upon the previously mentioned layers for data management and CSV manipulation.

The `BTimeLogger.Wpf` app contains the application starting point (`App.OnStartup`).

The `WpfCore` is a class library that contains classes common to creating WPF apps. It is contained within its own project for modularity and so it may be used in other WPF apps.

### Services and Dependency Injection

[Dependency injection](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection) is used throughout the solution to achieve Inversion of Control between classes and their dependencies. Classes are dependent upon high-level abstractions rather than concrete implementation which results in more modular, testable, and maintainable functional units.

The `BTimeLogger.Domain`, `BTimeLogger.Csv`, and `WpfCore` projects contain a `DependencyInjection` class which registers all services of the project to a DI container. These projects can also request services from the layers they depend upon (ex. `BTimeLogger.Wpf` can have `BTimeLogger.Domain` dependencies injected into class constructors).

The `BTimeLogger.Wpf` project creates a DI container in the `App` class and registers all services for the app using the aforementioned `DependencyInjection` classes.

## Testing & Test Results

### Manual System Tests

### Unit Tests
