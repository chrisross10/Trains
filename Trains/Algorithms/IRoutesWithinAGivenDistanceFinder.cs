namespace Trains
{
    public interface IRoutesWithinAGivenDistanceFinder
    {
        int AllRoutesWithin(IDistanceQuery query);
    }
}