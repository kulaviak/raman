using MathNet.Numerics.Interpolation;
using Point = System.Drawing.Point;

namespace Raman.Drawing;

/// <summary>
/// Calculates which peak should be removed. It is the peak, which base is the closest to the location where user clicked.
/// </summary>
public class PeakToRemoveCalculator
{
    public Peak GetPeakToRemove(List<Peak> peaks, CanvasCoordSystem coordSystem, Point location)
    {
        var ret = peaks.MinByOrDefault(peak => GetDistanceToPeak(peak, coordSystem, location));
        return ret;
    }

    private decimal GetDistanceToPeak(Peak peak, CanvasCoordSystem coordSystem, Point location)
    {
        var ret = GetDistanceToLine(peak.Base, coordSystem, location);
        return ret;
    }

    private decimal GetDistanceToLine(Line line, CanvasCoordSystem coordSystem, Point location)
    {
        var linePoints = GetLinePoints(line, coordSystem);
        var ret = linePoints.Min(point => Util.GetPixelDistance(point, location));
        return (decimal) ret;
    }
    
    private static List<Point> GetLinePoints(Line line, CanvasCoordSystem coordSystem)
    {
        var xLineValues = new[] {(double) coordSystem.ToPixelX(line.Start.X), coordSystem.ToPixelX(line.End.X)};
        var yLineValues = new[] {coordSystem.ToPixelY(line.Start.Y), (double) coordSystem.ToPixelY(line.End.Y)}; 
        var spline = LinearSpline.Interpolate(xLineValues, yLineValues);
        var steps = 100;
        var start = xLineValues.Min();
        var end = xLineValues.Max();
        var increment = (end - start) / steps;
        var ret = new List<Point>();
        for (var i = 0; i < steps; i++)
        {
            var x = (int) (start + i * increment);
            var y = (int) (spline.Interpolate(x));
            ret.Add(new Point(x, y));
        }
        return ret;
    }
}