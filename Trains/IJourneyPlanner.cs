namespace Trains
{
    public interface IJourneyPlanner
    {
		TravelResult Shortest(TravelQuery query);
        string AllRoutesWithin(string journey, int maxDistance);
    }
}