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
            _planner = new JourneyPlanner(new MapRespository());
        }

        [Test, Ignore("WIP")]
        public void It_finds_shortest_route()
        {
            Assert.That(_planner.ShortestRoute("AC").Miles, Is.EqualTo(9));
        }
    }
}