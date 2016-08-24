using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Trains.App
{
	public class Program
	{
		public static void Main(string[] args)
		{
			//args = new[] {"../../../Graph.txt", "-tm", "CC3"};
			

			if (args == null)
			{
				Console.WriteLine("This program requires 3 arguments. Type -h for help menu");
				Environment.Exit(1);
			}

			if (args.Contains("-h"))
			{
				Console.WriteLine();
				Console.WriteLine("HELP MENU");
				Console.WriteLine("---------------------------------------");
				Console.WriteLine("Trains.App.exe [FILEPATH] [COMMAND] [QUERY]");
				Console.WriteLine("---------------------------------------");
				Console.WriteLine("Here are the list of possible commands:");
				Console.WriteLine();
				Console.WriteLine("-d   Distance between two given stops");
				Console.WriteLine("-h   Help menu");
				Console.WriteLine("-s   Length of the shortest route between two given stops");
				Console.WriteLine("-n   Number of different routes between two given stops under a certain distance");
				Console.WriteLine("-te  Exact number of trips between two given stops");
				Console.WriteLine("-tm  Max number of trips between two given stops");

				Environment.Exit(0);
			}

			if (args.Length != 3 && !args.Contains("-h"))
			{
				Console.WriteLine("This program requires 3 arguments. Type -h for help");
				Environment.Exit(1);
			}

			var possibleCommands = new[] { "-d", "-tm", "-te", "-s", "-n" };


			// Args 0 - File path
			var filePath = args[0];
			if (!File.Exists(filePath))
			{
				Console.WriteLine("File does not exist. Please make sure you point to the correct location");
				Environment.Exit(1);
			}

			// Args 1 - Command
			var command = args[1];
			if (!possibleCommands.Contains(command))
			{
				Console.WriteLine("Unknown command. Type -h for help menu");
				Environment.Exit(1);
			}

			// Args 2 - Query
			var query = args[2];
			if (command == "-d")
			{
				var regex = new Regex(@"^[a-zA-Z]+$");
				if (!regex.IsMatch(query))
				{
					Console.WriteLine("Bad query. It must consist of a start station any number of named stations");
					Console.WriteLine("Eg. ABC or ABCDE");
					Environment.Exit(1);
				}
			}
			if (command == "-tm" || command == "-te" || command == "-n")
			{
				var regex = new Regex(@"^[a-zA-Z][a-zA-Z]\d+$");
				if (!regex.IsMatch(query))
				{
					Console.WriteLine("Bad query. It must consist of a start station, end station and a number of trips/distance.");
					Console.WriteLine("Eg. CC30 or AB4.");
					Environment.Exit(1);
				}
			}
			if (command == "-s" )
			{
				var regex = new Regex(@"^[a-zA-Z][a-zA-Z]$");
				if (!regex.IsMatch(query))
				{
					Console.WriteLine("Bad query. It must consist of a start station and an end station.");
					Console.WriteLine("Eg. AC or CC");
					Environment.Exit(1);
				}
			}

			// Initialise Bootstrap
			var railNetwork = Bootstrap(filePath);


			if (command.Equals("-d"))
			{
				var travelResult = railNetwork.Travel(query);
				Console.WriteLine(travelResult.Result);
			}
			if (command.Equals("-tm"))
			{
				var trips = railNetwork.Trips(query);
				Console.WriteLine(trips);
			}
			if (command.Equals("-te"))
			{
				var trips = railNetwork.TripsExact(query);
				Console.WriteLine(trips);
			}
			if (command.Equals("-s"))
			{
				var travelResult = railNetwork.Shortest(query);
				Console.WriteLine(travelResult.Result);
			}
			if (command.Equals("-n"))
			{
				var allRoutes = railNetwork.AllRoutesWithin(query);
				foreach (var r in allRoutes)
				{
					Console.WriteLine(r);
				}
			}
		}

		private static RailNetwork Bootstrap(string filePath)
		{
			var mapRespository = new MapRepository(filePath);
			return new RailNetwork(
				 new DistanceCalculator(mapRespository),
				 new StationTracker(mapRespository),
				 new JourneyPlanner(mapRespository
					 )
				 );
		}

	}


}
