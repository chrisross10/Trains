using System;
using System.Collections.Generic;
using System.Linq;

namespace Trains
{
    public class MapRespository : IMapRepository
    {
        private readonly string _graph;
        private List<Route> _map;

        public MapRespository(string graph)
        {
            _graph = graph;
        }

        public List<Route> Map()
        {
            return _map ?? _graph.Replace(" ", string.Empty).Split(',').Select(route => new Route(route[0].ToString(),
                route[1].ToString(),
                Distance.FromMiles(int.Parse(route[2].ToString())))).ToList();
        }
    }
}