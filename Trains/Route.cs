namespace Trains
{
    public class Route
    {
        private readonly string _start;
        private readonly string _end;
        private readonly Distance _distance;

        public Route(string start, string end, Distance distance)
        {
            _start = start;
            _end = end;
            _distance = distance;
        }

        public string Start { get { return _start; } }
        public string End { get { return _end; } }
        public Distance Distance { get { return _distance; } }
    }
}