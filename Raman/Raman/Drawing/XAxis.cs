using System.Collections.Generic;
using System.Drawing;

namespace Raman.Drawing
{
    public class XAxis
    {
        private readonly Canvas _canvas;

        private static int TICK_LINE_LENGTH = 5;
        
        private static int DISTANCE_FROM_TICK_TO_NUMBER = 8;

        public XAxis(Canvas canvas)
        {
            _canvas = canvas;
        }

        public void Draw()
        {
            var x1 = _canvas.BORDER;
            var y1 = _canvas.BORDER + _canvas.PixelHeight;
            var x2 = _canvas.BORDER + _canvas.PixelWidth;
            var y2 = _canvas.BORDER + _canvas.PixelHeight;
            _canvas._graphics.DrawLine(Pens.Black, x1, y1, x2, y2);
            DrawXAxisNumbers();
        }

        private void DrawXAxisNumbers()
        {
            var gap = GetValueGap(_canvas.ValueWidth, _canvas.PixelWidth);
            var numbers = GetAxisNumbers(_canvas._minX, _canvas._maxX, gap);
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
            var pos = _canvas.ToGraphicsX(number);
            DrawXAxisTick(pos);
            var numberStr = number + "";
            var font = SystemFonts.DefaultFont;
            var x = pos - GetDrawnStringLength(numberStr, font) / 2;
            var y = _canvas.ToGraphicsY(_canvas._minY) + TICK_LINE_LENGTH + DISTANCE_FROM_TICK_TO_NUMBER;
            _canvas._graphics.DrawString(numberStr, font, Brushes.Black, x, y);   
        }

        private void DrawXAxisTick(float pos)
        {
            var y = _canvas.ToGraphicsY(_canvas._minY);
            _canvas._graphics.DrawLine(Pens.Black, pos, y, pos, y + TICK_LINE_LENGTH);
        }
        
        private float GetDrawnStringLength(string str, Font font)
        {
            var ret = _canvas._graphics.MeasureString(str, font).Width;
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
            var x1 = _canvas.BORDER;
            var y1 = _canvas.BORDER + _canvas.PixelHeight;
            var x2 = _canvas.BORDER;
            var y2 = _canvas.BORDER;
            _canvas._graphics.DrawLine(Pens.Black, x1, y1, x2, y2);
        }

    }
}