using System;
using System.Collections.Generic;
using System.Linq;

namespace Trains
{
	public class StationTracker : IStationTracker
	{
		private readonly IMapRepository _mapRepository;

		public StationTracker(IMapRepository mapRepository)
		{
			_mapRepository = mapRepository;
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
			return _mapRepository.Map().Where(r => r.Start.Equals(start)).ToList();
		}
	}


}