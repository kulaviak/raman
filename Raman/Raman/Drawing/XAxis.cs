namespace Raman.Drawing;

public class XAxis
{
    private readonly CanvasCoordSystem coordSystem;
        
    private readonly Graphics graphics;

    private const int TICK_LINE_LENGTH = 5;
        
    private const int DISTANCE_FROM_TICK_TO_NUMBER = 8;

    public XAxis(CanvasCoordSystem coordSystem, Graphics graphics)
    {
        this.coordSystem = coordSystem;
        this.graphics = graphics;
    }

    public void Draw()
    {
        var x1 = coordSystem.LeftBorder;
        var y1 = coordSystem.TopBorder + coordSystem.PixelHeight;
        var x2 = coordSystem.LeftBorder + coordSystem.PixelWidth;
        var y2 = coordSystem.TopBorder + coordSystem.PixelHeight;
        graphics.DrawLine(Pens.Black, x1, y1, x2, y2);
        DrawNumbers();
    }

    private void DrawNumbers()
    {
        var gap = GetValueGap(coordSystem.ValueWidth, coordSystem.PixelWidth);
        var numbers = GetNumbers(coordSystem.MinX, coordSystem.MaxX, gap);
        numbers.ForEach(x => DrawNumberWithTicks(x));
    }
        
    private void DrawNumberWithTicks(decimal number)
    {
        var x = coordSystem.ToPixelX(number);
        DrawTick(x);
        DrawNumber(number, x);
    }

    private void DrawNumber(decimal number, float pos)
    {
        var numberStr = number + "";
        var font = SystemFonts.DefaultFont;
        var numberX = pos - GetDrawnStringLength(numberStr, font) / 2;
        var numberY = coordSystem.ToPixelY(coordSystem.MinY) + TICK_LINE_LENGTH + DISTANCE_FROM_TICK_TO_NUMBER;
        graphics.DrawString(numberStr, font, Brushes.Black, numberX, numberY);
    }

    private void DrawTick(float pos)
    {
        var y = coordSystem.ToPixelY(coordSystem.MinY);
        graphics.DrawLine(Pens.Black, pos, y, pos, y + TICK_LINE_LENGTH);
    }
        
    private float GetDrawnStringLength(string str, Font font)
    {
        var ret = graphics.MeasureString(str, font).Width;
        return ret;
    }

    private static List<decimal> GetNumbers(decimal min, decimal max, int gap)
    {
        var firstNumber = ((int) min / gap + 1) * gap;
        var lastNumber = (int) max / gap * gap;
        var ret = new List<decimal>();
        for (var number = firstNumber; number <= lastNumber; number += gap)
        {
            ret.Add(number);    
        }
        return ret;
    }

    /// <summary>
    /// Get reasonable value gap between numbers on axis - so the gap is 1 or 10 or 100. 
    /// </summary>
    private static int GetValueGap(float valueDistance, float pixelDistance)
    {
        var pixelGap = 50;
        var valueGap = valueDistance / pixelDistance * pixelGap;
        if (valueGap < 1)
        {
            return 1;
        } else if (valueGap < 10)
        {
            return 10;
        } else if (valueGap < 100)
        {
            return 100;
        }
        return 100;
    }
}