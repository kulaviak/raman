using System.Windows.Forms;

namespace Raman.Drawing
{
    public class StatusBarLayer : LayerBase
    {
        private readonly CanvasPanel _canvasPanel;
        
        public StatusBarLayer(CanvasCoordSystem coordSystem, CanvasPanel canvasPanel) : base(coordSystem)
        {
            _canvasPanel = canvasPanel;
        }

        public override void HandleMouseMove(object sender, MouseEventArgs e)
        {
            base.HandleMouseMove(sender, e);
        }
    }
}