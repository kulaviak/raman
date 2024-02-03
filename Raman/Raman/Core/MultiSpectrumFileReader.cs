using System.IO;

namespace Raman.Core;

public class MultiSpectrumFileReader
{
    private readonly string _filePath;

    private readonly List<string> _ignoredLines = new List<string>();

    public List<string> IgnoredLines => _ignoredLines;

    public MultiSpectrumFileReader(string filePath)
    {
        _filePath = filePath;
    }
        
    /// <summary>
    /// Reads multi spectrum file. If x value or y value of the point can not be evaluated (invalid number or it is just empty string), then the point is ignored. 
    /// </summary>
    public List<List<Point>> TryReadFile()
    {
        try
        {
            var lines = File.ReadLines(_filePath).ToList().ToList();
            var ret = new List<List<Point>>();
            if (lines.Count >= 2)
            {
                var xLine = lines.First();
                var xValues = TryParseLine(xLine);
                if (xValues != null)
                {
                    for (var i = 1; i < lines.Count; i++)
                    {
                        var yLine = lines[i];
                        var yValues = TryParseLine(yLine);
                        if (yValues != null)
                        {
                            if (xValues.Count == yValues.Count)
                            {
                                var points = GetPoints(xValues, yValues);
                                ret.Add(points);
                            }
                            else
                            {
                                _ignoredLines.Add(yLine);
                            }
                        }
                    }   
                }
                else
                {
                    _ignoredLines.Add(xLine);
                }
            }
            return ret;
        }
        catch (Exception ex)
        {
            throw new Exception($"Loading file {_filePath} failed.", ex);
        }
    }

    private static List<Point> GetPoints(List<decimal?> xValues, List<decimal?> yValues)
    {
        var ret = new List<Point>();
        for (var i = 0; i < xValues.Count; i++)
        {
            if (xValues[i] != null && yValues[i] != null)
            { 
                var point = new Point(xValues[i].Value, yValues[i].Value);
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
            return null;
        }
    }
}