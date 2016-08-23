﻿using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace Trains.Tests.Unit
{
    [TestFixture]
    public class RailNetwork_NumberOfTripsWithMax_Tests
    {
        private StationTracker _tracker;

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
            _tracker = new StationTracker(mapRepository);
        }

        [TestCase("CC3", ExpectedResult = 2)]
        [TestCase("CC4", ExpectedResult = 4)]
        [TestCase("CC5", ExpectedResult = 6)]
        [TestCase("AB3", ExpectedResult = 3)]
        [TestCase("AB4", ExpectedResult = 5)]
        [TestCase("AB5", ExpectedResult = 8)]
        public int It_calculates_the_number_of_trips(string journey)
        {
            return _tracker.Trips(journey);
        }
    }
}