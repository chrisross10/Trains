using System.Collections.Generic;
using NUnit.Framework;

namespace Trains.Tests.Unit
{
    [TestFixture]
    public class RailNetwork_NumberOfTripsWithMax_Tests
    {
        private RailNetwork _network;

        [SetUp]
        public void SetUp()
        {
            _network = new RailNetwork(new DistanceCalculator(new MapRespository()), new StationTracker(new MapRespository()), new JourneyPlanner(new MapRespository(), new DistanceCalculator(new MapRespository())));
        }

        [TestCase("CC3", ExpectedResult = 2)]
        [TestCase("CC4", ExpectedResult = 4)]
        [TestCase("CC5", ExpectedResult = 6)]
        [TestCase("AB3", ExpectedResult = 3)]
        [TestCase("AB4", ExpectedResult = 5)]
        [TestCase("AB5", ExpectedResult = 8)]
        public int It_calculates_the_number_of_trips(string journey)
        {
            return _network.Trips(journey);
        }
    }
}