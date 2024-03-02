namespace Raman.Drawing;

public class ZeroYLevel(CanvasCoordSystem coordSystem, Graphics graphics)
{
    public void Draw()
    {
        var isVisible = coordSystem.MinY < 0 && 0 < coordSystem.MaxY;
        if (isVisible)
        {
            var x1 = coordSystem.LeftBorder;
            var y = coordSystem.ToPixelY(0);
            var x2 = coordSystem.LeftBorder + coordSystem.PixelWidth;
            graphics.DrawLine(Pens.DimGray, x1, y, x2, y);
        }
    }
}