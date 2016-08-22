using System.Collections.Generic;

namespace Trains
{
    public class RouteDistance : IRouteDistance
    {
        private readonly IMapRepository _mapRepository;

        public RouteDistance(IMapRepository mapRepository)
        {
            _mapRepository = mapRepository;
        }

        public TravelResult Travel(string journey)
        {
            if (string.IsNullOrEmpty(journey))
                return new TravelResult(null);

            var totalDistance = Distance.FromMiles(0);
            var route = SplitRoute(journey);
            foreach (var r in route)
            {
                if (_mapRepository.Map().ContainsKey(r))
                {
                    totalDistance = totalDistance.Add(_mapRepository.Map()[r]);
                }
                else
                {
                    return new TravelResult(null);
                }
            }
            return new TravelResult(totalDistance);
        }

        private IEnumerable<string> SplitRoute(string journey)
        {
            var route = new List<string>();
            for (int i = 1; i < journey.Length; i++)
            {
                route.Add(string.Format("{0}{1}", journey[i - 1], journey[i]));
            }
            return route;
        }
    }
}