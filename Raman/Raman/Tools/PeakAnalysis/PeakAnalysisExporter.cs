namespace Raman.Tools.PeakAnalysis;

public class PeakAnalysisExporter
{
    public void ExportPeaks(string filePath, List<Peak> peaks)
    {
        try
        {
            peaks = peaks.OrderBy(peak => peak.TopRoot.X).ThenBy(x => x.Spectrum.Name).ToList();
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
        var ret = peaks.Select(x => ToLine(x, peaks)).ToList();
        return ret;
    }

    private string ToLine(Peak peak, List<Peak> peaks)
    {
        var ret = $"{peak.Spectrum.Name};{GetPeakOrderNumber(peak, peaks) + 1};{Util.Format(peak.Start.X)};{Util.Format(peak.End.X)};{Util.Format(peak.Height)};{Util.Format(peak.TopRoot.X)};{Util.Format(peak.Area)}";
        return ret;
    }

    private int GetPeakOrderNumber(Peak peak, List<Peak> peaks)
    {
        var spectrumPeaks = peaks.Where(x => x.Spectrum.Name == peak.Spectrum.Name).OrderBy(x => x.TopRoot.X).ToList();
        for (var i = 0; i < spectrumPeaks.Count; i++)
        {
            if (peak.TopRoot.X == spectrumPeaks[i].TopRoot.X)
            {
                return i;
            }
        }
        throw new AppException("Peak number couldn't be calculated.");
    }
}