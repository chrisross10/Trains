using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace Trains.Tests.Unit
{
    [TestFixture]
    public class DistanceCalculator_Tests
    {
        private DistanceCalculator _calc;

        [SetUp]
        public void SetUp()
        {
            var mapRepository = Substitute.For<IMapRepository>();
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
            mapRepository.Map().Returns(map);
            _calc = new DistanceCalculator(mapRepository);
        }

        [TestCase("ABC", ExpectedResult = "9")]
        [TestCase("AD", ExpectedResult = "5")]
        [TestCase("ADC", ExpectedResult = "13")]
        [TestCase("AEBCD", ExpectedResult = "22")]
        [TestCase("AED", ExpectedResult = "NO SUCH ROUTE")]
        [TestCase("", ExpectedResult = "NO SUCH ROUTE")]
        [TestCase("*", ExpectedResult = "NO SUCH ROUTE")]
        public string It_calculates_the_distance_of_the_journey(string journey)
        {
            return _calc.DistanceTravelled(journey).Result;
        }
    }
}
