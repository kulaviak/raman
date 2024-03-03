namespace Raman.Drawing;

public class MouseZoomLayer(CanvasPanel canvasPanel) : LayerBase(canvasPanel.CoordSystem)
{
    public override void HandleMouseMove(object sender, MouseEventArgs e)
    {
    }

    public override void HandleMouseWheel(object sender, MouseEventArgs e)
    {
        var zoomIn = e.Delta > 0;
        canvasPanel.CoordSystem = CoordSystemCalculator.ZoomOnPoint(CoordSystem, e.Location, zoomIn);
        canvasPanel.Refresh();
    }
}