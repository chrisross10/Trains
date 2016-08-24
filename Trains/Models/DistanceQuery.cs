using System.Text.RegularExpressions;

namespace Trains
{
    public interface IDistanceQuery : IStationsQuery
    {
        Distance MaxDistance { get; }
    }

    public class DistanceQuery : IDistanceQuery
    {
        private readonly string _journey;
        private readonly Regex _regex;

        public DistanceQuery(string journey)
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

        public Distance MaxDistance
        {
            get { return Distance.FromMiles(int.Parse(_regex.Replace(_journey, "$3"))); }
        }
    }
}