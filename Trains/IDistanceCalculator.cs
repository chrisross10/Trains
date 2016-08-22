namespace Trains
{
    public interface IDistanceCalculator
    {
        TravelResult DistanceTravelled(string journey);
    }
}