using System.Collections.Generic;
using System.Linq;

namespace Trains
{
    public class MapRespository : IMapRepository
    {
        //TODO: make this read text file
        public Dictionary<string, Distance> Map()
        {
            return new Dictionary<string, Distance>
            {
                {"AB", Distance.FromMiles(5)},
                {"AD", Distance.FromMiles(5)},
                {"AE", Distance.FromMiles(7)},
                {"BC", Distance.FromMiles(4)},
                {"CD", Distance.FromMiles(8)},
                {"CE", Distance.FromMiles(2)},
                {"DC", Distance.FromMiles(8)},
                {"DE", Distance.FromMiles(6)},
                {"EB", Distance.FromMiles(3)},
            };
        }


        //TODO: could this move into a MapHelper class?
        public List<string> GetAllTripsThatStartWith(string start)
        {
            return Map().Keys.Where(k => k.StartsWith(start)).ToList();
        }
    }
}