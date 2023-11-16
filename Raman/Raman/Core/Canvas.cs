using System.Drawing;

namespace Raman.Core
{
    /// <summary>
    /// Core class for drawing all the stuff. It puts together objects that should be drawn and the panel on which are drawn. Objects
    /// are drawn by PanelPaint method.
    /// </summary>
    public class Canvas
    {
        private readonly Graphics _graphics;

        private readonly int _graphicsWidth;

        private readonly int _graphicsHeight;

        private readonly decimal _minX;

        private readonly decimal _maxX;

        private readonly decimal _minY;

        private readonly decimal _maxY;

        private static float BORDER = 50;

        public float PixelWidth => _graphicsWidth - 2 * BORDER;

        public float PixelHeight => _graphicsHeight - 2 * BORDER;

        public decimal ValueWidth => _maxX - _minX;

        public decimal ValueHeight => _maxY - _minY;

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

        public void DrawAxes()
        {
            DrawXAxis();
            DrawYAxis();
        }

        private void DrawXAxis()
        {
            var x1 = BORDER;
            var y1 = BORDER + PixelHeight;
            var x2 = BORDER + PixelWidth;
            var y2 = BORDER + PixelHeight;
            _graphics.DrawLine(Pens.Black, x1, y1, x2, y2);
        }

        void DrawYAxis()
        {
            var x1 = BORDER;
            var y1 = BORDER + PixelHeight;
            var x2 = BORDER;
            var y2 = BORDER;
            _graphics.DrawLine(Pens.Black, x1, y1, x2, y2);
        }

        private float ToGraphicsY(decimal y)
        {
            var ret = BORDER + PixelHeight / (double) ValueHeight * (double) (ValueHeight - y);
            return (float) ret;
        }

        private float ToGraphicsX(decimal x)
        {
            var ret = BORDER + (float) (((decimal) (PixelWidth / (double) ValueWidth)) * x);
            return ret;
        }
    }
}