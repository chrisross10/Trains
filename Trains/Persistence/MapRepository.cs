using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Trains
{
    public class MapRepository : IMapRepository
    {
        private List<Route> _map;
        private readonly string _graph;

        public MapRepository(string filePath)
        {
            _graph = File.ReadAllText(filePath);
        }

        public List<Route> Map()
        {
            return _map = _map ?? _graph.Replace(" ", string.Empty).Split(',').Select(route => new Route(route[0].ToString(),
                route[1].ToString(),
                Distance.FromMiles(int.Parse(route[2].ToString())))).ToList();
        }
    }
}