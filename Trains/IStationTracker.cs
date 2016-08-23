namespace Trains
{
    public interface IStationTracker
    {
        int Trips(string journey);
        int TripsExact(string journey);
    }
}