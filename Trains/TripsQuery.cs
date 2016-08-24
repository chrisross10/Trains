namespace Trains
{
    public class TripsQuery
    {
        private readonly string _journey;

        public TripsQuery(string journey)
        {
            _journey = journey;
        }

        public string Start { get { return _journey[0].ToString().ToUpper(); } }
        public string End { get { return _journey[1].ToString().ToUpper(); } }
        public int Trips { get { return int.Parse(_journey[2].ToString()); } }
    }
}