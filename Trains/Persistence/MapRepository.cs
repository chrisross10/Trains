using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Trains
{
    public class MapRepository : IMapRepository
    {
        private List<Route> _map;
        private readonly string _graph;
        private Regex _regex;

        public MapRepository(string filePath)
        {
            _graph = File.ReadAllText(filePath);
            _regex = new Regex(@"^([a-zA-Z])([a-zA-Z])(\d+)$");
        }

        public List<Route> Map()
        {
            return _map = _map ?? _graph.Replace(" ", string.Empty)
                .Split(',')
                .Select(route => new Route(
                    _regex.Replace(route, "$1").ToUpper(),
                    _regex.Replace(route, "$2").ToUpper(),
                    Distance.FromMiles(int.Parse(_regex.Replace(route, "$3").ToUpper())))).ToList();
        }
    }
}