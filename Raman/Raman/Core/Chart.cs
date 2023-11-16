using System;
using System.Collections.Generic;
using System.Drawing;

namespace Raman.Core
{
    public class Chart : IDrawable
    {
        private readonly List<Point> _points;

        public Chart(List<Point> points)
        {
            _points = points;
        }

        public override string ToString()
        {
            return $"Points.Count: {_points.Count}";
        }

        public void Draw(Graphics graphics)
        {
            var pen = new Pen(Color.Black, 3);
            if (_points.Count < 2)
            {
                throw new Exception("Chart has less than 2 points.");
            }
            for (var i = 0; i < _points.Count - 1; i++)
            {
                var point1 = _points[i];
                var point2 = _points[i + 1];
                DrawLine(point1, point2, pen, graphics);
            }
        }

        private void DrawLine(Point point1, Point point2, Pen pen, Graphics graphics)
        {
            var point1X = ToGraphicsX(point1.X);
            var point1Y = ToGraphicsY(point1.Y);
            var point2X = ToGraphicsX(point2.X);
            var point2Y = ToGraphicsY(point2.Y);
            graphics.DrawLine(pen, point1X, point1Y, point2X, point2Y);
        }

        private int ToGraphicsY(decimal y)
        {
            throw new NotImplementedException();
        }

        private int ToGraphicsX(decimal x)
        {
            throw new NotImplementedException();
        }
    }
}