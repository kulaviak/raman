using Raman.Drawing;

namespace Raman.View;

public class MouseZoomLayer(CanvasPanel canvasPanel) : LayerBase(canvasPanel.CoordSystem)
{

    private const decimal ZOOM_RATIO = 0.8m;

    private Point? previousPosition;
    
    public override void HandleMouseMove(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Middle && !UserWantsDeletePoint())
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
        if (e.Button == MouseButtons.Middle && !UserWantsDeletePoint())
        {
            previousPosition = null;
        }
    }

    public override void HandleMouseWheel(object sender, MouseEventArgs e)
    {
        if (UserWantsDeletePoint())
        {
            return;
        }
        var zoomIn = e.Delta > 0;
        var zoomRatio = zoomIn ? ZOOM_RATIO : 1 / ZOOM_RATIO;
        canvasPanel.CoordSystem = CoordSystemCalculator.GetCoordSystemForZoomMouseWheel(CoordSystem, e.Location, zoomRatio);
        canvasPanel.Refresh();
    }
 
    // if user wants to delete point it will click middle button and press Ctrl Key => so the checks are added here to make sure, that if 
    // the user clicks and scrolls or moves accidentally then it will not zoom, because user actually wants to delete point
    private bool UserWantsDeletePoint()
    {
        return Util.IsCtrlKeyPressed();
    }
}