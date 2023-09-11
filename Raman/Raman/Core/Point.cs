namespace Raman.Core
{
    public class Point
    {
        public decimal X { get; }
        
        public decimal Y { get; }

        public Point(decimal x, decimal y)
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