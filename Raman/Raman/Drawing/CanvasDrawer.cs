namespace Raman.Drawing;

public class CanvasDrawer
{
    private readonly CanvasCoordSystem coordSystem;
        
    private readonly Graphics graphics;

    public CanvasDrawer(CanvasCoordSystem coordSystem, Graphics graphics)
    {
        this.coordSystem = coordSystem;
        this.graphics = graphics;
    }

    public void DrawLine(ValuePoint point1, ValuePoint point2, Pen pen)
    {
        var point1X = coordSystem.ToPixelX(point1.X);
        var point1Y = coordSystem.ToPixelY(point1.Y);
        var point2X = coordSystem.ToPixelX(point2.X);
        var point2Y = coordSystem.ToPixelY(point2.Y);
        graphics.DrawLine(pen, point1X, point1Y, point2X, point2Y);
    }

    public void DrawLine(Line line, Pen pen)
    {
        DrawLine(line.Start, line.End, pen);
    }
        
    public void DrawLines(List<ValuePoint> points, Pen pen)
    {
        if (points.Count < 2)
        {
            throw new AppException("Spectrum has less than 2 points.");
        }
        for (var i = 0; i < points.Count - 1; i++)
        {
            var point1 = points[i];
            var point2 = points[i + 1];
            DrawLine(point1, point2, pen);
        }
    }
}