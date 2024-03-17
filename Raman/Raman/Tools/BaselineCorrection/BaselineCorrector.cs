namespace Raman.Tools.BaselineCorrection;

public class BaselineCorrector
{
    /// <summary>
    /// Expects that baseline points are calculated exactly at same places as chart points and this is exactly what SplineBaselineCalculator does
    /// </summary>
    public List<ValuePoint> CorrectChartByBaseline(List<ValuePoint> chartPoints, List<ValuePoint> baselinePoints)
    {
        var ret = new List<ValuePoint>();
        foreach (var chartPoint in chartPoints)
        {
            var baselinePoint = baselinePoints.FirstOrDefault(point => point.X == chartPoint.X);
            var correctedPoint = baselinePoint != null ? new ValuePoint(chartPoint.X, chartPoint.Y - baselinePoint.Y) : chartPoint;
            ret.Add(correctedPoint);
        }
        return ret;
    }
}