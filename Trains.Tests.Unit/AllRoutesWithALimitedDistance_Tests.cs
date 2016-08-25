using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace Trains.Tests.Unit
{
	[TestFixture]
	public class AllRoutesWithALimitedDistance_Tests
	{
		private RoutesWithinAGivenDistanceFinder _planner;

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
			_planner = new RoutesWithinAGivenDistanceFinder(mapRepository);
		}

		[TestCase("CC30", ExpectedResult = 7)]
		public int It_finds_all_possible_routes_within_a_given_distance(string journey)
		{
			return _planner.AllRoutesWithin(new DistanceQuery(journey));
		}
	}
}