namespace Raman.File;

public class SingleSpectrumFileReader
{
    private readonly string filePath;

    public SingleSpectrumFileReader(string filePath)
    {
        this.filePath = filePath;
    }

    /// <summary>
    /// Reads single spectrum files. If x value or y value is missing then the point is ignored.
    /// </summary>
    public List<ValuePoint> TryReadFile()
    {
        try
        {
            var lines = System.IO.File.ReadLines(filePath).ToList().Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            var points = TryParseLines(lines);
            points = points.OrderBy(x => x.X).ToList();
            if (points.Count < 2)
            {
                throw new AppException($"File {filePath} has less than two points.");
            }

            return points;
        }
        catch (Exception ex)
        {
            throw new AppException($"Loading file {filePath} failed.", ex);
        }
    }

    private List<ValuePoint> TryParseLines(List<string> lines)
    {
        var ret = lines.Select(line => TryParseLine(line)).Where(x => x != null).ToList();
        return ret;
    }

    public static ValuePoint TryParseLine(string line)
    {
        try
        {
            var parts = Util.SplitOnWhitespaceOrTab(line);
            if (parts.Length >= 2)
            {
                var x = Util.UniversalParseDouble(parts[0]);
                var y = Util.UniversalParseDouble(parts[1]);
                if (x != null && y != null)
                {
                    return new ValuePoint(x.Value, y.Value);
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
}