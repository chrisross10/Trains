namespace Trains
{
    public interface IStationTracker
    {
        int Trips(TripsQuery query);
        int TripsExact(TripsQuery query);
    }
}