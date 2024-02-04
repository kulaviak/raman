namespace Raman.Drawing;

public class CoordSystemCalculator
{
    private const int PIXEL_SPACE = 25;
    
    public static CanvasCoordSystem GetCoordSystemToShowAllCharts(IList<Chart> charts, int canvasWidth, int canvasHeight)
    {
        if (charts.Count == 0)
        {
            return null;
        }

        var allPoints = charts.SelectMany(x => x.Points).ToList();
        var minX = allPoints.Min(point => point.X);
        var maxX = allPoints.Max(point => point.X);
        var minY = allPoints.Min(point => point.Y);
        var maxY = allPoints.Max(point => point.Y);
        
        // modify displayed range so the chart is not going to axes, but there is a distance to X or Y axis
        // it is needed when baseline correction points are selected, I want to often select points that is beyond chart range
        var spaceX = (maxX - minX) / canvasWidth * PIXEL_SPACE;
        var spaceY = (maxY - minY) / canvasHeight * PIXEL_SPACE;
        minX -= spaceX;
        maxX += spaceX;
        minY -= spaceY;
        maxY += spaceY;
        
        var coordSystem = new CanvasCoordSystem(canvasWidth, canvasHeight, minX, maxX, minY, maxY);
        return coordSystem;
    }
}