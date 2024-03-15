using System.IO;

namespace Raman.Core;

public class PeakAnalysisExporter
{
    public void ExportPeaks(string filePath, List<Peak> peaks)
    {
        try
        {
            peaks = peaks.OrderBy(peak => peak.TopRoot.X).ThenBy(x => x.Chart.Name).ToList();
            var peakLines = GetLines(peaks);
            var lines = new List<string>();
            lines.Add(GetHeader());
            lines.AddRange(peakLines);
            File.WriteAllLines(filePath, peakLines);
        }
        catch (Exception ex)
        {
            throw new AppException($"Saving file {filePath} failed. Reason: {ex.Message}", ex);
        }
    }

    private string GetHeader()
    {
        return "Spectrum_Name\tPeak_Number\tPeak_Start\tPeak_End\tPeak_Height\tPeak_Position\tArea";
    }

    private List<string> GetLines(List<Peak> peaks)
    {
        var ret = peaks.Select(x => ToLine(x)).ToList();
        return ret;
    }

    private string ToLine(Peak peak)
    {
        var ret = $"{peak.Chart.Name}\t{0}\t{Util.Format(peak.Start.X)}\t{Util.Format(peak.End.X)}\t{Util.Format(peak.Height)}\t{Util.Format(peak.TopRoot.X)}\t{Util.Format(peak.Area)}";
        return ret;
    }
}