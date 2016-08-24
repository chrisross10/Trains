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

		public TravelResult Shortest(TravelQuery query)
        {
            var allRoutes = new List<KeyValuePair<string, Distance>>();
            var currentRoute = new Journey();

            var shortestRouteRecursive = AllRoutes(query.Start, query.End, ref allRoutes, ref currentRoute);
            if (shortestRouteRecursive.Count == 0)
            {
                return new TravelResult(null);
            }
            var journey = shortestRouteRecursive.ToDictionary(r => r.Key, r => r.Value);
            var distance = journey.OrderBy(d => d.Value.Miles).First().Value;
            return new TravelResult(distance);
        }

        private List<KeyValuePair<string, Distance>> AllRoutes(string start, string end, ref List<KeyValuePair<string, Distance>> allRoutes, ref Journey currentRoute)
        {
            var startTrips = GetAllTripsThatStartWith(start);
            foreach (var trip in startTrips)
            {
                if (currentRoute.Contains(trip))
                {
                    continue;
                }
                currentRoute.Add(trip);
                if (trip.End.Equals(end))
                {
                    allRoutes.Add(FlattenRoute(currentRoute));
                    currentRoute.RemovePrevious();
                    continue;
                }
                AllRoutes(trip.End, end, ref allRoutes, ref currentRoute);
                currentRoute.RemovePrevious();
            }
            return allRoutes;
        }

        public string AllRoutesWithin(DistanceQuery query)
        {
            var allRoutes = new List<KeyValuePair<string, Distance>>();
            var currentRoute = new Journey();
			var routes = AllRoutesWithinRecursive(query.Start, query.End, query.MaxDistance.Miles, ref allRoutes, ref currentRoute);
            return routes.Count.ToString();
        }

        private List<KeyValuePair<string, Distance>> AllRoutesWithinRecursive(string start, string end, int maxDistance, ref List<KeyValuePair<string, Distance>> allRoutes,
            ref Journey currentRoute)
        {
            var startTrips = GetAllTripsThatStartWith(start);
            foreach (var trip in startTrips)
            {
                currentRoute.Add(trip);
                if (currentRoute.Sum(r => r.Distance.Miles) >= maxDistance)
                {
                    currentRoute.RemovePrevious();
                    continue;
                }
                if (trip.End.Equals(end))
                {
                    allRoutes.Add(FlattenRoute(currentRoute));
                }
                AllRoutesWithinRecursive(trip.End, end, maxDistance, ref allRoutes, ref currentRoute);
                currentRoute.RemovePrevious();
            }
            return allRoutes;
        }

        private KeyValuePair<string, Distance> FlattenRoute(Journey currentRoute)
        {
            var sb = new StringBuilder();
            var totalDistance = Distance.FromMiles(0);
            for (int i = 0; i < currentRoute.Routes.Count; i++)
            {
                totalDistance = totalDistance.Add(currentRoute.Routes[i].Distance);
                if (i == currentRoute.Routes.Count - 1)
                    sb.Append(currentRoute.Routes[i].Start + currentRoute.Routes[i].End);
                else
                    sb.Append(currentRoute.Routes[i].Start);
            }
            return new KeyValuePair<string, Distance>(sb.ToString(), totalDistance);
        }

        private IEnumerable<Route> GetAllTripsThatStartWith(string start)
        {
            return _mapRepository.Map().Where(k => k.Start.Equals(start)).ToList();
        }
    }
}