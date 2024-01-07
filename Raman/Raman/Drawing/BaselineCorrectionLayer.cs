using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Raman.Core;
using Point = Raman.Core.Point;

namespace Raman.Drawing
{
    public class BaselineCorrectionLayer : LayerBase
    {
        private static Color COLOR = Color.Red;
        
        public BaselineCorrectionLayer(CanvasCoordSystem coordSystem) : base(coordSystem) {}

        private List<Point> _points = new List<Point>();

        private static int MAX_PIXEL_DISTANCE = 20;
        
        public override void HandleMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var point = CoordSystem.ToValuePoint(e.Location.X, e.Location.Y);
                _points.Add(point);
            }
            else if (e.Button == MouseButtons.Middle)
            {
                var point = GetPointToRemove(e.Location);
                if (point != null)
                {
                    _points = _points.Where(x => x != point).ToList();
                }
            }
        }

        private Point GetPointToRemove(System.Drawing.Point pos)
        {
            var closestPoint = _points.MinByOrDefault(x => Util.GetPixelDistance(CoordSystem.ToPixelPoint(x), pos));
            var closestPointDistance = Util.GetPixelDistance(CoordSystem.ToPixelPoint(closestPoint), pos);
            if (closestPoint != null && closestPointDistance < MAX_PIXEL_DISTANCE)
            {
                return closestPoint;
            }
            return null;
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