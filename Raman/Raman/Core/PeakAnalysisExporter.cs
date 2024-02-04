using System.IO;

namespace Raman.Core;

public class PeakAnalysisExporter
{

    private const int DECIMAL_PLACES = 3;
    
    public void ExportPeaks(string filePath, List<ExportedPeak> peaks)
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

    private List<string> GetLines(List<ExportedPeak> peaks)
    {
        var ret = peaks.Select(x => ToLine(x)).ToList();
        return ret;
    }

    private string ToLine(ExportedPeak peak)
    {
        var ret = $"{Format(peak.Height)}\t{Format(peak.LeftX)}\t{Format(peak.RightX)}\t{Format(peak.PeakX)}";
        return ret;
    }

    private string Format(decimal number)
    {
        return Util.Format(number, DECIMAL_PLACES);
    }
}