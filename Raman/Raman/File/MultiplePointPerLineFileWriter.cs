using System.Text;

namespace Raman.File;

/// <summary>
/// Export spectra to one file. First column contains x values. Other columns contains y values.
/// </summary>
public class MultiplePointPerLineFileWriter
{
    public void WritePoints(List<Spectrum> spectra, string filePath)
    {
        var lines = SpectraToLines(spectra);
        System.IO.File.WriteAllLines(filePath, lines);
    }

    private List<string> SpectraToLines(List<Spectrum> spectra)
    {
        var allXValues = spectra.SelectMany(x => x.Points).Select(point => point.X).Distinct().ToList();
        var ret = new List<string>();
        foreach (var x in allXValues)
        {
            var line = GetLine(x, spectra);
            ret.Add(line);
        }
        return ret;
    }

    private string GetLine(double x, List<Spectrum> spectra)
    {
        var sb = new StringBuilder(Util.Format(x, AppConstants.EXPORT_DECIMAL_PLACES));
        foreach (var spectrum in spectra)
        {
            var point = spectrum.Points.First(point => point.X == x);
            sb.Append("\t"); 
            if (point != null)
            {
                sb.Append(Util.Format(point.Y, AppConstants.EXPORT_DECIMAL_PLACES));
            }
        }
        var ret = sb.ToString();
        return ret;
    }
}