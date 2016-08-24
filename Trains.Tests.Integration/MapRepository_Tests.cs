using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Trains.Tests.Integration
{
	[TestFixture]
	public class MapRepository_Tests
	{
		private string _filePath;
		private int _distance1;
		private int _distance2;
		private string _station1;
		private string _station2;
		private string _station3;
		private string _station4;

		[SetUp]
		public void SetUp()
		{
			_distance1 = GetRandomDistance();
			_distance2 = GetRandomDistance();
			_station1 = GetRandomStation();
			_station2 = GetRandomStation();
			_station3 = GetRandomStation();
			_station4 = GetRandomStation();
			var route1 = string.Format("{0}{1}{2}", _station1, _station2, _distance1);
			var route2 = string.Format("{0}{1}{2}", _station3, _station4, _distance2);
			var data = string.Format("{0}, {1}", route1, route2);
			_filePath = Path.GetTempPath() + "test_data.txt";
			File.WriteAllText(_filePath, data);
		}

		[TearDown]
		public void TearDown()
		{
			File.Delete(_filePath);
		}

		[Test]
		public void It_reads_data()
		{
			var repository = new MapRepository(_filePath);

			var routes = repository.Map();
			Assert.That(routes[0].Start, Is.EqualTo(_station1));
			Assert.That(routes[0].End, Is.EqualTo(_station2));
			Assert.That(routes[0].Distance, Is.EqualTo(Distance.FromMiles(_distance1)));

			Assert.That(routes[1].Start, Is.EqualTo(_station3));
			Assert.That(routes[1].End, Is.EqualTo(_station4));
			Assert.That(routes[1].Distance, Is.EqualTo(Distance.FromMiles(_distance2)));
		}

		private int GetRandomDistance()
		{
			return new Random().Next(1, 9);
		}

		private string GetRandomStation()
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			return new string(Enumerable.Repeat(chars, 1)
										.Select(s => s[new Random().Next(s.Length)]).ToArray());
		}
	}
}
