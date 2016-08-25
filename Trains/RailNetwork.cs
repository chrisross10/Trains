namespace Trains
{
	public class RailNetwork
	{
		private readonly ITripCounterWithMax _tripCounterWithMax;
		private readonly IJourneyPlanner _journeyPlanner;
		private readonly IDistanceCalculator _distanceCalculator;
		private readonly ITripCounterWithExact _tripCounterWithExact;

		public RailNetwork(IDistanceCalculator distanceCalculator, ITripCounterWithMax tripCounterWithMax, ITripCounterWithExact tripCounterWithExact, IJourneyPlanner journeyPlanner)
		{
			_distanceCalculator = distanceCalculator;
			_tripCounterWithMax = tripCounterWithMax;
			_journeyPlanner = journeyPlanner;
			_tripCounterWithExact = tripCounterWithExact;
		}

		public ITravelResult Travel(string journey)
		{
			return _distanceCalculator.DistanceTravelled(journey);
		}

		public int Trips(string journey)
		{
			return _tripCounterWithMax.Trips(new TripsQuery(journey));
		}

		public int TripsExact(string journey)
		{
			return _tripCounterWithExact.TripsExact(new TripsQuery(journey));
		}

		public ITravelResult Shortest(string journey)
		{
			return _journeyPlanner.Shortest(new TravelQuery(journey));
		}

		public int AllRoutesWithin(string journey)
		{
			return _journeyPlanner.AllRoutesWithin(new DistanceQuery(journey));
		}
	}
}