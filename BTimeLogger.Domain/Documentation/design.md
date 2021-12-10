# BTimeLogger.Domain

**Description**: This project contains the core model types associated with the app, as well as the services which act upon them.

**Project status**: feature-complete, not fully tested, test failures, and no known bugs

**Author**: Andrew Richerson

## Dependencies

-   [Microsoft Dependency Injection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection) - dependency injection library for .NET

Exact dependency details can be seen in `BTimeLogger.Domain.csproj`.

## Software Architecture

The architecture of this layer is very simple.

### Model

Within the `Model` directory, you will find the model types used throughout the app.

The main model types include:

-   `Activity` - A unique activity type only, not including any interval data
-   `ActivityCode` - Used to uniquely identify activities
-   `Interval` - The time spent doing a certain activity and the data that pertains to tracking an activity
-   `Statistic` - Statistical data about time spent doing a certain activity within a specific timespan
-   `GroupStatistic` - Statistical data about time spent doing activites which are found within the activity group. A parent of `Statistic` objects for each child activity in the activity group
-   `PaginatedList`- A generic collection which represents a portion of potentially larger list
-   `PagingParams` - Paging parameters for querying and creating paginated lists

### Services

Within the `Services` directory, you will find repositories for the `Activity` and `Interval` types. Repositories are abstractions around a data store. For now, data is stored in-memory until the user decides to save to a `.csv` file.

The `StatisticsGenerator` and `GroupStatisticGenerator` services are also found here, which are responsible for creating a `Statistic` given activity data.

## Testing & Test Results
