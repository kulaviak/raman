using Point = Raman.Core.Point;

namespace Raman.Drawing;

public class CanvasCoordSystem
{
    public int GraphicsWidth { get; }

    public int GraphicsHeight { get; }

    public decimal MinX { get; }

    public decimal MaxX { get; }

    public decimal MinY { get; }

    public decimal MaxY { get; }

    // left and top border are higher because they need more space for axis values
    public float LeftBorder => 50;
        
    public float BottomBorder => 50;
        
    public float RightBorder => 20;
        
    public float TopBorder => 20;

    public float PixelWidth => GraphicsWidth - LeftBorder - RightBorder;

    public float PixelHeight => GraphicsHeight - TopBorder - BottomBorder;

    public float ValueWidth => (float) (MaxX - MinX);

    public float ValueHeight => (float) (MaxY - MinY);

    public CanvasCoordSystem(int graphicsWidth, int graphicsHeight, decimal minX, decimal maxX, decimal minY, decimal maxY)
    {
        GraphicsWidth = graphicsWidth;
        GraphicsHeight = graphicsHeight;
        MinX = minX;
        MaxX = maxX;
        MinY = minY;
        MaxY = maxY;
    }
        
    public float ToPixelX(decimal valueX)
    {
        var valueDistance = valueX - MinX;
        var ret = LeftBorder + PixelWidth / ValueWidth * (float) valueDistance;
        return ret;
    }
        
    public float ToPixelY(decimal valueY)
    {
        var valueDistance = MaxY - valueY;
        var ret = TopBorder + PixelHeight / ValueHeight * (float) valueDistance;
        return ret;
    }

    public decimal ToValueX(float pixelX)
    {
        var pixelDistance = pixelX - LeftBorder;
        var ret = ValueWidth / PixelWidth * pixelDistance + (double) MinX;
        return (decimal) ret;
    }
        
    public decimal ToValueY(float pixelY)
    {
        var pixelDistance = pixelY - TopBorder;
        var ret = MaxY - (decimal) (ValueHeight / PixelHeight * pixelDistance);
        return ret;
    }
        
    public Point ToValuePoint(float pixelX, float pixelY)
    {
        var ret = new Point(ToValueX(pixelX), ToValueY(pixelY));
        return ret;
    }

    public override string ToString()
    {
        return $"MinX: {MinX}, MaxX: {MaxX}, MinY: {MinY}, MaxY: {MaxY}, PixelWidth: {PixelWidth}, PixelHeight: {PixelHeight}, " +
               $"ValueWidth: {ValueWidth}, ValueHeight: {ValueHeight}";
    }

    public System.Drawing.Point ToPixelPoint(Point point)
    {
        var ret = new System.Drawing.Point((int) ToPixelX(point.X), (int) ToPixelY(point.Y));
        return ret;
    }
}