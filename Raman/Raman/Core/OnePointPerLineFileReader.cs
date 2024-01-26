using System.IO;

namespace Raman.Core;

public class OnePointPerLineFileReader
{
    private readonly string _filePath;

    private readonly List<string> _ignoredLines = new List<string>();

    public List<string> IgnoredLines => _ignoredLines;

    public OnePointPerLineFileReader(string filePath)
    {
        _filePath = filePath;
    }
        
    public List<Point> TryReadFile()
    {
        try
        {
            var lines = File.ReadLines(_filePath).ToList().Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            var points = TryParseLines(lines);
            points = points.OrderBy(x => x.X).ToList();
            return points;
        }
        catch (Exception ex)
        {
            throw new Exception($"Loading file {_filePath} failed.", ex);
        }
    }

    private List<Point> TryParseLines(List<string> lines)
    {
        var ret = new List<Point>();
        try
        {
            foreach (var line in lines)
            {
                var point = TryParseLine(line);
                if (point != null)
                {
                    ret.Add(point);
                }
                else
                {
                    _ignoredLines.Add(line);
                }
            }
            return ret;
        }
        catch (Exception e)
        {
            // ignore
            return null;
        }      
    }

    public static Point TryParseLine(string line)
    {
        try
        {
            // split on white space or tab
            var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
            {
                var x = Util.UniversalParseDecimal(parts[0]);
                var y = Util.UniversalParseDecimal(parts[1]);
                if (x != null && y != null)
                {
                    return new Point(x.Value, y.Value);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        catch (Exception e)
        {
            return null;
        }
    }

}