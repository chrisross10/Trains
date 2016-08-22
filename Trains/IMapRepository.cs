using System.Collections.Generic;

namespace Trains
{
    public interface IMapRepository
    {
        Dictionary<string, Distance> Map();
        List<string> GetAllTripsThatStartWith(string start);
    }
}