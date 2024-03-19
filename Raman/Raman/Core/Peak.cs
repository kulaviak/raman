namespace Raman.Core;

public class Peak
{
    public Peak(ValuePoint start, ValuePoint end, ValuePoint top, Chart chart)
    {
        Start = start;
        End = end;
        Top = top;
        Chart = chart;
        TopRoot = GetIntersectionOfBaseAndVertical(start, end, top);
        Base = new Line(start, end);
        Vertical = new Line(TopRoot, top);
        Height = Util.GetDistance(Top, TopRoot);
        Area = new PeakAreaCalculator().CalculateArea(this);
    }

    public ValuePoint Start { get; }
    
    public ValuePoint End { get; }

    public ValuePoint Top { get; }
    
    public Chart Chart { get; }

    /// <summary>
    /// TopRoot is the intersection of line drawn from Start to End and vertical line drawn from Top.
    /// </summary>
    public ValuePoint TopRoot { get; }
    
    public Line Base { get; }

    public Line Vertical { get; } 
    
    public double Height { get; }

    public double Area { get; }

    public override string ToString()
    {
        return $"Start: {Start}, End: {End}, Top: {Top}, Height: {Height}, TopRoot: {TopRoot}, Base: {Base}, Vertical: {Vertical}, Chart: {Chart}";
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
    
    // from Chat GPT
    private ValuePoint GetIntersectionOfBaseAndVertical(ValuePoint start, ValuePoint end, ValuePoint top)
    {
        var x1 = start.X;
        var y1 = start.Y;
        
        var x2 = end.X;
        var y2 = end.Y;
        
        var m1 = (y2 - y1) / (x2 - x1);
        var b1 = y1 - m1 * x1;
        
        var intersectionX = top.X;
        var intersectionY = m1 * intersectionX + b1;

        var ret = new ValuePoint(intersectionX, intersectionY);
        return ret;
    }
    
    
}