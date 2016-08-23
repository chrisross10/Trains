using System.Collections.Generic;
using System.Linq;

namespace Trains
{
    public class RailNetwork
    {
        private readonly IMapRepository _mapRepository;
        private readonly IDistanceCalculator _distanceCalculator;
        private readonly IJourneyPlanner _journeyPlanner;

        //TODO: eventually, the mapRepo will move out of this class
        public RailNetwork(IMapRepository mapRepository, IDistanceCalculator distanceCalculator, IJourneyPlanner journeyPlanner)
        {
            _mapRepository = mapRepository;
            _distanceCalculator = distanceCalculator;
            _journeyPlanner = journeyPlanner;
        }

        public TravelResult Travel(string journey)
        {
            return _distanceCalculator.DistanceTravelled(journey);
        }

        //public int Trips(string journey)
        //{
        //    var allRoutes = new List<string>();
        //    var currentRoute = new List<string>();
        //    var routes = _journeyPlanner.AllRoutes(journey[0].ToString(), journey[1].ToString(), ref allRoutes, ref currentRoute);
        //    return routes.Count(r => (r.Length-1) <= 3);
        //}


        public int Trips(string journey)
        {
            string start = journey[0].ToString();
            string end = journey[1].ToString();
            int maxTrips = int.Parse(journey[2].ToString());
            int numberOfTrips = 0;
            int counter = 0;

            return TripsRecursive(start, end, numberOfTrips, maxTrips, ref counter);
        }

        public int TripsRecursive(string start, string end, int numberOfTrips, int maxTrips, ref int counter)
        {
            var startTrips = _mapRepository.GetAllTripsThatStartWith(start);
            numberOfTrips++;
            foreach (var trip in startTrips)
            {
                if (numberOfTrips > maxTrips)
                {
                    continue;
                }
                if (trip.EndsWith(end))
                {
                    counter++;
                }
                TripsRecursive(trip[1].ToString(), end, numberOfTrips, maxTrips, ref counter);
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

        public int ExactTripsRecursive(string start, string end, int numberOfRoutes, int exactTrips, ref int counter)
        {
            var startTrips = _mapRepository.GetAllTripsThatStartWith(start);
            numberOfRoutes++;
            foreach (var trip in startTrips)
            {
                if (numberOfRoutes > exactTrips)
                {
                    continue;
                }
                if (numberOfRoutes == exactTrips && trip.EndsWith(end))
                {
                    counter++;
                }
                ExactTripsRecursive(trip[1].ToString(), end, numberOfRoutes, exactTrips, ref counter);
            }
            return counter;
        }


    }
}