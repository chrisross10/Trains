# Trains

This application is written in C# .NET and it's used to calculate distances and routes within a rail network (graph database).

## Installation Instructions

### Make sure you have MSBuild installed.

## Windows
+ If you are using a Windows machine, make sure you have .NET 4.5.2 or higher [installed](https://www.microsoft.com/en-gb/download/details.aspx?id=48130) first.

+ MSBuild comes out of the box with .NET, so if you already have .NET installed, you won't need to install MSBuild.

## Mac/Linux
+ Please refer to the official MSBuild Github [repository](https://github.com/Microsoft/msbuild) for installation instructions on various platforms.

## Assumptions

+ The data or rail network map consists of routes: ```[StationFrom][StationTo][DistanceBetween]```, eg ```AB5```

+ Each route must be comma seperated ```AB5, BC4```

+ The station names must only be one alpha-character ```[a-zA-Z]```

+ The distance may be of any integer size greater than ZERO

+ The application treats the unit of distance as *miles*. This is to help prevent connascence of meaning. I.e. to reduce the coupling between the application knowing that an ```int``` represents some unit fo distance

## Run the application

1. Locate the MSBuild executable (eg. C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe) and remember the path

2. Unzip the contents of TrainsCSharpCR

3. Open up your preferred terminal and browse to TrainsCSharpCR

4. Build the solution by running the follwing command:

    ```C:\Windows\Microsoft.NET\...\MSBuild.exe Trains.sln /t:Rebuild```
    
5. ```cd Trains.Console/bin/Debug```

6. You can then run the executable, ```Trains.App.exe``` with the following syntax:

    ```Trains.App.exe [FILEPATH] [COMMAND] [QUERY]```
    
    For example, to find the distance of the route A-B-C:
    
    ```Trains.App.exe ../../../Graph.txt -d ABC```
    
    If you need to bring up the help menu:
    
    ```Trains.App.exe -h```

## Data text file

You can point the application to any other data file by specifying the relative or full path in the command line

## Commands

There are 6 commands:

* ```-d```   Distance between given stops

     ```Trains.App.exe [FILEPATH] -d ABC```
     
* ```-h```   Help menu

     ```Trains.App.exe -h```
     
* ```-s```   Length of the shortest route between two given stops

     ```Trains.App.exe [FILEPATH] -s AC```
     
* ```-n```   Number of different routes between two given stops under a certain distance

     ```Trains.App.exe [FILEPATH] -n CC30```
     
* ```-te```  Exact number of trips between two given stops

     ```Trains.App.exe [FILEPATH] -te AC4```
     
* ```-tm```  Max number of trips between two given stops

     ```Trains.App.exe [FILEPATH] -tm CC3```
     
     
The application exits immediately after displaying the result.

## Queries

The queries vary depending on which command you choose:

+ ```-d```     This needs to display the entire route and have a minimum of two stations ```AB``` or ```ABCDE```

+ ```-s```     This must contain only two stations ```AC``` or ```BD```

+ ```-n```     This must contain two stations and an integer representing the maximum distance between the two stations ```CC30``` or ```AE20```

+ ```-te```    This must contain two stations and an integer representing the exact number of trips between the two stations ```AC4``` or ```BD10```

+ ```-tm```    This must contain two stations and an integer representing the maximum number of trips between the two stations ```CC3``` or ```CE15```

