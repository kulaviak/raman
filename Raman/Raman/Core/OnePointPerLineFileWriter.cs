using System.IO;

namespace Raman.Core;

public class OnePointPerLineFileWriter
{
    public void WritePoints(List<ValuePoint> points, string filePath)
    {
        var lines = PointsToLines(points);
        File.WriteAllLines(filePath, lines);
    }

    private List<string> PointsToLines(List<ValuePoint> points)
    {
        var ret = points.Select(x => PointToLine(x)).ToList();
        return ret;
    }

    private string PointToLine(ValuePoint point)
    {
        return $"{Math.Round(point.X, AppConstants.EXPORT_DECIMAL_PLACES)}\t{Math.Round(point.Y, AppConstants.EXPORT_DECIMAL_PLACES)}";
    }
}