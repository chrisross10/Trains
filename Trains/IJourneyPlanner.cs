namespace Trains
{
    public interface IJourneyPlanner
    {
        TravelResult Shortest(string route);
        string AllRoutesWithin(string journey, int maxDistance);
    }
}