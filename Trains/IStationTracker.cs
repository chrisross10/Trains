namespace Trains
{
    public interface IStationTracker
    {
        int Trips(ITripsQuery query);
        int TripsExact(ITripsQuery query);
    }
}