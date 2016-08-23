using System;
using System.Collections.Generic;
using System.IO;

namespace Trains.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var filePath = args[0];
            if (File.Exists(filePath))
            {
                Console.Write("File exists");
            }
            else
            {
                Console.Write("No such file");
            }

            var railNetwork = Bootstrap(filePath);
        }

        private static RailNetwork Bootstrap(string filePath)
        {
            var mapRespository = new MapRespository(filePath);
            return new RailNetwork(
                 new DistanceCalculator(mapRespository),
                 new StationTracker(mapRespository),
                 new JourneyPlanner(mapRespository
                     )
                 );
        }

    }


}
