namespace Trains
{
    public class Distance
    {
        private readonly int _miles;

        private Distance(int miles)
        {
            _miles = miles;
        }

        public int Miles { get { return _miles; } }

        public static Distance FromMiles(int miles)
        {
            return new Distance(miles);
        }

        public Distance Add(Distance other)
        {
            return new Distance(_miles + other._miles);
        }

        public override bool Equals(object obj)
        {
            var d = (Distance)obj;
            return _miles == d._miles;
        }

        public override int GetHashCode()
        {
            return (int)_miles.GetHashCode();
        }

        public override string ToString()
        {
            return _miles > 0 ? _miles.ToString() : "NO SUCH ROUTE";
        }
    }
}