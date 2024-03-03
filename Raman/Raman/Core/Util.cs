using System.Globalization;
using Raman.Drawing;

namespace Raman.Core;

public abstract class Util
{
    public static float GetPixelDistance(System.Drawing.Point point1, System.Drawing.Point point2)
    {
        var x = Math.Abs(point1.X - point2.X);
        var y = Math.Abs(point1.Y - point2.Y);
        var ret = (float) Math.Sqrt(x*x + y*y);
        return ret;
    }
    
    public static decimal GetDistance(ValuePoint point1, ValuePoint point2)
    {
        var x = (double) Math.Abs(point1.X - point2.X);
        var y = (double) Math.Abs(point1.Y - point2.Y);
        var ret = (decimal) Math.Sqrt(x*x + y*y);
        return ret;
    }
        
    /// <summary>
    /// Universal decimal parser. Decimal delimiter can be both '.' and ','
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static Decimal? UniversalParseDecimal(string str)
    {
        if (str.IsNullOrWhiteSpace())
        {
            return null;
        }
        str = str.Replace(",", ".");
        var style = NumberStyles.Number;
        var culture = CultureInfo.CreateSpecificCulture("en-US");
        if (Decimal.TryParse(str, style, culture, out var ret))
        {
            return ret;
        }
        else
        {
            throw new AppException($"Parsing string {str} as number failed.");
        }
    }

    public static string Format(decimal value, int decimalPlaces)
    {
        return Math.Round(value, decimalPlaces).ToString();
    }

    public static Chart GetClosestChart(List<Chart> charts, ValuePoint point, decimal maximumDistance = decimal.MaxValue)
    {
        var ret = charts.Where(chart => GetClosestDistance(chart, point) <= maximumDistance).MinByOrDefault(chart => GetClosestDistance(chart, point));
        return ret;
    }

    private static decimal GetClosestDistance(Chart chart, ValuePoint point)
    {
        var ret = chart.Points.Min(chartPoint => GetDistance(chartPoint, point));
        return ret;
    }
    
    public static string Format(decimal number)
    {
        return Format(number, AppConstants.EXPORT_DECIMAL_PLACES);
    }
}