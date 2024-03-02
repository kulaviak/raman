using Point = System.Drawing.Point;

namespace Raman.Drawing;

public class ZoomToWindowLayer : LayerBase
{
    private readonly CanvasPanel canvasPanel;
    
    private Point? start;
    
    private Rectangle? rectangle;

    public ZoomToWindowLayer(CanvasCoordSystem coordSystem, CanvasPanel canvasPanel) : base(coordSystem)
    {
        this.canvasPanel = canvasPanel;
    }

    public override void HandleMouseMove(object sender, MouseEventArgs e)
    {
        if (start != null)
        {
            var x = Math.Min(start.Value.X, e.X);
            var y = Math.Min(start.Value.Y, e.Y);
            var width = Math.Abs(start.Value.X - e.X);
            var height = Math.Abs(start.Value.Y - e.Y);
            rectangle = new Rectangle(x, y, width, height);
            canvasPanel.Refresh();
        }
    }

    public override void Draw(Graphics graphics)
    {
        if (rectangle != null)
        {
            graphics.DrawRectangle(Pens.Gray, rectangle.Value);
        }
    }
    
    public override void HandleMouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left && rectangle != null)
        {
            canvasPanel.CoordSystem = CoordSystemCalculator.GetCoordSystemForZoom(CoordSystem, rectangle.Value, canvasPanel.Width, canvasPanel.Height);
            canvasPanel.UnsetZoomToWindowMode();
        }
    }
    
    public override void HandleMouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            start = e.Location;
        }
    }
}