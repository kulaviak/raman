using MathNet.Numerics.Interpolation;

namespace Raman.Tools.BaselineCorrection;

/// <summary>
/// https://www.youtube.com/watch?v=D06d88NLdR4
/// </summary>
public class SplineBaselineCalculator
{
    public List<ValuePoint> GetBaseline(List<double> xPositions, List<ValuePoint> correctionPoints)
    {
        if (xPositions.Count < 2)
        {
            throw new AppException("Calculation of baseline failed. There are less than 2 spectrum points.");
        }
        if (correctionPoints.Count < 4)
        {
            throw new AppException("Calculation of baseline failed. There are less than 4 correction points.");
        }
        var ret = new List<ValuePoint>();
        var spline = GetSplineFromCorrectionPoints(correctionPoints);
        foreach (var x in xPositions)
        {
            var y = spline.Interpolate(x);
            var interpolatedPoint = new ValuePoint(x, y);
            ret.Add(interpolatedPoint);
        }
        return ret;
    }

    private static CubicSpline GetSplineFromCorrectionPoints(List<ValuePoint> correctionPoints)
    {
        var xValues = correctionPoints.Select(point => point.X).ToArray();
        var yValues = correctionPoints.Select(point => point.Y).ToArray();
        var spline = CubicSpline.InterpolateNatural(xValues, yValues);
        return spline;
    }
}