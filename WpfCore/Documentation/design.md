# WpfCore

**Description**: `WpfCore` is a class library that contains classes common to creating WPF apps. It is contained within its own project for modularity and so it may be used in any WPF app.

**Project status**: feature-complete, not fully tested, test failures, and no known bugs

**Author**: Andrew Richerson

## Dependencies

-   [Microsoft Dependency Injection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection) - dependency injection library for .NET

Exact dependency details can be seen in `WpfCore.csproj`.

## Software Architecture

The architecture of this layer is very simple.

### Commands

### Message Bus

### Services

Within the `Services` directory, you will find the following services:

The `DependencyInjection` class can be used by projects depending on this one to register available services to a DI Container.

### View

### View Model

## Testing & Test Results

Unit tests for library can be found in the `BTimeLogger.Csv.Tests` project.

Currently, the code is not fully tested and not all tests are passing.
