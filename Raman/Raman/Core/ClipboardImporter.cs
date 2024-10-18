using Raman.File;

namespace Raman.Core;

/// <summary>
/// Imports data from clipboard.
/// Expects lines to be separated by \r\n.
/// Expects values in line to be separated by tab or space.
/// Decimal separator can be dot or coma.
/// Clipboard from Excel or simple txt file satisfies this conditions. 
/// </summary>
public class ClipboardImporter
{
    public List<ValuePoint> ImportExcelData()
    {
        var data = Clipboard.GetText(TextDataFormat.Text);
        // support both windows and linux files
        string[] separators = ["\r\n", "\n"];
        var rows = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        var ret = new List<ValuePoint>();
        foreach (var row in rows)
        {
            var point = ParseValuePoint(row);
            if (point != null)
            {
                ret.Add(point);
            }
        }
        return ret;
    }

    private static ValuePoint ParseValuePoint(string line)
    {
        var numbers = new TxtLineParser().ParseLine(line).Select(x => x.Value).ToList();
        if (numbers.Count == 2)
        {
            return new ValuePoint(numbers[0], numbers[1]);
        }
        return null;
    }
}