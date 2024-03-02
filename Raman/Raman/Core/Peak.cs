using Raman.Drawing;

namespace Raman.Core;

public class Peak(Point start, Point end, Point top, Chart chart)
{
    public Point Start { get; } = start;
    
    public Point End { get; } = end;

    public Point Top { get; } = top;
    
    public Chart Chart { get; } = chart;

    public override string ToString()
    {
        return $"Start: {Start}, End: {End}, Top: {Top}, Chart: {Chart}";
    }

    protected bool Equals(Peak other)
    {
        return Equals(Start, other.Start) && Equals(End, other.End) && Equals(Top, other.Top);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Peak) obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = (Start != null ? Start.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (End != null ? End.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Top != null ? Top.GetHashCode() : 0);
            return hashCode;
        }
    }

    public static bool operator ==(Peak left, Peak right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Peak left, Peak right)
    {
        return !Equals(left, right);
    }
}