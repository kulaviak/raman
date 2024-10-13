using System.Globalization;

namespace Raman.Core;

public abstract class Util
{
    public static float GetPixelDistance(Point point1, Point point2)
    {
        var x = Math.Abs(point1.X - point2.X);
        var y = Math.Abs(point1.Y - point2.Y);
        var ret = (float) Math.Sqrt(x*x + y*y);
        return ret;
    }
    
    public static double GetDistance(ValuePoint point1, ValuePoint point2)
    {
        var x = Math.Abs(point1.X - point2.X);
        var y = Math.Abs(point1.Y - point2.Y);
        var ret = Math.Sqrt(x*x + y*y);
        return ret;
    }
        
    /// <summary>
    /// Universal decimal parser. Decimal delimiter can be both '.' and ','
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static Double? UniversalParseDouble(string str)
    {
        if (str.IsNullOrWhiteSpace())
        {
            return null;
        }
        str = str.Replace(",", ".");
        var style = NumberStyles.Number;
        var culture = CultureInfo.CreateSpecificCulture("en-US");
        if (Double.TryParse(str, style, culture, out var ret))
        {
            return ret;
        }
        else
        {
            throw new AppException($"Parsing string {str} as number failed.");
        }
    }

    public static string Format(double value, int decimalPlaces)
    {
        var culture = new CultureInfo("en-US");
        culture.NumberFormat.NumberDecimalSeparator = AppSettings.DecimalSeparator;
        return Math.Round(value, decimalPlaces).ToString(culture);
    }
    
    public static string Format(double number)
    {
        return Format(number, 0);
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

    public static string[] SplitOnWhitespaceOrTab(string line)
    {
        return line.Split(new[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);
    }
}