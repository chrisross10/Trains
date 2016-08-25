using System.Collections.Generic;
using System.Linq;

namespace Trains
{
    public class RoutesWithinAGivenDistanceFinder : IRoutesWithinAGivenDistanceFinder
    {
        private readonly IMapRepository _repository;

        public RoutesWithinAGivenDistanceFinder(IMapRepository repository)
        {
            _repository = repository;
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
            return _repository.Map().Where(k => k.Start.Equals(start)).ToList();
        }
    }
}