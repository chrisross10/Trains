using System.Collections.Generic;
using System.Linq;

namespace Trains
{
    public class RailNetwork
    {
        private readonly Dictionary<string, Distance> _map;

        public RailNetwork(Dictionary<string, Distance> map)
        {
            _map = map;
        }

        public Distance Travel(string journey)
        {
            var totalDistance = Distance.FromMiles(0);
            var route = SplitRoute(journey);
            return route.Aggregate(totalDistance, (current, r) => current.Add(_map[r]));
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