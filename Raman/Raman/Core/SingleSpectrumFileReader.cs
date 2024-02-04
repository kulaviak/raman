using System.IO;

namespace Raman.Core;

public class SingleSpectrumFileReader
{
    private readonly string _filePath;
    
    public SingleSpectrumFileReader(string filePath)
    {
        _filePath = filePath;
    }
        
    /// <summary>
    /// Reads single spectrum files. If x value or y value is missing then the point is ignored.
    /// </summary>
    public List<Point> TryReadFile()
    {
        try
        {
            var lines = File.ReadLines(_filePath).ToList().Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            var points = TryParseLines(lines);
            points = points.OrderBy(x => x.X).ToList();
            if (points.Count < 2)
            {
                throw new AppException($"File {_filePath} has less than two points.");
            }
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
                    // ignore
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
            var parts = SplitOnWhitespaceOrTab(line);
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
            throw new AppException($"Parsing line {line} failed.", e);
        }
    }
    
    public static string[] SplitOnWhitespaceOrTab(string line)
    {
        return line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
    }
}