using MathNet.Numerics.Interpolation;

namespace Raman.Core;

/// <summary>
/// https://www.youtube.com/watch?v=D06d88NLdR4
/// </summary>
public class SplineBaselineCalculator
{
    public List<Point> GetBaseline(List<decimal> xPositions, List<Point> correctionPoints)
    {
        if (xPositions.Count < 2)
        {
            throw new AppException("Calculation of baseline failed. There are less than 2 chart points.");
        }

        if (correctionPoints.Count < 4)
        {
            throw new AppException("Calculation of baseline failed. There are less than 4 correction points.");
        }

        var ret = new List<Point>();
        var spline = GetSplineFromCorrectionPoints(correctionPoints);
        foreach (var x in xPositions)
        {
            var y = spline.Interpolate((double) x);
            var interpolatedPoint = new Point(x, (decimal) y);
            ret.Add(interpolatedPoint);
        }

        return ret;
    }

    private static CubicSpline GetSplineFromCorrectionPoints(List<Point> correctionPoints)
    {
        var xValues = correctionPoints.Select(point => (double) point.X).ToArray();
        var yValues = correctionPoints.Select(point => (double) point.Y).ToArray();
        var spline = CubicSpline.InterpolateNatural(xValues, yValues);
        return spline;
    }
}