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
        var sep = AppSettings.CsvSeparator;
        return $"Spectrum_Name{sep}Peak_Number{sep}Peak_Start{sep}Peak_End{sep}Peak_Height{sep}Peak_Position{sep}Area";
    }

    private List<string> GetLines(List<Peak> peaks)
    {
        var ret = peaks.Select(x => ToLine(x, peaks)).ToList();
        return ret;
    }

    private string ToLine(Peak peak, List<Peak> peaks)
    {
        var sep = AppSettings.CsvSeparator;
        var ret = $"{peak.Spectrum.Name}{sep}{GetPeakOrderNumber(peak, peaks) + 1}{sep}{Util.Format(peak.Start.X)}{sep}{Util.Format(peak.End.X)}" +
                  $"{sep}{Util.Format(peak.Height)}{sep}{Util.Format(peak.TopRoot.X)}{sep}{Util.Format(peak.Area)}";
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