using System.Linq;

namespace Trains
{
    public class DistanceCalculator : IDistanceCalculator
    {
        private readonly IMapRepository _mapRepository;

        public DistanceCalculator(IMapRepository mapRepository)
        {
            _mapRepository = mapRepository;
        }

        public FlatRoute DistanceTravelled(string journey)
        {
            if (string.IsNullOrEmpty(journey))
                return new FlatRoute(journey);

            var map = _mapRepository.Map();
            var totalDistance = Distance.FromMiles(0);
            for (var i = 1; i < journey.Length; i++)
            {
                var route = map.SingleOrDefault(m => m.Start.Equals(journey[i - 1].ToString().ToUpper()) && m.End.Equals(journey[i].ToString().ToUpper()));
                if (route != null)
                {
                    totalDistance = totalDistance.Add(route.Distance);
                }
                else
                {
                    return new FlatRoute(journey);
                }
            }
            return new FlatRoute(journey,totalDistance);
        }
    }
}