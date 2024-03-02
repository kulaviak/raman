using System.IO;

namespace Raman.Core;

public class PeakAnalysisExporter
{
    public void ExportPeaks(string filePath, List<Peak> peaks)
    {
        try
        {
            var peaksGroupedByX = peaks.GroupBy(peak => peak.TopRoot.X).ToDictionary(x => x.Key, y => y.ToList());
            var counter = 1;
            var lines = new List<string>();
            foreach (var key in peaksGroupedByX.Keys.OrderBy(x => x))
            {
                var group = peaksGroupedByX.Get(key);
                var peaksWithSameTopRoot = group.OrderBy(x => x.Chart.Name).ToList();
                var peakLines = GetLines(peaksWithSameTopRoot, counter);
                lines.AddRange(peakLines);
                counter++;
            }
            File.WriteAllLines(filePath, lines);
        }
        catch (Exception ex)
        {
            throw new AppException($"Saving file {filePath} failed. Reason: {ex.Message}", ex);
        }
    }

    private List<string> GetLines(List<Peak> peaks, int peakNumber)
    {
        var ret = peaks.Select(x => ToLine(x, peakNumber)).ToList();
        return ret;
    }

    private string ToLine(Peak peak, int peakNumber)
    {
        var ret = $"{peakNumber}\t{peak.Chart.Name}\t{Util.Format(peak.Height)}\t{Util.Format(peak.Start.X)}\t{Util.Format(peak.End.X)}\t{Util.Format(peak.TopRoot.X)}";
        return ret;
    }
}