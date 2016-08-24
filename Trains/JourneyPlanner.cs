using System.Collections.Generic;
using System.Linq;

namespace Trains
{
    public class JourneyPlanner : IJourneyPlanner
    {
        private readonly IMapRepository _mapRepository;

        public JourneyPlanner(IMapRepository mapRepository)
        {
            _mapRepository = mapRepository;
        }

        public ITravelResult Shortest(IStationsQuery query)
        {
            var allRoutes = new List<FlatRoute>();
            var currentRoute = new Journey();

            var possibleRoutes = AllRoutes(query.Start, query.End, ref allRoutes, ref currentRoute);
            if (possibleRoutes.Count > 0)
            {
                var shortestDistance = possibleRoutes.OrderBy(r => r.Distance.Miles).First().Distance;
                return new TravelResult(shortestDistance);
            }
            return new NullTravelResult();
        }

        private List<FlatRoute> AllRoutes(string start, string end, ref List<FlatRoute> allRoutes, ref Journey journey)
        {
            var startTrips = GetAllTripsThatStartWith(start);
            foreach (var trip in startTrips)
            {
                if (journey.Contains(trip))
                {
                    continue;
                }
                journey.Add(trip);
                if (trip.End.Equals(end))
                {
                    allRoutes.Add(journey.FlattenRoute());
                    journey.RemovePrevious();
                    continue;
                }
                AllRoutes(trip.End, end, ref allRoutes, ref journey);
                journey.RemovePrevious();
            }
            return allRoutes;
        }

        public int AllRoutesWithin(IDistanceQuery query)
        {
            var allRoutes = new List<FlatRoute>();
            var currentRoute = new Journey();
            return AllRoutesWithinRecursive(query.Start, query.End, query.MaxDistance.Miles, ref allRoutes, ref currentRoute).Count;
        }

        private List<FlatRoute> AllRoutesWithinRecursive(string start, string end, int maxDistance, ref List<FlatRoute> allRoutes,
            ref Journey currentRoute)
        {
            var startTrips = GetAllTripsThatStartWith(start);
            foreach (var trip in startTrips)
            {
                currentRoute.Add(trip);
                if (currentRoute.TotalMiles >= maxDistance)
                {
                    currentRoute.RemovePrevious();
                    continue;
                }
                if (trip.End.Equals(end))
                {
                    allRoutes.Add(currentRoute.FlattenRoute());
                }
                AllRoutesWithinRecursive(trip.End, end, maxDistance, ref allRoutes, ref currentRoute);
                currentRoute.RemovePrevious();
            }
            return allRoutes;
        }

        private IEnumerable<Route> GetAllTripsThatStartWith(string start)
        {
            return _mapRepository.Map().Where(k => k.Start.Equals(start)).ToList();
        }
    }
}