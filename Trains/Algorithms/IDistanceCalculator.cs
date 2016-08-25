namespace Trains
{
    public interface IDistanceCalculator
    {
        ITravelResult DistanceTravelled(string journey);
    }
}