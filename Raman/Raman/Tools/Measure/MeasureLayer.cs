using Raman.Drawing;

namespace Raman.Tools.BaselineCorrection;

public class MeasureLayer : LayerBase
{
    private readonly CanvasPanel canvasPanel;

    private ValuePoint start;
    
    private ValuePoint end;
    
    private ValuePoint currentPoint;
    
    private static Pen PEN = Pens.Orange;

    public MeasureLayer(CanvasCoordSystem coordSystem, CanvasPanel canvasPanel) :
        base(coordSystem)
    {
        this.canvasPanel = canvasPanel;
    }

    public override void HandleMouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            // start over again
            if (start != null && end != null)
            {
                start = null;
                end = null;
            }
            if (start == null)
            {
                start = CoordSystem.ToValuePoint(e.Location.X, e.Location.Y);
            }
            else
            {
                end = CoordSystem.ToValuePoint(e.Location.X, e.Location.Y);
            }
            Refresh();
        }
    }
    
    public override void HandleMouseMove(object sender, MouseEventArgs e)
    {
        currentPoint = CoordSystem.ToValuePoint(e.Location.X, e.Location.Y);
        canvasPanel.Refresh();
    }
    
    public string GetDistanceText()
    {
        var currentEnd = GetCurrentEnd();
        if (start != null && currentEnd != null)
        {
            var xDistance = Math.Abs(start.X - currentEnd.X);
            var yDistance = Math.Abs(start.Y - currentEnd.Y);
            var xStr = Util.Format(xDistance, AppSettings.XDecimalPlaces);
            var yStr = Util.Format(yDistance, AppSettings.YDecimalPlaces);
            return $"Distance [{xStr}, {yStr}]. Press Escape to cancel Measure mode.";
        }
        else
        {
            return null;
        }
    }
    
    public override void Draw(Graphics graphics)
    {
        DrawLine(graphics);
    }
    
    private void DrawLine(Graphics graphics)
    {
        if (start != null)
        {
            var currentEnd = GetCurrentEnd();
            if (currentEnd != null)
            {
                new CanvasDrawer(CoordSystem, graphics).DrawLine(start, currentEnd, PEN);
            }
        }
    }

    private ValuePoint GetCurrentEnd()
    {
        return end != null ? end : currentPoint;
    }
    
    private void Refresh()
    {
        canvasPanel.Refresh();
    }
}