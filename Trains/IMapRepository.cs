using System.Collections.Generic;

namespace Trains
{
    public interface IMapRepository
    {
        Dictionary<string, Distance> Map();
    }
}