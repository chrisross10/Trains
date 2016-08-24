namespace Trains
{
    public interface IJourneyPlanner
    {
		TravelResult Shortest(TravelQuery query);
        int AllRoutesWithin(DistanceQuery query);
    }
}