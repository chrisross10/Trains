namespace Trains
{
    public interface IShortestRouteFinder
    {
		ITravelResult Shortest(IStationsQuery query);
    }
}