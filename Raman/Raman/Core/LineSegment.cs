namespace Raman.Core;

public class LineSegment
{
    public ValuePoint Start { get; }
    
    public ValuePoint End { get; }
    
    public Spectrum Spectrum { get; }

    public LineSegment(ValuePoint start, ValuePoint end, Spectrum spectrum)
    {
        Start = start;
        End = end;
        Spectrum = spectrum;
    }

    public override string ToString()
    {
        return $"Start: {Start}, End: {End}, Spectrum?.Name: {Spectrum?.Name}";
    }
}