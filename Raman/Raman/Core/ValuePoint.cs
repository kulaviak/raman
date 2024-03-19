namespace Raman.Core;

/// <summary>
/// Point that represent point in chart coordinates, not in pixel coordinates like System.Drawing.Point.
/// </summary>
public class ValuePoint
{
    public double X { get; }
        
    public double Y { get; }

    public ValuePoint(double x, double y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"({Util.Format(X)}, {Util.Format(Y)})";   
    }

    protected bool Equals(ValuePoint other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ValuePoint) obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (X.GetHashCode() * 397) ^ Y.GetHashCode();
        }
    }

    public static bool operator ==(ValuePoint left, ValuePoint right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ValuePoint left, ValuePoint right)
    {
        return !Equals(left, right);
    }
    
    public ValuePoint DeepClone()
    {
        return new ValuePoint(X, Y);
    }
}