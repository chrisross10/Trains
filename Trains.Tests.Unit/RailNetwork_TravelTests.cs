﻿using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Trains.Tests.Unit
{
    [TestFixture]
    public class RailNetwork_TravelTests
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

    //[TestFixture]
    //public class RailNetwork_NumberOfTripsTests
    //{
    //    [Test]
    //    public void It_calculates_the_number_of_trips()
    //    {
    //        var map = new Dictionary<string, Distance>
    //        {
    //            {"AB", Distance.FromMiles(5)},
    //            {"AD", Distance.FromMiles(5)},
    //            {"AE", Distance.FromMiles(7)},
    //            {"BC", Distance.FromMiles(4)},
    //            {"CD", Distance.FromMiles(8)},
    //            {"CE", Distance.FromMiles(2)},
    //            {"DC", Distance.FromMiles(8)},
    //            {"DE", Distance.FromMiles(6)},
    //            {"EB", Distance.FromMiles(3)},
    //        };
    //        var network = new RailNetwork(map);
    //        network.Trips("CC3");
    //    }
    //}
}
