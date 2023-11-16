using System.Collections.Generic;
using System.Drawing;
using Point = Raman.Core.Point;

namespace Raman.Drawing
{
    public class Canvas
    {

        private static int TICK_LINE_LENGTH = 5;
        
        private static int DISTANCE_FROM_TICK_TO_NUMBER = 8;
        
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
            DrawXAxisNumbers();
        }

        private void DrawXAxisNumbers()
        {
            var gap = GetValueGap(ValueWidth, PixelWidth);
            var numbers = GetAxisNumbers(_minX, _maxX, gap);
            DrawXAxisNumbersInternal(numbers);
        }

        private void DrawXAxisNumbersInternal(List<decimal> numbers)
        {
            foreach (var number in numbers)
            {
                DrawXAxisNumber(number);
            }
        }

        private void DrawXAxisNumber(decimal number)
        {
            var pos = ToGraphicsX(number);
            DrawXAxisTick(pos);
            var numberStr = number + "";
            var font = SystemFonts.DefaultFont;
            var x = pos - GetDrawnStringLength(numberStr, font) / 2;
            var y = ToGraphicsY(_minY) + TICK_LINE_LENGTH + DISTANCE_FROM_TICK_TO_NUMBER;
            _graphics.DrawString(numberStr, font, Brushes.Black, x, y);   
        }

        private void DrawXAxisTick(float pos)
        {
            var y = ToGraphicsY(_minY);
            _graphics.DrawLine(Pens.Black, pos, y, pos, y + TICK_LINE_LENGTH);
        }
        
        private float GetDrawnStringLength(string str, Font font)
        {
            var ret = _graphics.MeasureString(str, font).Width;
            return ret;
        }

        private List<decimal> GetAxisNumbers(decimal min, decimal max, int gap)
        {
            var firstNumber = ((int) min / gap + 1) * gap;
            var lastNumber = (int) max / gap * gap;
            var ret = new List<decimal>();
            for (var number = firstNumber; number <= lastNumber; number += gap)
            {
                ret.Add(number);    
            }
            return ret;
        }

        /// <summary>
        /// Get reasonable value gap between numbers on axis - so the gap is 1 or 10 or 100. 
        /// </summary>
        private int GetValueGap(float valueDistance, float pixelDistance)
        {
            var pixelGap = 50;
            var valueGap = valueDistance / pixelDistance * pixelGap;
            if (valueGap < 1)
            {
                return 1;
            } else if (valueGap < 10)
            {
                return 10;
            } else if (valueGap < 100)
            {
                return 100;
            }
            return 100;
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
            var valueDistance = _maxY - y;
            var ret = BORDER + PixelHeight / ValueHeight * (float) valueDistance;
            return ret;
        }

        private float ToGraphicsX(decimal x)
        {
            var valueDistance = x - _minX;
            var ret = BORDER + PixelWidth / ValueWidth * (float) valueDistance;
            return ret;
        }
    }
}