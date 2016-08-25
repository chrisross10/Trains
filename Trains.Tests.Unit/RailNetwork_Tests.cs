using System;
using NSubstitute;
using NUnit.Framework;

namespace Trains.Tests.Unit
{
	[TestFixture]
	public class RailNetwork_Tests
	{
		private RailNetwork _network;
		private IDistanceCalculator _calculator;
		private string _journey;
		private ITripCounterWithMax _counterWithMax;
		private IJourneyPlanner _planner;
		private ITripCounterWithExact _counterWithExact;

		[SetUp]
		public void SetUp()
		{
			_calculator = Substitute.For<IDistanceCalculator>();
			_counterWithMax = Substitute.For<ITripCounterWithMax>();
			_counterWithExact = Substitute.For<ITripCounterWithExact>();
			_planner = Substitute.For<IJourneyPlanner>();
			_journey = Guid.NewGuid().ToString();
			_network = new RailNetwork(_calculator, _counterWithMax,_counterWithExact, _planner);
		}

		[Test]
		public void It_gets_the_distance_travelled()
		{
			var distance = GetRandomDistance();
			_calculator.DistanceTravelled(_journey).Returns(new TravelResult(distance));
			var travelResult = _network.Travel(_journey);
			Assert.That(travelResult.Result, Is.EqualTo(distance.Miles.ToString()));
		}

		[Test]
		public void It_gets_number_of_trips_with_a_maximum_distance()
		{
			var trips = GetRandomTrips();
			_counterWithMax.Trips(Arg.Any<ITripsQuery>()).Returns(trips);
			var actualTrips = _network.Trips(_journey);
			Assert.That(actualTrips, Is.EqualTo(trips));
		}

		[Test]
		public void It_gets_number_of_trips_with_an_exact_distance()
		{
			var trips = GetRandomTrips();
			_counterWithExact.TripsExact(Arg.Any<ITripsQuery>()).Returns(trips);
			var actualTrips = _network.TripsExact(_journey);
			Assert.That(actualTrips, Is.EqualTo(trips));
		}

		[Test]
		public void It_gets_the_shortest_distance()
		{
			var distance = GetRandomDistance();
			_planner.Shortest(Arg.Any<IStationsQuery>()).Returns(new TravelResult(distance));
			var result = _network.Shortest(_journey);
			Assert.That(result.Result, Is.EqualTo(distance.Miles.ToString()));
		}

		[Test]
		public void It_gets_the_number_of_different_routes_for_a_given_distance()
		{
			var trips = GetRandomTrips();
			_planner.AllRoutesWithin(Arg.Any<IDistanceQuery>()).Returns(trips);
			var actualNumber = _network.AllRoutesWithin(_journey);
			Assert.That(actualNumber, Is.EqualTo(trips));
		}

		private int GetRandomTrips()
		{
			return new Random().Next(1, 100);
		}

		private Distance GetRandomDistance()
		{
			return Distance.FromMiles(new Random().Next(1, 100));
		}
	}
}