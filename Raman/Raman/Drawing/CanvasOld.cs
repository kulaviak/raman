using System.Drawing;
using Point = Raman.Core.Point;

namespace Raman.Drawing
{
    public class CanvasOld
    {
        
        public readonly Graphics _graphics;

        public int GraphicsWidth { get; }

        public int GraphicsHeight { get; }

        public decimal MinX { get; }

        public decimal MaxX { get; }

        public decimal MinY { get; }

        public decimal MaxY { get; }

        public float Border => 50;

        public float PixelWidth => GraphicsWidth - 2 * Border;

        public float PixelHeight => GraphicsHeight - 2 * Border;

        public float ValueWidth => (float) (MaxX - MinX);

        public float ValueHeight => (float) (MaxY - MinY);

        public CanvasOld(Graphics graphics, int graphicsWidth, int graphicsHeight, decimal minX, decimal maxX, decimal minY, decimal maxY)
        {
            _graphics = graphics;
            GraphicsWidth = graphicsWidth;
            GraphicsHeight = graphicsHeight;
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
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
            var valueDistance = MaxY - y;
            var ret = Border + PixelHeight / ValueHeight * (float) valueDistance;
            return ret;
        }

        public float ToGraphicsX(decimal x)
        {
            var valueDistance = x - MinX;
            var ret = Border + PixelWidth / ValueWidth * (float) valueDistance;
            return ret;
        }
    }
}