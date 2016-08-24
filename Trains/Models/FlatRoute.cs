namespace Trains
{
    public class FlatRoute
    {
        private readonly string _route;
        private readonly Distance _distance;

        public FlatRoute(string route, Distance distance)
        {
            _route = route;
            _distance = distance;
        }

        public string Route { get { return _route; } }
        public Distance Distance { get { return _distance; } }
    }
}