namespace Trains
{
    public interface IShortestRouteFinder
    {
        FlatRoute Shortest(IStationsQuery query);
    }
}