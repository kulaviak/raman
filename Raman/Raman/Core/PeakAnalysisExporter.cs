using System.IO;

namespace Raman.Core;

public class PeakAnalysisExporter
{
    public void ExportPeaks(string filePath, List<Peak> peaks)
    {
        try
        {
            var lines = GetLines(peaks);
            File.WriteAllLines(filePath, lines);
        }
        catch (Exception ex)
        {
            throw new AppException($"Saving file {filePath} failed. Reason: {ex.Message}", ex);
        }
    }

    private List<string> GetLines(List<Peak> peaks)
    {
        var ret = peaks.Select(x => ToLine(x)).ToList();
        return ret;
    }

    private string ToLine(Peak peak)
    {
        var ret = $"{Format(peak.Height)}\t{Format(peak.Start.X)}\t{Format(peak.End.X)}\t{Format(peak.TopRoot.X)}";
        return ret;
    }

    private string Format(decimal number)
    {
        return Util.Format(number, AppConstants.EXPORT_DECIMAL_PLACES);
    }
}