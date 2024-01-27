using MathNet.Numerics.Interpolation;

namespace Raman.Core;

/// <summary>
/// https://www.youtube.com/watch?v=D06d88NLdR4
/// </summary>
public class SplineBaselineCalculator
{
    public List<Point> GetBaseline(List<Point> chartPoints, List<Point> correctionPoints)
    {
        if (correctionPoints.Count < 4)
        {
            throw new AppException("Calculation of baseline failed. There are less than 4 correction points.");
        }
        if (chartPoints.Count < 2)
        {
            throw new AppException("Calculation of baseline failed. There are less than 2 chart points.");
        }
        correctionPoints = correctionPoints.OrderBy(point => point.X).ToList();
        var ret = new List<Point>();
        var correctionStart = correctionPoints[0].X;
        var correctionEnd = correctionPoints[correctionPoints.Count - 1].X;
        var chartPointsBetweenCorrectionPoints = chartPoints.Where(point => correctionStart  <= point.X && point.X <= correctionEnd).ToList();
        var baselinePoints = GetBaselineInternal(chartPointsBetweenCorrectionPoints, correctionPoints);
        ret.AddRange(baselinePoints);
        return ret;
    }

    private List<Point> GetBaselineInternal(List<Point> chartPoints, List<Point> correctionPoints)
    {
        var xValues = correctionPoints.Select(point => (double) point.X).ToArray();
        var yValues = correctionPoints.Select(point => (double) point.Y).ToArray();

        var spline = CubicSpline.InterpolateNatural(xValues, yValues);

        var ret = new List<Point>();
        foreach (var point in chartPoints)
        {
            var y = spline.Interpolate((double) point.X);
            var interpolatedPoint = new Point(point.X, (decimal) y);
            ret.Add(interpolatedPoint);
        }
        
        return ret;
    }
}