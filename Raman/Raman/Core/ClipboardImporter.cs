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
            var cells = Util.SplitOnWhitespaceOrTab(row);
            var point = ParseValuePoint(cells);
            if (point != null)
            {
                ret.Add(point);
            }
        }
        return ret;
    }

    private static ValuePoint ParseValuePoint(string[] cells)
    {
        if (cells.Length == 2)
        {
            var x = Util.UniversalParseDouble(cells[0]);
            var y = Util.UniversalParseDouble(cells[1]);
            if (x != null && y != null)
            {
                return new ValuePoint(x.Value, y.Value);
            }
        }
        return null;
    }
}