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
            return _stationTracker.TripsExact(journey);
        }

        public TravelResult Shortest(string route)
        {
            return _journeyPlanner.Shortest(route);
        }

        public string AllRoutesWithin(string journey, int maxDistance)
        {
            return _journeyPlanner.AllRoutesWithin(journey, maxDistance);
        }

    }
}