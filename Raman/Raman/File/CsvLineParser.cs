using System.Globalization;

namespace Raman.File;

public class CsvLineParser : ILineParser
{
    public List<double?> ParseLine(string line)
    {
        try
        {
            var parts = line.Split([',']).Select(x => x.Trim()).ToList();
            var ret = parts.Select(x => ParseDouble(x)).ToList();
            return ret;
        }
        catch (Exception e)
        {
            throw new AppException($"Parsing line {line} failed.", e);
        }
    }
    
    private static double? ParseDouble(string str)
    {
        if (str.IsNullOrWhiteSpace())
        {
            return null;
        }
        var style = NumberStyles.Number;
        var culture = CultureInfo.CreateSpecificCulture("en-US");
        if (double.TryParse(str, style, culture, out var ret))
        {
            return ret;
        }
        else
        {
            throw new AppException($"Parsing string {str} as number failed.");
        }
    }
}