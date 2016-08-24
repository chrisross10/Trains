namespace Trains
{
    public interface IJourneyPlanner
    {
		ITravelResult Shortest(IStationsQuery query);
        int AllRoutesWithin(IDistanceQuery query);
    }
}