using System.Collections.Generic;
using System.Linq;

namespace Trains
{
    public class ShortestRouteFinder : IShortestRouteFinder
    {
        private readonly IMapRepository _repository;

        public ShortestRouteFinder(IMapRepository repository)
        {
            _repository = repository;
        }

        public FlatRoute Shortest(IStationsQuery query)
        {
            var allRoutes = new List<FlatRoute>();
            var currentRoute = new Journey();

            var possibleRoutes = AllRoutes(query.Start, query.End, ref allRoutes, ref currentRoute);
            if (possibleRoutes.Count > 0)
            {
                var shortest = possibleRoutes.OrderBy(r => r.Distance.Miles).First();
                return new FlatRoute(shortest.Route, shortest.Distance);
            }
            var route = string.Format("{0}{1}", query.Start, query.End);
            return new FlatRoute(route);
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

        private IEnumerable<Route> GetAllTripsThatStartWith(string start)
        {
            return _repository.Map().Where(k => k.Start.Equals(start)).ToList();
        }
    }
}