namespace Raman.Core;

public class ValuePoint
{
    public decimal X { get; }
        
    public decimal Y { get; }

    public ValuePoint(decimal x, decimal y)
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
}