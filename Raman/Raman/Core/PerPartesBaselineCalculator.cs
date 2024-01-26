namespace Raman.Core;

public class PerPartesBaselineCalculator
{
    public List<Point> GetBaseline(List<Point> chartPoints, List<Point> correctionPoints, int degree)
    {
        if (correctionPoints.Count == 0)
        {
            throw new AppException("Calculation of baseline failed. There are no correction points.");
        }
        if (chartPoints.Count == 0)
        {
            throw new AppException("Calculation of baseline failed. There are no chart points.");
        }
        correctionPoints = correctionPoints.OrderBy(point => point.X).ToList();
        var ret = new List<Point>();
        var correctionStart = correctionPoints[0].X;
        var correctionEnd = correctionPoints[correctionPoints.Count - 1].X;
        var chartPointsBetweenCorrectionPoints = chartPoints.Where(point => correctionStart  <= point.X && point.X <= correctionEnd).ToList();
        var baselinePoints = GetBaselineInternal(chartPointsBetweenCorrectionPoints, correctionPoints, degree);
        ret.AddRange(baselinePoints);
        return ret;
    }

    private List<Point> GetBaselineInternal(List<Point> chartPoints, List<Point> correctionPoints, int degree)
    {
        var ret = new List<Point>();
        var batches = SplitList(correctionPoints, degree + 1);
        foreach (var batchPoints in batches)
        {
            var correctionStart = batchPoints[0].X;
            var correctionEnd = batchPoints[batchPoints.Count - 1].X;
            var chartPointsBetweenCorrectionPoints = chartPoints.Where(point => correctionStart <= point.X && point.X <= correctionEnd).ToList();
            var baselinePoints = new PolynomialFitCalculator().GetCorrectedPoints(chartPointsBetweenCorrectionPoints, batchPoints, degree + 1);
            ret.AddRange(baselinePoints);
        }
        return ret;
    }
        
    private static List<List<Point>> SplitList(List<Point> points, int batchSize)
    {
        var batches = new List<List<Point>>();
        for (var i = 0; i < points.Count; i += batchSize)
        {
            var batch = points.GetRange(i, Math.Min(batchSize, points.Count - i));
            batches.Add(batch);
        }
        return batches;
    }
}