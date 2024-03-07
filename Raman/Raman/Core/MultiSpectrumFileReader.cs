using System.IO;

namespace Raman.Core;

public class MultiSpectrumFileReader
{
    private readonly string filePath;

    public MultiSpectrumFileReader(string filePath)
    {
        this.filePath = filePath;
    }

    /// <summary>
    /// Reads multi spectrum file. If x value or y value of the point is empty then the point is ignored.
    /// </summary>
    public List<List<ValuePoint>> TryReadFile()
    {
        List<string> lines;
        try
        {
            lines = File.ReadLines(filePath).ToList().Where(x => !x.IsNullOrWhiteSpace()).ToList();
        }
        catch (Exception e)
        {
            throw new AppException($"Opening file {filePath} failed: {e.Message}", e);
        }

        try
        {
            var ret = new List<List<ValuePoint>>();
            if (lines.Count >= 2)
            {
                var xLine = lines.First();
                var xValues = TryParseLine(xLine);
                if (xValues.Count < 2)
                {
                    throw new AppException($"There are less than two points. Line: {xLine}");
                }
                for (var i = 1; i < lines.Count; i++)
                {
                    var yLine = lines[i];
                    var yValues = TryParseLine(yLine);
                    if (yValues != null)
                    {
                        if (yValues.Count < 2)
                        {
                            throw new AppException($"There are less than two points. Line: {yLine}");
                        }

                        if (xValues.Count == yValues.Count)
                        {
                            var points = GetPoints(xValues, yValues);
                            points = points.OrderBy(x => x.X).ToList();
                            ret.Add(points);
                        }
                        else
                        {
                            throw new AppException(
                                $"Line with y coordinates doesn't have same count of numbers as line with x coordinates. Line: {yLine}");
                        }
                    }
                    else
                    {
                        throw new AppException($"Parsing line with y coordinates failed. Line: {yLine}");
                    }
                }
            }
            else
            {
                throw new AppException($"File has less than 2 lines.");
            }
            return ret;
        }
        catch (Exception e)
        {
            throw new AppException($"Parsing data from file {filePath} failed.", e);
        }
    }

    private static List<ValuePoint> GetPoints(List<decimal?> xValues, List<decimal?> yValues)
    {
        var ret = new List<ValuePoint>();
        for (var i = 0; i < xValues.Count; i++)
        {
            if (xValues[i] != null && yValues[i] != null)
            {
                var point = new ValuePoint(xValues[i].Value, yValues[i].Value);
                ret.Add(point);
            }
            // else ignore the point
        }

        return ret;
    }

    private static List<decimal?> TryParseLine(string line)
    {
        try
        {
            var parts = line.Split(' ', '\t');
            var numbers = parts.Select(x => !x.IsNullOrWhiteSpace() ? Util.UniversalParseDecimal(x) : null).ToList();
            var ret = numbers.Any() ? numbers : null;
            return ret;
        }
        catch (Exception e)
        {
            throw new AppException($"Parsing line {line} failed.", e);
        }
    }
}