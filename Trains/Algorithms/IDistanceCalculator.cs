namespace Trains
{
    public interface IDistanceCalculator
    {
        FlatRoute DistanceTravelled(string journey);
    }
}