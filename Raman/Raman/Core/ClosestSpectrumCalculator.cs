using Raman.Drawing;

namespace Raman.Core;

/// <summary>
/// Spectrum calculator must work in coordination system of canvas! Because user wants the closest spectrum to the mouse position. The
/// coordination system of spectrum has axes x and y that have different dimensions. It doesn't make sense measure distance in such coordination
/// system.
/// </summary>
public class ClosestSpectrumCalculator
{
    public Spectrum GetClosestSpectrum(List<Spectrum> spectra, Point point, CanvasCoordSystem coordSystem, double maxAllowedDistance = double.MaxValue)
    {
        if (!spectra.Any()) return null;
        var segments = GetAllSegments(spectra);
        segments = segments.OrderBy(segment => GetDistanceToSegment(point, segment, coordSystem)).ToList();
        var closestSegment = segments.First();
        // Console.WriteLine("Point: " + point);
        var distanceToClosestSegment = GetDistanceToSegment(point, closestSegment, coordSystem);
        // Console.WriteLine("distanceToClosestSegment: " + distanceToClosestSegment);
        var ret = distanceToClosestSegment <= maxAllowedDistance ? closestSegment.Spectrum : null;
        return ret;
    }
    
    // chatGPT prompt: create c# method for getting distance between line segment and point
    public static double GetDistanceToSegment(Point point, Point start, Point end)
    {
        // Calculate the squared length of the line segment AB
        var l2 = Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2);

        // If the line segment is degenerate (i.e., A == B), return the distance to A
        if (l2 == 0.0)
            return Math.Sqrt(Math.Pow(point.X - start.X, 2) + Math.Pow(point.Y - start.Y, 2));

        // Calculate the projection of point C onto the line segment AB
        var t = Math.Max(0, Math.Min(1, ((point.X - start.X) * (end.X - start.X) + (point.Y - start.Y) * (end.Y - start.Y)) / l2));

        var x = start.X + t * (end.X - start.X);
        var y = start.Y + t * (end.Y - start.Y);
        var projection = new ValuePoint(x, y);

        // Return the distance between point C and the projection
        var ret = Math.Sqrt(Math.Pow(point.X - projection.X, 2) + Math.Pow(point.Y - projection.Y, 2));
        return ret;
    }
    
    private static double GetDistanceToSegment(Point point, LineSegment segment, CanvasCoordSystem coordSystem)
    {
        var ret = GetDistanceToSegment(point, coordSystem.ToPixelPoint(segment.Start), coordSystem.ToPixelPoint(segment.End));
        return ret;
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
