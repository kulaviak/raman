using Point = System.Drawing.Point;

namespace Raman.Drawing;

public class ZoomToWindowLayer : LayerBase
{
    private readonly CanvasPanel canvasPanel;
    
    private Point? _zoomStart;
    
    private Rectangle? _zoomRectangle;

    public ZoomToWindowLayer(CanvasCoordSystem coordSystem, CanvasPanel canvasPanel) : base(coordSystem)
    {
        this.canvasPanel = canvasPanel;
    }

    public override void HandleMouseMove(object sender, MouseEventArgs e)
    {
        if (_zoomStart != null)
        {
            var x = Math.Min(_zoomStart.Value.X, e.X);
            var y = Math.Min(_zoomStart.Value.Y, e.Y);
            var width = Math.Abs(_zoomStart.Value.X - e.X);
            // calculate selection window height to have same aspect ratio as panel => after zoom chart will not change
            var height = (int) (width * (canvasPanel.Height / (float) canvasPanel.Width));
            if (width != 0 && height != 0)
            {
                _zoomRectangle = new Rectangle(x, y, width, height);
                canvasPanel.Refresh();
            }
        }
    }

    public override void Draw(Graphics graphics)
    {
        if (_zoomRectangle != null)
        {
            graphics.DrawRectangle(Pens.Gray, _zoomRectangle.Value);
        }
    }
    
    public override void HandleMouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left && _zoomRectangle != null)
        {
            canvasPanel.CoordSystem = canvasPanel.GetCoordSystemForZoom(CoordSystem, _zoomRectangle.Value);
            canvasPanel.UnsetZoomToWindowMode();
        }
    }
    
    public override void HandleMouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            _zoomStart = e.Location;
            // canvasPanel.Refresh();
        }
    }
}