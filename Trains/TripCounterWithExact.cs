using System.Collections.Generic;
using System.Linq;

namespace Trains
{
	public class TripCounterWithExact : ITripCounterWithExact
	{
		private readonly IMapRepository _repository;

		public TripCounterWithExact(IMapRepository repository)
		{
			_repository = repository;
		}

		public int TripsExact(ITripsQuery query)
		{
			const int numberOfRoutes = 0;
			var counter = 0;
			return ExactTripsRecursive(query.Start, query.End, query.Trips, numberOfRoutes, ref counter);
		}

		private int ExactTripsRecursive(string start, string end, int trips, int numberOfRoutes, ref int counter)
		{
			var startTrips = GetAllRoutesThatStartWith(start);
			numberOfRoutes++;
			foreach (var trip in startTrips.Where(trip => numberOfRoutes <= trips))
			{
				if (trip.End.Equals(end) && numberOfRoutes == trips)
				{
					counter++;
				}
				ExactTripsRecursive(trip.End, end, trips, numberOfRoutes, ref counter);
			}
			return counter;
		}

		private IEnumerable<Route> GetAllRoutesThatStartWith(string start)
		{
			return _repository.Map().Where(r => r.Start.Equals(start)).ToList();
		}
	}
}