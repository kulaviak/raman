using Point = Raman.Core.Point;

namespace Raman.Drawing;

public class Mark
{
    private readonly CanvasCoordSystem _coordSystem;
        
    private readonly Graphics _graphics;
        
    private readonly Color _color;
        
    private readonly Point _point;

    private const int RADIUS = 5;
    
    private static int MARK_THICKNESS = 2;

    public Mark(CanvasCoordSystem coordSystem, Graphics graphics, Color color, Point point)
    {
        _coordSystem = coordSystem;
        _graphics = graphics;
        _color = color;
        _point = point;
    }

    public void Draw()
    {
        var pixelX = _coordSystem.ToPixelX(_point.X);
        var pixelY = _coordSystem.ToPixelY(_point.Y);
        DrawHorizontalLine(pixelX, pixelY);
        DrawVerticalLine(pixelX, pixelY);
    }

    private void DrawVerticalLine(float pixelX, float pixelY)
    {
        var pen = new Pen(_color, MARK_THICKNESS); 
        _graphics.DrawLine(pen, pixelX, pixelY - RADIUS, pixelX, pixelY + RADIUS);
    }

    private void DrawHorizontalLine(float pixelX, float pixelY)
    {
        var pen = new Pen(_color, MARK_THICKNESS); 
        _graphics.DrawLine(pen, pixelX - RADIUS, pixelY, pixelX + RADIUS, pixelY);
    }
}