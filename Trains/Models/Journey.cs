using System.Collections;
using System.Collections.Generic;

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
    }
}