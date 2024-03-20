namespace Raman.Core;

public class NewClosestSpectrumCalculator
{
    public Spectrum GetClosestSpectrum(List<Spectrum> spectra, ValuePoint point, double maxAllowedDistance = double.MaxValue)
    {
        if (!spectra.Any()) return null;
        var segments = GetAllSegments(spectra);
        segments = segments.OrderBy(segment => GetDistanceToSegment(point, segment)).ToList();
        var closestSegment = segments.First();
        Console.WriteLine("Point: " + point);
        var distanceToClosestSegment = GetDistanceToSegment(point, closestSegment);
        Console.WriteLine("distanceToClosestSegment: " + distanceToClosestSegment);
        var ret = distanceToClosestSegment <= maxAllowedDistance ? closestSegment.Spectrum : null;
        return ret;
    }
    
    // chatGPT prompt: create c# method for getting distance between line segment and point
    public static double GetDistanceToSegment(ValuePoint point, LineSegment segment)
    {
        // Calculate the squared length of the line segment AB
        var l2 = Math.Pow(segment.End.X - segment.Start.X, 2) + Math.Pow(segment.End.Y - segment.Start.Y, 2);

        // If the line segment is degenerate (i.e., A == B), return the distance to A
        if (l2 == 0.0)
            return Math.Sqrt(Math.Pow(point.X - segment.Start.X, 2) + Math.Pow(point.Y - segment.Start.Y, 2));

        // Calculate the projection of point C onto the line segment AB
        var t = Math.Max(0, Math.Min(1, ((point.X - segment.Start.X) * (segment.End.X - segment.Start.X) + (point.Y - segment.Start.Y) * (segment.End.Y - segment.Start.Y)) / l2));

        var x = segment.Start.X + t * (segment.End.X - segment.Start.X);
        var y = segment.Start.Y + t * (segment.End.Y - segment.Start.Y);
        var projection = new ValuePoint(x, y);

        // Return the distance between point C and the projection
        return Math.Sqrt(Math.Pow(point.X - projection.X, 2) + Math.Pow(point.Y - projection.Y, 2));
    }

    private List<LineSegment> GetAllSegments(List<Spectrum> spectra)
    {
        var ret = spectra.SelectMany(spectrum => GetLineSegments(spectrum)).ToList();
        return ret;
    }

    private List<LineSegment> GetLineSegments(Spectrum spectrum)
    {
        var ret = new List<LineSegment>();
        for (var i = 0; i < spectrum.Points.Count - 1; i++)
        {
            var start = spectrum.Points[i];
            var end = spectrum.Points[i + 1];
            var segment = new LineSegment(start, end, spectrum);
            ret.Add(segment);
        }
        return ret;
    }
}
