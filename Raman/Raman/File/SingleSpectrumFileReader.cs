namespace Raman.File;

public class SingleSpectrumFileReader(string filePath)
{

    private ILineParser lineParser = Util.GetLineParser(filePath);
    
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

    private ValuePoint TryParseLine(string line)
    {
        try
        {
            var parts = lineParser.ParseLine(line);
            if (parts.Count >= 2 && parts[0] != null && parts[1] != null)
            {
                return new ValuePoint(parts[0].Value, parts[1].Value);
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