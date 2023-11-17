using System.Drawing;

namespace Raman.Drawing
{
    public class CanvasCoordinateSystem
    {
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

        public CanvasCoordinateSystem(int graphicsWidth, int graphicsHeight, decimal minX, decimal maxX, decimal minY, decimal maxY)
        {
            GraphicsWidth = graphicsWidth;
            GraphicsHeight = graphicsHeight;
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
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