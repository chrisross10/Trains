using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace Trains.Tests.Unit
{
    [TestFixture]
    public class StationTracker_ExactNumberOfTrips_Tests
    {
		private TripCounterWithExact _counter;

        [SetUp]
        public void SetUp()
        {
            var repository = Substitute.For<IMapRepository>();
            var map = new List<Route>
            {
                new Route("A","B",Distance.FromMiles(5)),
                new Route("A","D",Distance.FromMiles(5)),
                new Route("A","E",Distance.FromMiles(7)),
                new Route("B","C",Distance.FromMiles(4)),
                new Route("C","D",Distance.FromMiles(8)),
                new Route("C","E",Distance.FromMiles(2)),
                new Route("D","C",Distance.FromMiles(8)),
                new Route("D","E",Distance.FromMiles(6)),
                new Route("E","B",Distance.FromMiles(3)),
            };
            repository.Map().Returns(map);
            _counter = new TripCounterWithExact(repository);
        }

        [TestCase("AC4", ExpectedResult = 3)]
        [TestCase("AB1", ExpectedResult = 1)]
        [TestCase("CC4", ExpectedResult = 2)]
        public int It_calculates_the_exact_number_of_trips(string journey)
        {
            return _counter.TripsExact(new TripsQuery(journey));
        }
    }
}