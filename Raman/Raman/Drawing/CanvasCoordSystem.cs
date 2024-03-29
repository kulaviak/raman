namespace Raman.Drawing;

public class CanvasCoordSystem
{
    public int GraphicsWidth { get; }

    public int GraphicsHeight { get; }

    public double  MinX { get; }

    public double MaxX { get; }

    public double MinY { get; }

    public double MaxY { get; }

    // left and top border are higher because they need more space for axis values
    public float LeftBorder => 50;
        
    public float BottomBorder => 50;
        
    public float RightBorder => 20;
        
    public float TopBorder => 20;

    public float PixelWidth => GraphicsWidth - LeftBorder - RightBorder;

    public float PixelHeight => GraphicsHeight - TopBorder - BottomBorder;

    public float ValueWidth => (float) (MaxX - MinX);

    public float ValueHeight => (float) (MaxY - MinY);

    public CanvasCoordSystem(int graphicsWidth, int graphicsHeight, double minX, double maxX, double minY, double maxY)
    {
        GraphicsWidth = graphicsWidth;
        GraphicsHeight = graphicsHeight;
        MinX = minX;
        MaxX = maxX;
        MinY = minY;
        MaxY = maxY;
    }
        
    public float ToPixelX(double valueX)
    {
        var valueDistance = valueX - MinX;
        var ret = LeftBorder + PixelWidth / ValueWidth * (float) valueDistance;
        return ret;
    }
        
    public float ToPixelY(double valueY)
    {
        var valueDistance = MaxY - valueY;
        var ret = TopBorder + PixelHeight / ValueHeight * (float) valueDistance;
        return ret;
    }

    public double ToValueX(float pixelX)
    {
        var pixelDistance = pixelX - LeftBorder;
        var ret = ValueWidth / PixelWidth * pixelDistance + MinX;
        return ret;
    }
        
    public double ToValueY(float pixelY)
    {
        var pixelDistance = pixelY - TopBorder;
        var ret = MaxY - (ValueHeight / PixelHeight * pixelDistance);
        return ret;
    }
        
    public ValuePoint ToValuePoint(float pixelX, float pixelY)
    {
        var ret = new ValuePoint(ToValueX(pixelX), ToValueY(pixelY));
        return ret;
    }
    
    public ValuePoint ToValuePoint(Point point)
    {
        var ret = new ValuePoint(ToValueX(point.X), ToValueY(point.Y));
        return ret;
    }

    public override string ToString()
    {
        return $"MinX: {MinX}, MaxX: {MaxX}, MinY: {MinY}, MaxY: {MaxY}, PixelWidth: {PixelWidth}, PixelHeight: {PixelHeight}, " +
               $"ValueWidth: {ValueWidth}, ValueHeight: {ValueHeight}";
    }

    public Point ToPixelPoint(ValuePoint point)
    {
        var ret = new Point((int) ToPixelX(point.X), (int) ToPixelY(point.Y));
        return ret;
    }
}