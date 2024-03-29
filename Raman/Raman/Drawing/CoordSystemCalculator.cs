namespace Raman.Drawing;

public class CoordSystemCalculator
{
    private const int PIXEL_SPACE = 25;
    
    private const double MIN_Y = 0;
    
    private const double MAX_Y = 1000;

    private const double MIN_X = 0;
    
    private const double MAX_X = 1000;
    
    public static CanvasCoordSystem GetCoordSystemToShowAllSpectra(IList<Spectrum> spectra, int canvasWidth, int canvasHeight)
    {
        if (spectra.Count == 0)
        {
            return GetDefaultCoordSystem(canvasWidth, canvasHeight);
        }

        var allPoints = spectra.SelectMany(x => x.Points).ToList();
        var minX = allPoints.Min(point => point.X);
        var maxX = allPoints.Max(point => point.X);
        var minY = allPoints.Min(point => point.Y);
        var maxY = allPoints.Max(point => point.Y);
        
        // modify displayed range so the spectrum is not going to axes, but there is a distance to X or Y axis
        // it is needed when baseline correction points are selected, I want to often select points that is beyond spectrum range
        var spaceX = (maxX - minX) / canvasWidth * PIXEL_SPACE;
        var spaceY = (maxY - minY) / canvasHeight * PIXEL_SPACE;
        minX -= spaceX;
        maxX += spaceX;
        minY -= spaceY;
        maxY += spaceY;
        
        var coordSystem = new CanvasCoordSystem(canvasWidth, canvasHeight, minX, maxX, minY, maxY);
        return coordSystem;
    }

    public static CanvasCoordSystem GetCoordSystemForZoom(CanvasCoordSystem oldCoordSystem, Rectangle zoomRectangle, int canvasWidth, int canvasHeight)
    {
        var minX = oldCoordSystem.ToValueX(zoomRectangle.X);
        var maxX = oldCoordSystem.ToValueX(zoomRectangle.X + zoomRectangle.Width);
        var minY = oldCoordSystem.ToValueY(zoomRectangle.Y + zoomRectangle.Height);
        var maxY = oldCoordSystem.ToValueY(zoomRectangle.Y);
        var ret = new CanvasCoordSystem(canvasWidth, canvasHeight, minX, maxX, minY, maxY);
        return ret;
    }

    public static CanvasCoordSystem GetDefaultCoordSystem(int canvasWidth, int canvasHeight)
    {
        return new CanvasCoordSystem(canvasWidth, canvasHeight, MIN_X, MAX_X, MIN_Y, MAX_Y);
    }

    public static CanvasCoordSystem GetCoordSystemForZoomMouseWheel(CanvasCoordSystem oldCoordSystem, Point point, double zoomRatio)
    {
        var valuePoint = oldCoordSystem.ToValuePoint(point);
        var minX = valuePoint.X - (valuePoint.X - oldCoordSystem.MinX) * zoomRatio;
        var maxX = valuePoint.X + (oldCoordSystem.MaxX - valuePoint.X) * zoomRatio;
        var minY = valuePoint.Y - (valuePoint.Y - oldCoordSystem.MinY) * zoomRatio;
        var maxY = valuePoint.Y + (oldCoordSystem.MaxY - valuePoint.Y) * zoomRatio;
        var ret = new CanvasCoordSystem(oldCoordSystem.GraphicsWidth, oldCoordSystem.GraphicsHeight, minX, maxX, minY, maxY);
        return ret;
    }
    
    public static CanvasCoordSystem GetCoordSystemForMouseDragging(CanvasCoordSystem oldCoordSystem, Point startPoint, Point endPoint)
    {
        var startValuePoint = oldCoordSystem.ToValuePoint(startPoint);
        var endValuePoint = oldCoordSystem.ToValuePoint(endPoint);
        var incrementX = endValuePoint.X - startValuePoint.X;
        var incrementY = endValuePoint.Y - startValuePoint.Y;
        // Increment is subtracted because, when user moves with mouse to the right it means, that he wants to see the left part of the spectrum => 
        // decrease minX, maxX. The same is for Y direction.
        var minX = oldCoordSystem.MinX - incrementX;
        var maxX = oldCoordSystem.MaxX - incrementX;
        var minY = oldCoordSystem.MinY - incrementY;
        var maxY = oldCoordSystem.MaxY - incrementY;
        var ret = new CanvasCoordSystem(oldCoordSystem.GraphicsWidth, oldCoordSystem.GraphicsHeight, minX, maxX, minY, maxY);
        return ret;
    }
}