using System;
using System.IO;
using System.Linq;

namespace Trains.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args == null || args.Length != 3)
            {
                Console.Write("This program requires 3 arguments. Type -h for help");
                Environment.Exit(1);
            }

            // Args 0 - File path
            var filePath = args[0];
            if (!File.Exists(filePath))
            {
                Console.Write("File does not exist. Please make sure you point to the correct location");
                Environment.Exit(1);
            }

            // Args 1 - Command
            var command = args[1];
            var possibleCommands = new string[] { "-d", "-tm", "-te", "-s", "-n" };
            if (!possibleCommands.Contains(command))
            {
                Console.Write("Unknown command. Type -h for help");
                Environment.Exit(1);
            }

            // Args 2 - Query
            var query = args[2];

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
                string journey = string.Format("{0}{1}", query[0], query[1]);
                int maxDistance = int.Parse(string.Format("{0}{1}", query[2], query[3]));
                var allRoutes = railNetwork.AllRoutesWithin(journey, maxDistance);
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
