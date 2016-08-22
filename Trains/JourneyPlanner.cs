using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trains
{
    public class JourneyPlanner
    {
        private readonly IMapRepository _mapRepository;
        private readonly IRouteDistance _routeDistance;

        public JourneyPlanner(IMapRepository mapRepository, IRouteDistance routeDistance)
        {
            _mapRepository = mapRepository;
            _routeDistance = routeDistance;
        }

        public TravelResult ShortestRoute(string route)
        {
            var start = route[0].ToString();
            var end = route[1].ToString();
            var allRoutes = new List<string>();
            var currentRoute = new List<string>();

            var shortestRouteRecursive = ShortestRouteRecursive(start, end, ref allRoutes, ref currentRoute);
            if (shortestRouteRecursive.Count == 0)
            {
                return new TravelResult(null);
            }
            var journey = shortestRouteRecursive.ToDictionary(r => r, r => _routeDistance.Travel(r).Distance);
            var distance = journey.OrderBy(d => d.Value.Miles).First().Value;
            return new TravelResult(distance);
        }

        private List<string> ShortestRouteRecursive(string start, string end, ref List<string> allRoutes, ref List<string> currentRoute)
        {
            var startTrips = _mapRepository.GetAllTripsThatStartWith(start);
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
                ShortestRouteRecursive(trip[1].ToString(), end, ref allRoutes, ref currentRoute);
                currentRoute.RemoveAt(currentRoute.Count - 1);
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

    }
}