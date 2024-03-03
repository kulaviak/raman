namespace Raman.Drawing;

public class MouseZoomLayer(CanvasPanel canvasPanel) : LayerBase(canvasPanel.CoordSystem)
{

    private const decimal ZOOM_RATIO = 0.8m;

    private Point? previousPosition;
    
    public override void HandleMouseMove(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Middle)
        {
            if (previousPosition == null)
            {
                previousPosition = e.Location;
            }
            else
            {
                if (previousPosition != e.Location)
                {
                    canvasPanel.CoordSystem = CoordSystemCalculator.GetCoordSystemForMouseDragging(CoordSystem, previousPosition.Value, e.Location);
                    canvasPanel.Refresh();
                    previousPosition = e.Location;
                }
            }
        }
    }

    public override void HandleMouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Middle)
        {
            previousPosition = null;
        }
    }

    public override void HandleMouseWheel(object sender, MouseEventArgs e)
    {
        var zoomIn = e.Delta > 0;
        var zoomRatio = zoomIn ? ZOOM_RATIO : 1 / ZOOM_RATIO;
        canvasPanel.CoordSystem = CoordSystemCalculator.GetCoordSystemForZoomMouseWheel(CoordSystem, e.Location, zoomRatio);
        canvasPanel.Refresh();
    }
}