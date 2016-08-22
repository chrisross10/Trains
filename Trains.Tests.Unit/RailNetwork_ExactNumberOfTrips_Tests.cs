using System.Collections.Generic;
using NUnit.Framework;

namespace Trains.Tests.Unit
{
    [TestFixture]
    public class RailNetwork_ExactNumberOfTrips_Tests
    {
        private Dictionary<string, Distance> _map;
        private RailNetwork _network;

        [SetUp]
        public void SetUp()
        {
            _map = new Dictionary<string, Distance>
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
            _network = new RailNetwork(new MapRespository(), new DistanceCalculator(new MapRespository()));
        }

        [TestCase("AC4", ExpectedResult = 3)]
        [TestCase("AB1", ExpectedResult = 1)]
        [TestCase("CC4", ExpectedResult = 2)]
        public int It_calculates_the_exact_number_of_trips(string journey)
        {
            return _network.TripsExact(journey);
        }
    }
}