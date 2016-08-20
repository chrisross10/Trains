using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Trains
{
    public class RailNetwork
    {
        private readonly Dictionary<string, Distance> _map;

        public RailNetwork(Dictionary<string, Distance> map)
        {
            _map = map;
        }

        public TravelResult Travel(string journey)
        {
            if (string.IsNullOrEmpty(journey))
                return new TravelResult(null);

            var totalDistance = Distance.FromMiles(0);
            var route = SplitRoute(journey);
            foreach (var r in route)
            {
                if (_map.ContainsKey(r))
                {
                    totalDistance = totalDistance.Add(_map[r]);
                }
                else
                {
                    return new TravelResult(null);
                }
            }
            return new TravelResult(totalDistance);
        }

        private IEnumerable<string> SplitRoute(string journey)
        {
            var route = new List<string>();
            for (int i = 1; i < journey.Length; i++)
            {
                route.Add(string.Format("{0}{1}", journey[i - 1], journey[i]));
            }
            return route;
        }

        public int TripsRecursive(string start, string end, int numberOfTrips, int maxTrips, ref int counter)
        {
            var startTrips = GetAllTripsThatStartWith(start);
            numberOfTrips++;
            foreach (var trip in startTrips)
            {
                if (numberOfTrips > maxTrips)
                {
                    break;
                }
                if (trip.EndsWith(end))
                {
                    counter++;
                }
                TripsRecursive(trip[1].ToString(), end, numberOfTrips, maxTrips, ref counter);
            }
            return counter;
        }

        public int Trips(string journey)
        {
            string start = journey[0].ToString();
            string end = journey[1].ToString();
            int maxTrips = int.Parse(journey[2].ToString());
            int numberOfTrips = 0;
            int counter=0;

            return TripsRecursive(start, end, numberOfTrips, maxTrips, ref counter);
        }

        private List<string> GetAllTripsThatStartWith(string start)
        {
            return _map.Keys.Where(k => k.StartsWith(start)).ToList();
        }
    }
}