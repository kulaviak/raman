namespace Raman.Drawing;

/// <summary>
/// Calculates closest chart to the mouse position.
/// </summary>
public class ClosestChartCalculator
{

    public Chart GetClosestChart(List<Chart> charts, Point point, CanvasCoordSystem coordSystem, decimal maxAllowedDistance = decimal.MaxValue)
    {
        var minimumDistance = double.MaxValue;
        Chart ret = null;
        foreach (var chart in charts)
        {
            var distance = GetDistance(chart, point, coordSystem);
            if (distance < minimumDistance && distance < (double) maxAllowedDistance)
            {
                minimumDistance = distance;
                ret = chart;
            }
        }
        return ret;
    }

    private double GetDistance(Chart chart, Point point, CanvasCoordSystem coordSystem)
    {
        var ret = chart.Points.Min(chartPoint => Util.GetPixelDistance(coordSystem.ToPixelPoint(chartPoint), point));
        return ret;
    }
}