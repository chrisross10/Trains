namespace Trains
{
    public interface IJourneyPlanner
    {
		TravelResult Shortest(IStationsQuery query);
        int AllRoutesWithin(IDistanceQuery query);
    }
}