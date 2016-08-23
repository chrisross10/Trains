namespace Trains
{
    public class StationTracker : IStationTracker
    {
        private readonly IMapRepository _mapRepository;

        public StationTracker(IMapRepository mapRepository)
        {
            _mapRepository = mapRepository;
        }

        public int Trips(string journey)
        {
            string start = journey[0].ToString();
            string end = journey[1].ToString();
            int maxTrips = int.Parse(journey[2].ToString());
            int numberOfTrips = 0;
            int counter = 0;

            return TripsRecursive(start, end, numberOfTrips, maxTrips, ref counter);
        }

        private int TripsRecursive(string start, string end, int numberOfTrips, int maxTrips, ref int counter)
        {
            var startTrips = _mapRepository.GetAllTripsThatStartWith(start);
            numberOfTrips++;
            foreach (var trip in startTrips)
            {
                if (numberOfTrips > maxTrips)
                {
                    continue;
                }
                if (trip.EndsWith(end))
                {
                    counter++;
                }
                TripsRecursive(trip[1].ToString(), end, numberOfTrips, maxTrips, ref counter);
            }
            return counter;
        }

        public int TripsExact(string journey)
        {
            string start = journey[0].ToString();
            string end = journey[1].ToString();
            int exactTrips = int.Parse(journey[2].ToString());
            int numberOfRoutes = 0;
            int counter = 0;

            return ExactTripsRecursive(start, end, numberOfRoutes, exactTrips, ref counter);
        }

        private int ExactTripsRecursive(string start, string end, int numberOfRoutes, int exactTrips, ref int counter)
        {
            var startTrips = _mapRepository.GetAllTripsThatStartWith(start);
            numberOfRoutes++;
            foreach (var trip in startTrips)
            {
                if (numberOfRoutes > exactTrips)
                {
                    continue;
                }
                if (numberOfRoutes == exactTrips && trip.EndsWith(end))
                {
                    counter++;
                }
                ExactTripsRecursive(trip[1].ToString(), end, numberOfRoutes, exactTrips, ref counter);
            }
            return counter;
        }
    }
}