namespace Raman.Core
{
    public class DataPoint
    {
        public decimal X { get; }
        
        public decimal Y { get; }

        public DataPoint(decimal x, decimal y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}";   
        }
    }
}