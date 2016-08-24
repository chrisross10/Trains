using System.Text.RegularExpressions;

namespace Trains
{
	public class DistanceQuery : IQuery
	{
		private readonly string _journey;

		public DistanceQuery(string journey)
		{
			_journey = journey;
		}

		public string Start
		{
			get
			{
				var regex = new Regex(@"^([a-zA-Z])([a-zA-Z])$");
				var returnValue = regex.Replace(_journey, "$1");
				return returnValue.ToUpper();
			}
		}

		public string End
		{
			get
			{
				var regex = new Regex(@"^([a-zA-Z])([a-zA-Z])$");
				var returnValue = regex.Replace(_journey, "$2");
				return returnValue.ToUpper();
			}
		}

		public Distance MaxDistance
		{
			get
			{
				var regex = new Regex(@"^([a-zA-Z])([a-zA-Z])(\d+)$");
				var returnValue = regex.Replace(_journey, "$3");
				return Distance.FromMiles(int.Parse(returnValue));
			}
		}
	}

	public class TravelQuery : IQuery
	{
		private readonly string _journey;

		public TravelQuery(string journey)
		{
			_journey = journey;
		}

		public string Start
		{
			get
			{
				var regex = new Regex(@"^([a-zA-Z])([a-zA-Z])$");
				var returnValue = regex.Replace(_journey, "$1");
				return returnValue.ToUpper();
			}
		}

		public string End
		{
			get
			{
				var regex = new Regex(@"^([a-zA-Z])([a-zA-Z])$");
				var returnValue = regex.Replace(_journey, "$2");
				return returnValue.ToUpper();
			}
		}
	}

	public class TripsQuery : IQuery
	{
		private readonly string _journey;

		public TripsQuery(string journey)
		{
			_journey = journey;
		}

		public string Start
		{
			get
			{
				var regex = new Regex(@"^([a-zA-Z])([a-zA-Z])(\d+)$");
				var returnValue = regex.Replace(_journey, "$1");
				return returnValue.ToUpper();
			}
		}

		public string End
		{
			get
			{
				var regex = new Regex(@"^([a-zA-Z])([a-zA-Z])(\d+)$");
				var returnValue = regex.Replace(_journey, "$2");
				return returnValue.ToUpper();
			}
		}

		public int Trips
		{
			get
			{
				var regex = new Regex(@"^([a-zA-Z])([a-zA-Z])(\d+)$");
				var returnValue = regex.Replace(_journey, "$3");
				return int.Parse(returnValue);
			}
		}
	}

	public interface IQuery
	{
		string Start { get; }
		string End { get; }
	}
}