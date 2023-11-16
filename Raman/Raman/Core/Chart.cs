using System;
using System.Collections.Generic;
using System.Drawing;

namespace Raman.Core
{
    public class Chart : IDrawable
    {
        private readonly List<Point> _points;

        public List<Point> Points => _points;

        public Chart(List<Point> points)
        {
            _points = points;
        }

        public void Draw(Canvas canvas)
        {
            if (_points.Count < 2)
            {
                throw new Exception("Chart has less than 2 points.");
            }
            for (var i = 0; i < _points.Count - 1; i++)
            {
                var point1 = _points[i];
                var point2 = _points[i + 1];
                canvas.DrawLine(point1, point2);
            }
        }
        
        public override string ToString()
        {
            return $"Points.Count: {_points.Count}";
        }
    }
}