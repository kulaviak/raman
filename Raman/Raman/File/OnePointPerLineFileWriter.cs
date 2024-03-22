namespace Raman.File;

public class OnePointPerLineFileWriter
{
    public void WritePoints(List<ValuePoint> points, string filePath)
    {
        var lines = PointsToLines(points);
        System.IO.File.WriteAllLines(filePath, lines);
    }

    private List<string> PointsToLines(List<ValuePoint> points)
    {
        var ret = points.Select(x => PointToLine(x)).ToList();
        return ret;
    }

    private string PointToLine(ValuePoint point)
    {
        return $"{Util.Format(point.X, AppConstants.EXPORT_DECIMAL_PLACES)}\t{Util.Format(point.Y, AppConstants.EXPORT_DECIMAL_PLACES)}";
    }
}