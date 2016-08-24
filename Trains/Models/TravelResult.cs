namespace Trains
{
    public interface ITravelResult
    {
        string Result { get; }
    }

    public class TravelResult : ITravelResult
    {
        private readonly Distance _distance;

        public TravelResult(Distance distance)
        {
            _distance = distance;
        }

        public string Result
        {
            get { return _distance.Miles.ToString(); }
        }
    }

    public class NullTravelResult : ITravelResult
    {
        public string Result { get { return "NO SUCH ROUTE"; } }
    }
}