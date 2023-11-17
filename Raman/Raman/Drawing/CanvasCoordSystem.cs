namespace Raman.Drawing
{
    public class CanvasCoordSystem
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

        public CanvasCoordSystem(int graphicsWidth, int graphicsHeight, decimal minX, decimal maxX, decimal minY, decimal maxY)
        {
            GraphicsWidth = graphicsWidth;
            GraphicsHeight = graphicsHeight;
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
        }
        
        public float ToPixelX(decimal valueX)
        {
            var valueDistance = valueX - MinX;
            var ret = Border + PixelWidth / ValueWidth * (float) valueDistance;
            return ret;
        }
        
        public float ToPixelY(decimal valueY)
        {
            var valueDistance = MaxY - valueY;
            var ret = Border + PixelHeight / ValueHeight * (float) valueDistance;
            return ret;
        }

        public decimal ToValueX(float pixelX)
        {
            var pixelDistance = pixelX - Border;
            var ret = ValueWidth / PixelWidth * pixelDistance;
            return (decimal) ret;
        }
        
        public decimal ToValueY(float pixelY)
        {
            var pixelDistance = pixelY - Border;
            var ret = MaxY - (decimal) (ValueHeight / PixelHeight * pixelDistance);
            return ret;
        }
    }
}