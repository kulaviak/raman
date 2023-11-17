using System.Collections.Generic;
using System.Drawing;
using Point = Raman.Core.Point;

namespace Raman.Drawing
{
    public class Canvas
    {
        
        public readonly Graphics _graphics;

        public readonly int _graphicsWidth;

        public readonly int _graphicsHeight;

        public readonly decimal _minX;

        public readonly decimal _maxX;

        public readonly decimal _minY;

        public readonly decimal _maxY;

        public float BORDER = 50;

        public float PixelWidth => _graphicsWidth - 2 * BORDER;

        public float PixelHeight => _graphicsHeight - 2 * BORDER;

        public float ValueWidth => (float) (_maxX - _minX);

        public float ValueHeight => (float) (_maxY - _minY);

        public Canvas(Graphics graphics, int graphicsWidth, int graphicsHeight, decimal minX, decimal maxX, decimal minY, decimal maxY)
        {
            _graphics = graphics;
            _graphicsWidth = graphicsWidth;
            _graphicsHeight = graphicsHeight;
            _minX = minX;
            _maxX = maxX;
            _minY = minY;
            _maxY = maxY;
        }

        public void DrawLine(Point point1, Point point2)
        {
            var point1X = ToGraphicsX(point1.X);
            var point1Y = ToGraphicsY(point1.Y);
            var point2X = ToGraphicsX(point2.X);
            var point2Y = ToGraphicsY(point2.Y);
            _graphics.DrawLine(Pens.Blue, point1X, point1Y, point2X, point2Y);
        }
        
        public float ToGraphicsY(decimal y)
        {
            var valueDistance = _maxY - y;
            var ret = BORDER + PixelHeight / ValueHeight * (float) valueDistance;
            return ret;
        }

        public float ToGraphicsX(decimal x)
        {
            var valueDistance = x - _minX;
            var ret = BORDER + PixelWidth / ValueWidth * (float) valueDistance;
            return ret;
        }
    }
}