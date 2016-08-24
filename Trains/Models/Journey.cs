using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trains
{
    public class Journey : IEnumerable<Route>
    {
        private readonly List<Route> _journey;

        public Journey()
        {
            _journey = new List<Route>();
        }

        public List<Route> Routes { get { return _journey; } }

        public IEnumerator<Route> GetEnumerator()
        {
            return _journey.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Route route)
        {
            _journey.Add(route);
        }

        public void RemovePrevious()
        {
            _journey.RemoveAt(_journey.Count - 1);
        }

        public KeyValuePair<string, Distance> FlattenRoute()
        {
            var sb = new StringBuilder();
            var totalDistance = Distance.FromMiles(0);
            for (int i = 0; i < this.Routes.Count; i++)
            {
                totalDistance = totalDistance.Add(this.Routes[i].Distance);
                if (i == this.Routes.Count - 1)
                    sb.Append(this.Routes[i].Start + this.Routes[i].End);
                else
                    sb.Append(this.Routes[i].Start);
            }
            return new KeyValuePair<string, Distance>(sb.ToString(), totalDistance);
        }

        public int TotalMiles
        {
            get
            {
                return _journey.Sum(r => r.Distance.Miles);
            }
        }
    }
}