using System.Collections.Generic;
using System.Linq;

namespace Trains
{
    public class StationTracker : IStationTracker
    {
        private readonly IMapRepository _mapRepository;

        public StationTracker(IMapRepository mapRepository)
        {
            _mapRepository = mapRepository;
        }

        public int Trips(TripsQuery query)
        {
            int numberOfTrips = 0;
            int counter = 0;

            return TripsRecursive(query.Start, query.End, query.Trips, numberOfTrips, ref counter);
        }

        private int TripsRecursive(string start, string end, int maxTrips, int numberOfTrips, ref int counter)
        {
            var startTrips = GetAllTripsThatStartWith(start);
            numberOfTrips++;
            foreach (var trip in startTrips)
            {
                if (numberOfTrips > maxTrips)
                {
                    continue;
                }
                if (trip.End.Equals(end))
                {
                    counter++;
                }
                TripsRecursive(trip.End, end, maxTrips, numberOfTrips, ref counter);
            }
            return counter;
        }

        public int TripsExact(string journey)
        {
            string start = journey[0].ToString();
            string end = journey[1].ToString();
            int exactTrips = int.Parse(journey[2].ToString());
            int numberOfRoutes = 0;
            int counter = 0;

            return ExactTripsRecursive(start, end, numberOfRoutes, exactTrips, ref counter);
        }

        private int ExactTripsRecursive(string start, string end, int numberOfRoutes, int exactTrips, ref int counter)
        {
            var startTrips = GetAllTripsThatStartWith(start);
            numberOfRoutes++;
            foreach (var trip in startTrips)
            {
                if (numberOfRoutes > exactTrips)
                {
                    continue;
                }
                if (numberOfRoutes == exactTrips && trip.End.Equals(end))
                {
                    counter++;
                }
                ExactTripsRecursive(trip.End, end, numberOfRoutes, exactTrips, ref counter);
            }
            return counter;
        }

        private List<Route> GetAllTripsThatStartWith(string start)
        {
            return _mapRepository.Map().Where(r => r.Start.Equals(start)).ToList();
        }
    }

    
}