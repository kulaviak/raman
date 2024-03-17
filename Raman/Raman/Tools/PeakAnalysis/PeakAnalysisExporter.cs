namespace Raman.Tools.PeakAnalysis;

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
            System.IO.File.WriteAllLines(filePath, lines);
        }
        catch (Exception ex)
        {
            throw new AppException($"Saving file {filePath} failed. Reason: {ex.Message}", ex);
        }
    }

    private string GetHeader()
    {
        return "Spectrum_Name;Peak_Number;Peak_Start;Peak_End;Peak_Height;Peak_Position;Area";
    }

    private List<string> GetLines(List<Peak> peaks)
    {
        var ret = peaks.Select(x => ToLine(x)).ToList();
        return ret;
    }

    private string ToLine(Peak peak)
    {
        var ret = $"{peak.Chart.Name};{0};{Util.Format(peak.Start.X)};{Util.Format(peak.End.X)};{Util.Format(peak.Height)};{Util.Format(peak.TopRoot.X)};{Util.Format(peak.Area)}";
        return ret;
    }
}