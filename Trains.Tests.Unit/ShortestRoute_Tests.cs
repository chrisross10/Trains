using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace Trains.Tests.Unit
{
	[TestFixture]
	public class ShortestRoute_Tests
	{
		private ShortestRouteFinder _planner;

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
			_planner = new ShortestRouteFinder(mapRepository);
		}

        [TestCase("AC", ExpectedResult = "9")]
        [TestCase("CC", ExpectedResult = "9")]
        [TestCase("BB", ExpectedResult = "9")]
        [TestCase("AD", ExpectedResult = "5")]
        [TestCase("CA", ExpectedResult = "NO SUCH ROUTE")]
        [TestCase("AB", ExpectedResult = "5")]
		public string It_finds_shortest_route(string route)
		{
			return _planner.Shortest(new TravelQuery(route)).Result;
		}
	}
}