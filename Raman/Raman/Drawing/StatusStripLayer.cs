using Raman.Controls;

namespace Raman.Drawing;

public class StatusStripLayer : LayerBase
{
    private readonly AppStatusStrip statusStrip;

    public StatusStripLayer(CanvasCoordSystem coordSystem, AppStatusStrip statusStrip) : base(coordSystem)
    {
        this.statusStrip = statusStrip;
    }

    public override void HandleMouseMove(object sender, MouseEventArgs e)
    {
        var position = CoordSystem.ToValuePoint(e.Location.X, e.Location.Y);
        statusStrip.ShowPosition(position);
    }
}