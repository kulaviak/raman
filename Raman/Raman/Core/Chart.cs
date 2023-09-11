using System.Collections.Generic;

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

        public void Draw(Canvas canvas)
        {
            throw new System.NotImplementedException();
        }
    }
}