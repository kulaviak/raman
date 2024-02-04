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
            // calculate selection window height to have same aspect ratio as panel => after zoom chart will not change
            var height = (int) (width * (canvasPanel.Height / (float) canvasPanel.Width));
            if (width != 0 && height != 0)
            {
                rectangle = new Rectangle(x, y, width, height);
                canvasPanel.Refresh();
            }
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
            canvasPanel.CoordSystem = canvasPanel.GetCoordSystemForZoom(CoordSystem, rectangle.Value);
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