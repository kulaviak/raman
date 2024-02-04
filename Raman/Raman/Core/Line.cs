namespace Raman.Core;

public class Line(Point start, Point end)
{
    public Point Start { get; } = start;
    
    public Point End { get; } = end;

    public override string ToString()
    {
        return $"Start: {Start}, End: {End}";
    }

    protected bool Equals(Line other)
    {
        return Equals(Start, other.Start) && Equals(End, other.End);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Line) obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return ((Start != null ? Start.GetHashCode() : 0) * 397) ^ (End != null ? End.GetHashCode() : 0);
        }
    }

    public static bool operator ==(Line left, Line right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Line left, Line right)
    {
        return !Equals(left, right);
    }
}