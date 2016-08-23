using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trains
{
    public class JourneyPlanner : IJourneyPlanner
    {
        private readonly IMapRepository _mapRepository;

        public JourneyPlanner(IMapRepository mapRepository)
        {
            _mapRepository = mapRepository;
        }

        public TravelResult Shortest(string route)
        {
            var start = route[0].ToString();
            var end = route[1].ToString();
            var allRoutes = new List<KeyValuePair<string, Distance>>();
            var currentRoute = new List<KeyValuePair<string, Distance>>();

            var shortestRouteRecursive = AllRoutes(start, end, ref allRoutes, ref currentRoute);
            if (shortestRouteRecursive.Count == 0)
            {
                return new TravelResult(null);
            }
            var journey = shortestRouteRecursive.ToDictionary(r => r.Key, r => r.Value);
            var distance = journey.OrderBy(d => d.Value.Miles).First().Value;
            return new TravelResult(distance);
        }

        private List<KeyValuePair<string, Distance>> AllRoutes(string start, string end, ref List<KeyValuePair<string, Distance>> allRoutes, ref List<KeyValuePair<string, Distance>> currentRoute)
        {
            var startTrips = GetAllTripsThatStartWith(start);
            foreach (var trip in startTrips)
            {
                if (currentRoute.Contains(trip))
                {
                    continue;
                }
                currentRoute.Add(trip);
                if (trip.Key.EndsWith(end))
                {
                    allRoutes.Add(FlattenRoute(currentRoute));
                    currentRoute.RemoveAt(currentRoute.Count - 1);
                    continue;
                }
                AllRoutes(trip.Key[1].ToString(), end, ref allRoutes, ref currentRoute);
                currentRoute.RemoveAt(currentRoute.Count - 1);
            }
            return allRoutes;
        }

        public string AllRoutesWithin(string journey, int maxDistance)
        {
            var start = journey[0].ToString();
            var end = journey[1].ToString();
            var allRoutes = new List<KeyValuePair<string, Distance>>();
            var currentRoute = new List<KeyValuePair<string, Distance>>();

            var routes = AllRoutesWithinRecursive(start, end, maxDistance, ref allRoutes, ref currentRoute);
            return routes.Count.ToString();
        }

        private List<KeyValuePair<string, Distance>> AllRoutesWithinRecursive(string start, string end, int maxDistance, ref List<KeyValuePair<string, Distance>> allRoutes,
            ref List<KeyValuePair<string, Distance>> currentRoute)
        {
            var startTrips = GetAllTripsThatStartWith(start);
            foreach (var trip in startTrips)
            {
                currentRoute.Add(trip);
                if (currentRoute.Sum(r=>r.Value.Miles) >= maxDistance)
                {
                    currentRoute.RemoveAt((currentRoute.Count - 1));
                    continue;
                }
                if (trip.Key.EndsWith(end))
                {
                    allRoutes.Add(FlattenRoute(currentRoute));
                }
                AllRoutesWithinRecursive(trip.Key[1].ToString(), end, maxDistance, ref allRoutes, ref currentRoute);
                currentRoute.RemoveAt((currentRoute.Count - 1));
            }
            return allRoutes;
        }

        private KeyValuePair<string, Distance> FlattenRoute(List<KeyValuePair<string, Distance>> currentRoute)
        {
            var sb = new StringBuilder();
            var totalDistance = Distance.FromMiles(0);
            for (int i = 0; i < currentRoute.Count; i++)
            {
                totalDistance = totalDistance.Add(currentRoute[i].Value);
                if (i == currentRoute.Count - 1)
                    sb.Append(currentRoute[i].Key);
                else
                    sb.Append(currentRoute[i].Key.First());
            }
            return new KeyValuePair<string, Distance>(sb.ToString(),totalDistance);
        }

        private List<KeyValuePair<string, Distance>> GetAllTripsThatStartWith(string start)
        {
            return _mapRepository.Map().Where(k => k.Key.StartsWith(start)).ToList();
        }
    }
}