using System.Globalization;
using Raman.File;

namespace Raman.Core;

public abstract class Util
{
    public static float GetPixelDistance(Point point1, Point point2)
    {
        var x = Math.Abs(point1.X - point2.X);
        var y = Math.Abs(point1.Y - point2.Y);
        var ret = (float) Math.Sqrt(x * x + y * y);
        return ret;
    }

    public static double GetDistance(ValuePoint point1, ValuePoint point2)
    {
        var x = Math.Abs(point1.X - point2.X);
        var y = Math.Abs(point1.Y - point2.Y);
        var ret = Math.Sqrt(x * x + y * y);
        return ret;
    }
    
    public static string Format(double value, int decimalPlaces)
    {
        var culture = new CultureInfo("en-US");
        culture.NumberFormat.NumberDecimalSeparator = AppSettings.DecimalSeparator;
        return Math.Round(value, decimalPlaces).ToString(culture);
    }

    public static bool IsCtrlKeyPressed()
    {
        return Control.ModifierKeys == Keys.Control;
    }

    public static void SetSpectrumVisibilityAccordingToCurrentVisibleSpectra(List<Spectrum> spectra, List<Spectrum> visibleSpectra)
    {
        var visibleSpectrumNames = visibleSpectra.Select(spectrum => spectrum.Name).ToHashSet();
        foreach (var spectrum in spectra)
        {
            spectrum.IsVisible = visibleSpectrumNames.Contains(spectrum.Name);
        }
    }
    
    public static ILineParser GetLineParser(string filePath)
    {
        if (filePath.ToUpper().EndsWith("TXT"))
        {
            return new TxtLineParser();
        }
        else if (filePath.ToUpper().EndsWith("CSV"))
        {
            return new CsvLineParser();
        }
        else
        {
            return new TxtLineParser();
        }
    }
}   