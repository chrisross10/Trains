namespace Trains
{
	public interface ITripCounterWithExact
	{
		int TripsExact(ITripsQuery query);
	}
}