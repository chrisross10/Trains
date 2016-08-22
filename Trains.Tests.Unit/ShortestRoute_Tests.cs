using NUnit.Framework;

namespace Trains.Tests.Unit
{
    [TestFixture]
    public class ShortestRoute_Tests
    {
        private JourneyPlanner _planner;

        [SetUp]
        public void SetUp()
        {
            //TODO: mock the repo
            _planner = new JourneyPlanner(new MapRespository(), new DistanceCalculator(new MapRespository()));
        }

        [TestCase("AC", ExpectedResult = "9")]
        [TestCase("CC", ExpectedResult = "9")]
        [TestCase("BB", ExpectedResult = "9")]
        [TestCase("AD", ExpectedResult = "5")]
        [TestCase("CA", ExpectedResult = "NO SUCH ROUTE")]
        public string It_finds_shortest_route(string route)
        {
            return _planner.Shortest(route).Result;
        }
    }

    [TestFixture]
    public class AllRoutesWithALimitedDistance_Tests
    {
        private JourneyPlanner _planner;

        [SetUp]
        public void SetUp()
        {
            //TODO: mock the repo
            _planner = new JourneyPlanner(new MapRespository(), new DistanceCalculator(new MapRespository()));
        }

        [TestCase("CC", 30, ExpectedResult = "7")]
        public string It_finds_all_possible_routes_within_a_given_distance(string journey, int distance)
        {
            return _planner.AllRoutesWithin(journey, distance);
        }
    }
}