namespace Raman.Drawing;

/// <summary>
/// Calculates closest chart to the mouse position.
/// </summary>
public class ClosestChartCalculator
{

    public Chart GetClosestChart(List<Chart> charts, Point point, CanvasCoordSystem coordSystem, decimal maxAllowedDistance = Decimal.MaxValue)
    {
        var minimumDistance = Decimal.MaxValue;
        Chart ret = null;
        foreach (var chart in charts)
        {
            var distance = GetDistance(chart, point, coordSystem);
            if (distance < minimumDistance && distance < maxAllowedDistance)
            {
                minimumDistance = distance;
                ret = chart;
            }
        }
        return ret;
    }

    private decimal GetDistance(Chart chart, Point point, CanvasCoordSystem coordSystem)
    {
        var ret = chart.Points.Min(chartPoint => Util.GetPixelDistance(coordSystem.ToPixelPoint(chartPoint), point));
        return (decimal) ret;
    }
}