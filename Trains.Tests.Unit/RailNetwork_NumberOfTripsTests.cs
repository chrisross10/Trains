using System.Collections.Generic;
using NUnit.Framework;

namespace Trains.Tests.Unit
{
    [TestFixture]
    public class RailNetwork_NumberOfTripsWithMax_Tests
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
            _network = new RailNetwork(_map);
        }

        [TestCase("CC3", ExpectedResult = 2)]
        [TestCase("CC4", ExpectedResult = 4)]
        [TestCase("CC5", ExpectedResult = 6)]
        [TestCase("AB3", ExpectedResult = 3)]
        //[TestCase("AB4", ExpectedResult = 6)]
        [TestCase("AB5", ExpectedResult = 8)]
        public int It_calculates_the_number_of_trips(string journey)
        {
            return _network.Trips(journey);
        }
    }

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
            _network = new RailNetwork(_map);
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