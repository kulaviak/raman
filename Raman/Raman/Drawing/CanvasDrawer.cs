using System.Drawing;
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

        public void DrawLine(Point point1, Point point2)
        {
            var point1X = _coordSystem.ToPixelX(point1.X);
            var point1Y = _coordSystem.ToPixelY(point1.Y);
            var point2X = _coordSystem.ToPixelX(point2.X);
            var point2Y = _coordSystem.ToPixelY(point2.Y);
            _graphics.DrawLine(Pens.Blue, point1X, point1Y, point2X, point2Y);
        }
    }
}