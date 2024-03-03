using Raman.Controls;

namespace Raman.Drawing;

public class StatusStripLayer(CanvasCoordSystem coordSystem, AppStatusStrip statusStrip) : LayerBase(coordSystem)
{
    public override void HandleMouseMove(object sender, MouseEventArgs e)
    {
        var position = CoordSystem.ToValuePoint(e.Location.X, e.Location.Y);
        statusStrip.ShowPosition(position);
    }
}