using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Point = Raman.Core.Point;

namespace Raman.Drawing
{
    public class BaselineCorrectionLayer : LayerBase
    {
        private static Color COLOR = Color.Red;
        
        public BaselineCorrectionLayer(CanvasCoordSystem coordSystem) : base(coordSystem) {}

        private List<Point> _points = new List<Point>();
        
        public override void HandleMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var point = CoordSystem.ToValuePoint(e.Location.X, e.Location.Y);
                _points.Add(point);
            }
        }

        public override void Draw(Graphics graphics)
        {
            DrawMarks(graphics);
        }

        private void DrawMarks(Graphics graphics)
        {
            foreach (var point in _points)
            {
                new Mark(CoordSystem, graphics, COLOR, point).Draw();
            }
        }
    }
}