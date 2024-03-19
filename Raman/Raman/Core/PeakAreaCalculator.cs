namespace Raman.Core;

public class PeakAreaCalculator
{

    private static int NUMBER_OF_RECTANGLES = 1000;
    
    /// <summary>
    /// Calculate peak area by dividing into many rectangles filling the area under the peak. It is the  principle of numerical integration. 
    /// </summary>
    public double CalculateArea(Peak peak)
    {
        var dx = (peak.End.X - peak.Start.X) / NUMBER_OF_RECTANGLES;
        var dy = (peak.End.Y - peak.Start.Y) / NUMBER_OF_RECTANGLES;
        double ret = 0;
        // moving along peak bottom line by dx and dy
        for (var i = 0; i < NUMBER_OF_RECTANGLES - 1; i++)
        {
            var bottomX = peak.Start.X + i * dx;
            var bottomY = peak.Start.Y + i * dy;
            var topY = peak.Chart.GetValue(bottomX).Value;
            var subArea = (topY - bottomY) * dx;
            // small parts that are bellow bottom line are ignored
            if (subArea > 0)
            {
                ret += subArea;
            }
        }
        return ret;
    }
}