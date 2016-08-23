using System;
using System.IO;

namespace Trains.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //args = new[] { @"..\..\..\Graph.txt", "-d", "ABC" };

            var filePath = args[0];
            if (!File.Exists(filePath))
            {
                Console.Write("File does not exist. Please make sure you point to the correct location");
            }

            var graph = File.ReadAllText(filePath);
            var railNetwork = Bootstrap(graph);

            var command = args[1];
            var query = args[2];

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

        private static RailNetwork Bootstrap(string graph)
        {
            var mapRespository = new MapRespository(graph);
            return new RailNetwork(
                 new DistanceCalculator(mapRespository),
                 new StationTracker(mapRespository),
                 new JourneyPlanner(mapRespository
                     )
                 );
        }

    }


}
