using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trains
{
    public class JourneyPlanner : IJourneyPlanner
    {
        private readonly IMapRepository _mapRepository;
        private readonly IDistanceCalculator _distanceCalculator;

        public JourneyPlanner(IMapRepository mapRepository, IDistanceCalculator distanceCalculator)
        {
            _mapRepository = mapRepository;
            _distanceCalculator = distanceCalculator;
        }

        public TravelResult Shortest(string route)
        {
            var start = route[0].ToString();
            var end = route[1].ToString();
            var allRoutes = new List<string>();
            var currentRoute = new List<string>();

            var shortestRouteRecursive = AllRoutes(start, end, ref allRoutes, ref currentRoute);
            if (shortestRouteRecursive.Count == 0)
            {
                return new TravelResult(null);
            }
            var journey = shortestRouteRecursive.ToDictionary(r => r, r => _distanceCalculator.DistanceTravelled(r).Distance);
            var distance = journey.OrderBy(d => d.Value.Miles).First().Value;
            return new TravelResult(distance);
        }

        private List<string> AllRoutes(string start, string end, ref List<string> allRoutes, ref List<string> currentRoute)
        {
            var startTrips = GetAllTripsThatStartWith(start);
            foreach (var trip in startTrips)
            {
                if (currentRoute.Contains(trip))
                {
                    continue;
                }
                currentRoute.Add(trip);
                if (trip.EndsWith(end))
                {
                    allRoutes.Add(FlattenRoute(currentRoute));
                    currentRoute.RemoveAt(currentRoute.Count - 1);
                    continue;
                }
                AllRoutes(trip[1].ToString(), end, ref allRoutes, ref currentRoute);
                currentRoute.RemoveAt(currentRoute.Count - 1);
            }
            return allRoutes;
        }

        public string AllRoutesWithin(string journey, int maxDistance)
        {
            var start = journey[0].ToString();
            var end = journey[1].ToString();
            var allRoutes = new List<string>();
            var currentRoute = new List<string>();

            var routes = AllRoutesWithinRecursive(start, end, maxDistance, ref allRoutes, ref currentRoute);
            return routes.Count.ToString();
        }

        private List<string> AllRoutesWithinRecursive(string start, string end, int maxDistance, ref List<string> allRoutes, ref List<string> currentRoute)
        {
            var startTrips = GetAllTripsThatStartWith(start);
            foreach (var trip in startTrips)
            {
                currentRoute.Add(trip);
                if (_distanceCalculator.DistanceTravelled(FlattenRoute(currentRoute)).Distance.Miles >= maxDistance)
                {
                    currentRoute.RemoveAt((currentRoute.Count - 1));
                    continue;
                }
                if (trip.EndsWith(end))
                {
                    allRoutes.Add(FlattenRoute(currentRoute));
                }
                AllRoutesWithinRecursive(trip[1].ToString(), end, maxDistance, ref allRoutes, ref currentRoute);
                currentRoute.RemoveAt((currentRoute.Count - 1));
            }
            return allRoutes;
        }

        private string FlattenRoute(List<string> currentRoute)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < currentRoute.Count; i++)
            {
                if (i == currentRoute.Count - 1)
                    sb.Append(currentRoute[i]);
                else
                    sb.Append(currentRoute[i].First());
            }
            return sb.ToString();
        }

        private List<string> GetAllTripsThatStartWith(string start)
        {
            return _mapRepository.Map().Keys.Where(k => k.StartsWith(start)).ToList();
        }
    }
}