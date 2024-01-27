namespace Raman.Core;

public class BaselineCorrector
{
    public List<Point> CorrectChartByBaseline(List<Point> chartPoints, List<Point> baselinePoints)
    {
        // make sure baseline points are ordered
        baselinePoints = baselinePoints.OrderBy(x => x.X).ToList();
        if (chartPoints.Count < 2)
        {
            throw new Exception("Baseline correction failed. There must be at least 2 chart points.");
        }
        if (!baselinePoints.Any())
        {
            throw new Exception("Baseline correction failed. There must be at least 2 baseline points.");
        }
        var ret = new List<Point>();
        foreach (var chartPoint in chartPoints)
        {
            var baselineValue = GetBaselineValue(chartPoint.X, baselinePoints);
            var correctedPoint = new Point(chartPoint.X, chartPoint.Y - baselineValue);
            ret.Add(correctedPoint);
        }
        return ret;
    }

    private static decimal GetBaselineValue(decimal x, List<Point> baselinePoints)
    {
        if (x < baselinePoints.First().X || x > baselinePoints.Last().X)
        {
            return 0;
        }
        var indexWithGreaterX = baselinePoints.FindIndex(point => point.X >= x);
        if (baselinePoints[indexWithGreaterX].X == x)
        {
            return baselinePoints[indexWithGreaterX].Y;
        }
        else
        {
            var firstPoint = baselinePoints[indexWithGreaterX];
            var secondPoint = baselinePoints[indexWithGreaterX - 1];
            var ret = GetLinearInterpolatedValue(x, firstPoint, secondPoint);
            return ret;
        }
    }

    private static decimal GetLinearInterpolatedValue(decimal x, Point firstPoint, Point secondPoint)
    {
        var slope= (secondPoint.Y - firstPoint.Y) / (secondPoint.X - firstPoint.X);
        var ret = firstPoint.Y + slope * x;
        return ret;
    }
}