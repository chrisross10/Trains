namespace Trains
{
    public class TravelResult
    {
        private readonly Distance _distance;

        public TravelResult(Distance distance)
        {
            _distance = distance;
        }

        public string Result
        {
            get { return _distance != null ? _distance.Miles.ToString() : "NO SUCH ROUTE"; }
        }
    }
}