using Point = System.Drawing.Point;

namespace Raman.Drawing;

public class ZoomToWindowLayer : LayerBase
{
    private readonly CanvasPanel canvasPanel;
    
    private Point? zoomStart;
    
    private Rectangle? zoomRectangle;

    public ZoomToWindowLayer(CanvasCoordSystem coordSystem, CanvasPanel canvasPanel) : base(coordSystem)
    {
        this.canvasPanel = canvasPanel;
    }

    public override void HandleMouseMove(object sender, MouseEventArgs e)
    {
        if (zoomStart != null)
        {
            var x = Math.Min(zoomStart.Value.X, e.X);
            var y = Math.Min(zoomStart.Value.Y, e.Y);
            var width = Math.Abs(zoomStart.Value.X - e.X);
            // calculate selection window height to have same aspect ratio as panel => after zoom chart will not change
            var height = (int) (width * (canvasPanel.Height / (float) canvasPanel.Width));
            if (width != 0 && height != 0)
            {
                zoomRectangle = new Rectangle(x, y, width, height);
                canvasPanel.Refresh();
            }
        }
    }

    public override void Draw(Graphics graphics)
    {
        if (zoomRectangle != null)
        {
            graphics.DrawRectangle(Pens.Gray, zoomRectangle.Value);
        }
    }
    
    public override void HandleMouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left && zoomRectangle != null)
        {
            canvasPanel.CoordSystem = canvasPanel.GetCoordSystemForZoom(CoordSystem, zoomRectangle.Value);
            canvasPanel.UnsetZoomToWindowMode();
        }
    }
    
    public override void HandleMouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            zoomStart = e.Location;
            // canvasPanel.Refresh();
        }
    }
}