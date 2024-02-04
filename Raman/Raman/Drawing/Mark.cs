using Point = Raman.Core.Point;

namespace Raman.Drawing;

public class Mark
{
    private readonly CanvasCoordSystem coordSystem;
        
    private readonly Graphics graphics;
        
    private readonly Color color;
        
    private readonly Point point;

    private const int RADIUS = 8;
    
    private const int THICKNESS = 1;

    public Mark(CanvasCoordSystem coordSystem, Graphics graphics, Color color, Point point)
    {
        this.coordSystem = coordSystem;
        this.graphics = graphics;
        this.color = color;
        this.point = point;
    }

    public void Draw()
    {
        var pixelX = coordSystem.ToPixelX(point.X);
        var pixelY = coordSystem.ToPixelY(point.Y);
        DrawHorizontalLine(pixelX, pixelY);
        DrawVerticalLine(pixelX, pixelY);
    }

    private void DrawVerticalLine(float pixelX, float pixelY)
    {
        var pen = new Pen(color, THICKNESS); 
        graphics.DrawLine(pen, pixelX, pixelY - RADIUS, pixelX, pixelY + RADIUS);
    }

    private void DrawHorizontalLine(float pixelX, float pixelY)
    {
        var pen = new Pen(color, THICKNESS); 
        graphics.DrawLine(pen, pixelX - RADIUS, pixelY, pixelX + RADIUS, pixelY);
    }
}