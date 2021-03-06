using System.Text.RegularExpressions;

namespace Trains
{
    public interface IStationsQuery
    {
        string Start { get; }
        string End { get; }
    }

    public class TravelQuery : IStationsQuery
    {
        private readonly string _journey;
        private readonly Regex _regex;

        public TravelQuery(string journey)
        {
            _journey = journey;
            _regex = new Regex(@"^([a-zA-Z])([a-zA-Z])$");
        }

        public string Start
        {
            get { return _regex.Replace(_journey, "$1").ToUpper(); }
        }

        public string End
        {
            get { return _regex.Replace(_journey, "$2").ToUpper(); }
        }
    }
}