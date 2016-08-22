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
            _planner = new JourneyPlanner(new MapRespository(),new RouteDistance(new MapRespository()));
        }

        [TestCase("AC", ExpectedResult = "9")]
        [TestCase("CC", ExpectedResult = "9")]
        [TestCase("BB", ExpectedResult = "9")]
        [TestCase("AD", ExpectedResult = "5")]
        [TestCase("CA", ExpectedResult = "NO SUCH ROUTE")]
        public string It_finds_shortest_route(string route)
        {
            return _planner.ShortestRoute(route).Result;
        }
    }
}