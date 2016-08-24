using System.Text.RegularExpressions;

namespace Trains
{
    public interface ITripsQuery : IStationsQuery
    {
        int Trips { get; }
    }

    public class TripsQuery : ITripsQuery
    {
        private readonly string _journey;
        private readonly Regex _regex;

        public TripsQuery(string journey)
        {
            _journey = journey;
            _regex = new Regex(@"^([a-zA-Z])([a-zA-Z])(\d+)$");
        }

        public string Start
        {
            get { return _regex.Replace(_journey, "$1").ToUpper(); }
        }

        public string End
        {
            get { return _regex.Replace(_journey, "$2").ToUpper(); }
        }

        public int Trips
        {
            get { return int.Parse(_regex.Replace(_journey, "$3")); }
        }
    }
}