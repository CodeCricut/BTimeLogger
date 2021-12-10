# BTimeLogger.Csv

**Description**: This library contains the models and services used to read and write application data to and from CSV files. It provides a very simple/abstract way for other layers to read and write data without having to deal with the intricacies of data conversion and file manipulation.

**Project status**: feature-complete, not fully tested, test failures, and no known bugs

**Author**: Andrew Richerson

## Dependencies

-   [Csv Helper](https://joshclose.github.io/CsvHelper/) - A library for reading and writing CSV files

-   [Microsoft Dependency Injection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection) - dependency injection library for .NET

Exact dependency details can be seen in `BTimeLogger.Csv.csproj`.

## Software Architecture

The architecture of this layer is very simple.

### Model

Within the `Model` directory, you will find the model types which correspond to records in CSV files.

Namely, you will find `CsvIntervalRecord` which represents records storing `Interval`s in CSV files.

### Services

Within the `Services` directory, you will find the following services:

-   `IntervalsCsvReader` - reads a CSV file containing interval records and adds the intervals and activities found in it to the corresponding repository (either `IIntervalRepository` or `IActivityRepository`).
-   `IntervalsCsvWriter` - writes data from the activity and interval repositories into a CSV file
-   `CsvLocationsPrincipal` - a very simple service for maintaining the location of the CSV file to read and write to

The `DependencyInjection` class can be used by projects depending on this one to register available services to a DI Container.

## Testing & Test Results

Unit tests for library can be found in the `BTimeLogger.Csv.Tests` project.

Currently, the code is not fully tested and not all tests are passing.
