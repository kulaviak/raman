namespace Raman.Core;

public class BaselineCorrector
{
    /// <summary>
    /// Expects that baseline points are calculated exactly at same places as chart points and this is exactly what SplineBaselineCalculator does
    /// </summary>
    public List<Point> CorrectChartByBaseline(List<Point> chartPoints, List<Point> baselinePoints)
    {
        var ret = new List<Point>();
        foreach (var chartPoint in chartPoints)
        {
            var baselinePoint = baselinePoints.FirstOrDefault(point => point.X == chartPoint.X);
            var correctedPoint = baselinePoint != null ? new Point(chartPoint.X, chartPoint.Y - baselinePoint.Y) : chartPoint;
            ret.Add(correctedPoint);
        }
        return ret;
    }
}