namespace Trains
{
    public class NullTravelResult : ITravelResult
    {
        public string Result { get { return "NO SUCH ROUTE"; } }
    }
}