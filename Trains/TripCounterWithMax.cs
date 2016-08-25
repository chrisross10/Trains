using System;
using System.Collections.Generic;
using System.Linq;

namespace Trains
{
	public class TripCounterWithMax : ITripCounterWithMax
	{
		private readonly IMapRepository _repository;

		public TripCounterWithMax(IMapRepository repository)
		{
			_repository = repository;
		}

		public int Trips(ITripsQuery query)
		{
			const int numberOfTrips = 0;
			var counter = 0;
			return TripsRecursive(query.Start, query.End, query.Trips, numberOfTrips, ref counter);
		}

		private int TripsRecursive(string start, string end, int maxTrips, int numberOfRoutes, ref int counter)
		{
			var startTrips = GetAllRoutesThatStartWith(start);
			numberOfRoutes++;
			foreach (var trip in startTrips.Where(trip => numberOfRoutes <= maxTrips))
			{
				if (trip.End.Equals(end))
				{
					counter++;
				}
				TripsRecursive(trip.End, end, maxTrips, numberOfRoutes, ref counter);
			}
			return counter;
		}

		private IEnumerable<Route> GetAllRoutesThatStartWith(string start)
		{
			return _repository.Map().Where(r => r.Start.Equals(start)).ToList();
		}
	}
}