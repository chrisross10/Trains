using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trains
{
    public class JourneyPlanner
    {
        private readonly IMapRepository _mapRepository;

        public JourneyPlanner(IMapRepository mapRepository)
        {
            _mapRepository = mapRepository;
        }

        public Distance ShortestRoute(string route)
        {
            var start = route[0].ToString();
            var end = route[1].ToString();
            var allRoutes = new List<string>();
            var sb = new StringBuilder();
            sb.Append(start);

            var shortestRouteRecursive = ShortestRouteRecursive(start, end, ref allRoutes, ref sb);
            foreach (var routes in shortestRouteRecursive)
            {

            }
            return null;
        }

        private List<string> ShortestRouteRecursive(string start, string end, ref List<string> allRoutes, ref StringBuilder sb)
        {
            var startTrips = _mapRepository.GetAllTripsThatStartWith(start);
            foreach (var trip in startTrips)
            {
                sb.Append(trip[1]);
                if (trip.EndsWith(end))
                {
                    allRoutes.Add(sb.ToString());
                    var firstChar = sb[0];
                    sb.Clear();
                    sb.Append(firstChar);
                    break;
                }
                ShortestRouteRecursive(trip[1].ToString(), end, ref allRoutes, ref sb);
            }
            return allRoutes;
        }

        //public Distance ShortestRoute(string route)
        //{
        //    var start = route[0].ToString();
        //    var end = route[1].ToString();
        //    var distance = Distance.FromMiles(0);

        //    return ShortestRouteRecursive(start, end, ref distance);
        //}

        //public Distance ShortestRouteRecursive(string start, string end, ref Distance distance)
        //{
        //    var startTrips = GetAllTripsThatStartWith(start);
        //    foreach (var trip in startTrips)
        //    {
        //        distance = distance.Add(_map[trip]);
        //        if (distance.Miles > 30)
        //        {
        //            break;
        //        }
        //        if (trip.EndsWith(end))
        //        {
        //            distance.Add(_map[trip]);
        //        }
        //        ShortestRouteRecursive(trip[1].ToString(), end, ref distance);
        //    }
        //    return distance;
        //}

    }
}