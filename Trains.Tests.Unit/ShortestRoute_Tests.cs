using System.Collections.Generic;
using NSubstitute;
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
            var mapRepository = Substitute.For<IMapRepository>();
            var map = new Dictionary<string, Distance>
            {
                {"AB", Distance.FromMiles(5)},
                {"AD", Distance.FromMiles(5)},
                {"AE", Distance.FromMiles(7)},
                {"BC", Distance.FromMiles(4)},
                {"CD", Distance.FromMiles(8)},
                {"CE", Distance.FromMiles(2)},
                {"DC", Distance.FromMiles(8)},
                {"DE", Distance.FromMiles(6)},
                {"EB", Distance.FromMiles(3)},
            };
            mapRepository.Map().Returns(map);
            var distanceCalculator = Substitute.For<IDistanceCalculator>();
            _planner = new JourneyPlanner(mapRepository, distanceCalculator);
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
            var mapRepository = Substitute.For<IMapRepository>();
            var map = new Dictionary<string, Distance>
            {
                {"AB", Distance.FromMiles(5)},
                {"AD", Distance.FromMiles(5)},
                {"AE", Distance.FromMiles(7)},
                {"BC", Distance.FromMiles(4)},
                {"CD", Distance.FromMiles(8)},
                {"CE", Distance.FromMiles(2)},
                {"DC", Distance.FromMiles(8)},
                {"DE", Distance.FromMiles(6)},
                {"EB", Distance.FromMiles(3)},
            };
            mapRepository.Map().Returns(map);
            var distanceCalculator = Substitute.For<IDistanceCalculator>();
            _planner = new JourneyPlanner(mapRepository, distanceCalculator);
        }

        [TestCase("CC", 30, ExpectedResult = "7")]
        public string It_finds_all_possible_routes_within_a_given_distance(string journey, int distance)
        {
            return _planner.AllRoutesWithin(journey, distance);
        }
    }
}