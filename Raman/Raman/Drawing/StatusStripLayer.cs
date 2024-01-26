using System.Windows.Forms;
using Raman.Controls;

namespace Raman.Drawing
{
    public class StatusStripLayer : LayerBase
    {
        private readonly AppStatusStrip _statusStrip;

        public StatusStripLayer(CanvasCoordSystem coordSystem, AppStatusStrip statusStrip) : base(coordSystem)
        {
            _statusStrip = statusStrip;
        }

        public override void HandleMouseMove(object sender, MouseEventArgs e)
        {
            var position = CoordSystem.ToValuePoint(e.Location.X, e.Location.Y);
            _statusStrip.ShowPosition(position);
        }
    }
}