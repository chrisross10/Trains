namespace Trains
{
	public class RailNetwork
	{
		private readonly IStationTracker _stationTracker;
		private readonly IJourneyPlanner _journeyPlanner;
		private readonly IDistanceCalculator _distanceCalculator;

		public RailNetwork(IDistanceCalculator distanceCalculator, IStationTracker stationTracker, IJourneyPlanner journeyPlanner)
		{
			_distanceCalculator = distanceCalculator;
			_stationTracker = stationTracker;
			_journeyPlanner = journeyPlanner;
		}

		public TravelResult Travel(string journey)
		{
			return _distanceCalculator.DistanceTravelled(journey);
		}

		public int Trips(string jouney)
		{
			return _stationTracker.Trips(new TripsQuery(jouney));
		}

		public int TripsExact(string journey)
		{
			return _stationTracker.TripsExact(new TripsQuery(journey));
		}

		public TravelResult Shortest(string journey)
		{
			return _journeyPlanner.Shortest(new TravelQuery(journey));
		}

		public string AllRoutesWithin(string journey)
		{
			return _journeyPlanner.AllRoutesWithin(new DistanceQuery(journey));
		}

	}
}