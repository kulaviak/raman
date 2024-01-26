using System.Collections.Generic;
using System.Drawing;
using Raman.Core;
using Point = Raman.Core.Point;

namespace Raman.Drawing
{
    public class CanvasDrawer
    {
        private readonly CanvasCoordSystem _coordSystem;
        
        private readonly Graphics _graphics;

        public CanvasDrawer(CanvasCoordSystem coordSystem, Graphics graphics)
        {
            _coordSystem = coordSystem;
            _graphics = graphics;
        }

        public void DrawLine(Point point1, Point point2, Pen pen)
        {
            var point1X = _coordSystem.ToPixelX(point1.X);
            var point1Y = _coordSystem.ToPixelY(point1.Y);
            var point2X = _coordSystem.ToPixelX(point2.X);
            var point2Y = _coordSystem.ToPixelY(point2.Y);
            _graphics.DrawLine(pen, point1X, point1Y, point2X, point2Y);
        }
        
        public void DrawLines(List<Point> points, Pen pen)
        {
            if (points.Count < 2)
            {
                throw new AppException("Chart has less than 2 points.");
            }
            for (var i = 0; i < points.Count - 1; i++)
            {
                var point1 = points[i];
                var point2 = points[i + 1];
                DrawLine(point1, point2, pen);
            }
        }
    }
}