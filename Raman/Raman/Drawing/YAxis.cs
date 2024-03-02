namespace Raman.Drawing;

public class YAxis(CanvasCoordSystem coordSystem, Graphics graphics)
{
    private const int TICK_LINE_LENGTH = 5;
        
    private const int DISTANCE_FROM_TICK_TO_NUMBER = 2;
        
    private const double GAP_BETWEEN_TICKS = 50;

    public void Draw()
    {
        var x = coordSystem.LeftBorder;
        var y1 = coordSystem.TopBorder + coordSystem.PixelHeight;
        var y2 = coordSystem.TopBorder;
        graphics.DrawLine(AppConstants.COORD_SYSTEM_PEN, x, y1, x, y2);
        DrawNumbers();
    }

    private void DrawNumbers()
    {
        var gap = GetValueGap(coordSystem.ValueHeight, coordSystem.PixelHeight);
        var numbers = GetNumbers(coordSystem.MinY, coordSystem.MaxY, gap);
        numbers.ForEach(x => DrawNumberWithTick(x));
    }
        
    private void DrawNumberWithTick(decimal number)
    {
        var pos = coordSystem.ToPixelY(number);
        DrawTick(pos);
        DrawNumber(number, pos);
    }

    private void DrawNumber(decimal number, float pos)
    {
        var numberStr = number + "";
        var font = SystemFonts.DefaultFont;
        var numberX = coordSystem.ToPixelX(coordSystem.MinX) - TICK_LINE_LENGTH - DISTANCE_FROM_TICK_TO_NUMBER - GetDrawnStringLength(numberStr, font);
        var numberY = pos - GetDrawnStringHeight(font);
        graphics.DrawString(numberStr, font, Brushes.Black, numberX, numberY);
    }

    private static float GetDrawnStringHeight(Font font)
    {
        var ret = font.SizeInPoints;
        return ret;
    }

    private void DrawTick(float pos)
    {
        var x = coordSystem.ToPixelX(coordSystem.MinX);
        graphics.DrawLine(AppConstants.COORD_SYSTEM_PEN, x, pos, x - TICK_LINE_LENGTH, pos);
    }
        
    private float GetDrawnStringLength(string str, Font font)
    {
        var ret = graphics.MeasureString(str, font).Width;
        return ret;
    }

    private static List<decimal> GetNumbers(decimal min, decimal max, int gap)
    {
        var firstNumber = ((int) min / gap + 1) * gap;
        var lastNumber = ((int) max / gap) * gap;
        var ret = new List<decimal>();
        for (var number = firstNumber; number <= lastNumber; number += gap)
        {
            ret.Add(number);    
        }
        var isZeroVisible = min < 0 && 0 < max;
        if (isZeroVisible)
        {
            ret.Add(0);    
        }
        ret = ret.OrderBy(x => x).ToList();
        return ret;
    }

    /// <summary>
    /// Get reasonable value gap between numbers on axis - so the gap is 1 or 10 or 100. 
    /// </summary>
    private static int GetValueGap(float valueDistance, float pixelDistance)
    {
        var valueGap = valueDistance / pixelDistance * GAP_BETWEEN_TICKS;
        if (valueGap < 1)
        {
            return 1;
        } else if (valueGap < 10)
        {
            return 10;
        } else if (valueGap < 100)  
        {
            return 100;
        } else if (valueGap < 200)  
        {
            return 200;
        } else if (valueGap < 300)  
        {
            return 300;
        } else if (valueGap < 400)  
        {
            return 400;
        } 
        return 500;
    }
}