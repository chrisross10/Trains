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
            _calc = new DistanceCalculator(new MapRespository());
        }

        [TestCase("ABC", ExpectedResult = "9")]
        [TestCase("AD", ExpectedResult = "5")]
        [TestCase("ADC", ExpectedResult = "13")]
        [TestCase("AEBCD", ExpectedResult = "22")]
        [TestCase("AED", ExpectedResult = "NO SUCH ROUTE")]
        [TestCase("", ExpectedResult = "NO SUCH ROUTE")]
        //[TestCase("*", ExpectedResult = "NO SUCH ROUTE")]
        public string It_calculates_the_distance_of_the_journey(string journey)
        {
            return _calc.DistanceTravelled(journey).Result;
        }
    }
}
