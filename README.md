# Trains

This application is written in C# .NET v4.5.2 and it's used to calculate distances and routes within a rail network (graph database).

## Installation Instructions

### Prerequisites

1. [Docker](https://docs.docker.com/engine/installation/). Please make sure the Docker daemon is running before you continue.

2. Make (Don't worry if you don't have make installed, all the commands are wrapper in ```Makefile``` within the source code). Open ```Makefile``` to view the commands.

## Assumptions

+ The data or rail network map consists of routes: ```[StationFrom][StationTo][DistanceBetween]```, eg ```AB5```

+ Each route must be comma seperated ```AB5, BC4```

+ The station names must only be one alpha-character ```[a-zA-Z]```

+ The distance may be of any integer size greater than ZERO

+ The application treats the unit of distance as *miles*. This is to help prevent connascence of meaning. I.e. to prevent the coupling between the application knowing that an ```int``` represents some unit of distance

## Run the application

1. Unzip the contents of TrainsCSharpCR

2. Open up your preferred terminal and browse to TrainsCSharpCR

3. Build the docker container and run the tests: ```make```

4. You can then run the executable, ```Trains.App.exe``` with the following syntax:

    ```docker run trains mono Trains.App.exe [FILEPATH] [COMMAND] [QUERY]```
    
    ###NB. the filepath, in the above command, is relative to the entry point within the Docker container. Dockerfile adds everything within TrainsCSharpCR to /src, therefore Graph.txt can be located within the Docker container at /src/data/Graph.txt###
    
    For example, to find the distance of the route A-B-C:
    
    ```docker run trains mono Trains.App.exe /src/data/Graph.txt -d ABC``` (copy this exact command)
    
    If you need to bring up the help menu:
    
    ```docker run trains mono Trains.App.exe -h```
    
    ###```docker run trains``` will open and run the Docker container tagged 'trains'###
    

## Data text file

If you want to add another data file, simply place the file into the ```data``` folder, build and run the container again. You can then point the application to that file by specifying the path in the command line

## Commands

There are 6 commands:

* ```-d```   Distance between given stops

     ```docker run trains mono Trains.App.exe [FILEPATH] -d ABC```
     
* ```-h```   Help menu

     ```docker run trains mono Trains.App.exe -h```
     
* ```-s```   Length of the shortest route between two given stops

     ```docker run trains mono Trains.App.exe [FILEPATH] -s AC```
     
* ```-n```   Number of different routes between two given stops under a certain distance

     ```docker run trains mono Trains.App.exe [FILEPATH] -n CC30```
     
* ```-te```  Exact number of trips between two given stops

     ```docker run trains mono Trains.App.exe [FILEPATH] -te AC4```
     
* ```-tm```  Max number of trips between two given stops

     ```docker run trains mono Trains.App.exe [FILEPATH] -tm CC3```
     
     
The application exits immediately after displaying the result.

## Queries

The queries vary depending on which command you choose:

+ ```-d```     This needs to display the entire route and have a minimum of two stations ```AB``` or ```ABCDE```

+ ```-s```     This must contain only two stations ```AC``` or ```BD```

+ ```-n```     This must contain two stations and an integer representing the maximum distance between the two stations ```CC30``` or ```AE20```

+ ```-te```    This must contain two stations and an integer representing the exact number of trips between the two stations ```AC4``` or ```BD10```

+ ```-tm```    This must contain two stations and an integer representing the maximum number of trips between the two stations ```CC3``` or ```CE15```
