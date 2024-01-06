using System.Drawing;
using System.Windows.Forms;

namespace Raman.Drawing
{
    public class LayerBase
    {
        public CanvasCoordSystem CoordSystem { get; set; }
        
        public LayerBase(CanvasCoordSystem coordSystem)
        {
            CoordSystem = coordSystem;
        }
        
        public virtual void HandleMouseMove(object sender, MouseEventArgs e) {}

        public virtual void HandleMouseDown(object sender, MouseEventArgs e) {}

        public virtual void HandleMouseUp(object sender, MouseEventArgs e) {}

        public virtual void Draw(Graphics graphics) {}
    }
}