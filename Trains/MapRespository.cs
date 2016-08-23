using System.Collections.Generic;
using System.Linq;

namespace Trains
{
    public class MapRespository : IMapRepository
    {
        private readonly string _filePath;

        public MapRespository(string filePath)
        {
            _filePath = filePath;
        }

        //public Dictionary<string, Distance> Map()
        //{
        //    return new Dictionary<string, Distance>
        //    {
        //        {"AB", Distance.FromMiles(5)},
        //        {"AD", Distance.FromMiles(5)},
        //        {"AE", Distance.FromMiles(7)},
        //        {"BC", Distance.FromMiles(4)},
        //        {"CD", Distance.FromMiles(8)},
        //        {"CE", Distance.FromMiles(2)},
        //        {"DC", Distance.FromMiles(8)},
        //        {"DE", Distance.FromMiles(6)},
        //        {"EB", Distance.FromMiles(3)},
        //    };
        //}

        public List<Route> Map()
        {
            return new List<Route>
            {
                new Route("A","B",Distance.FromMiles(5)),
                new Route("A","D",Distance.FromMiles(5)),
                new Route("A","E",Distance.FromMiles(7)),
                new Route("B","C",Distance.FromMiles(4)),
                new Route("C","D",Distance.FromMiles(8)),
                new Route("C","E",Distance.FromMiles(2)),
                new Route("D","C",Distance.FromMiles(8)),
                new Route("D","E",Distance.FromMiles(6)),
                new Route("E","B",Distance.FromMiles(3)),
            };
        }
    }
}