namespace Raman.Core;

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

    protected bool Equals(Point other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Point) obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (X.GetHashCode() * 397) ^ Y.GetHashCode();
        }
    }

    public static bool operator ==(Point left, Point right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Point left, Point right)
    {
        return !Equals(left, right);
    }
}