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

        public override string ToString()
        {
            return $"Points.Count: {_points.Count}";
        }

        public void Draw(Graphics graphics)
        {
            var pen = new Pen(Color.Black, 3);
            foreach (var point in _points)
            {
                DrawPoint(point, pen, graphics);
            }
        }

        private void DrawPoint(Point point, Pen pen, Graphics graphics)
        {
            var x = point.X;
            var y = point.Y;
            graphics.DrawRectangle(pen, x, y, 1, 1);  
        }
    }
}