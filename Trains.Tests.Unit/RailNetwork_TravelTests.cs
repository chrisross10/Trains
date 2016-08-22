using NUnit.Framework;

namespace Trains.Tests.Unit
{
    [TestFixture]
    public class RailNetwork_TravelTests
    {
        private RailNetwork _network;

        [SetUp]
        public void SetUp()
        {
            _network = new RailNetwork(new MapRespository(), new RouteDistance(new MapRespository()));
        }

        [TestCase("ABC", ExpectedResult = "9")]
        [TestCase("AD", ExpectedResult = "5")]
        [TestCase("ADC", ExpectedResult = "13")]
        [TestCase("AEBCD", ExpectedResult = "22")]
        [TestCase("AED", ExpectedResult = "NO SUCH ROUTE")]
        [TestCase("", ExpectedResult = "NO SUCH ROUTE")]
        public string It_calculates_the_distance_of_the_journey(string journey)
        {
            return _network.Travel(journey).Result;
        }
    }
}
