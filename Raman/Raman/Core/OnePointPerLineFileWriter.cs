using System.IO;

namespace Raman.Core;

public class OnePointPerLineFileWriter
{
    private const int DECIMAL_PLACES = 3;
        
    public void WritePoints(List<Point> points, string filePath)
    {
        var lines = PointsToLines(points);
        File.WriteAllLines(filePath, lines);
    }

    private List<string> PointsToLines(List<Point> points)
    {
        var ret = points.Select(x => PointToLine(x)).ToList();
        return ret;
    }

    private string PointToLine(Point point)
    {
        return $"{Math.Round(point.X, DECIMAL_PLACES)}\t{Math.Round(point.Y, DECIMAL_PLACES)}";
    }
}