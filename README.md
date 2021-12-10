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

-   MediatR
-   Scrutor

Additional dependencies for the project can be found in the `.csproj` files of each project.

## Software Architecture

## Testing & Test Results

### Manual System Tests

### Unit Tests
