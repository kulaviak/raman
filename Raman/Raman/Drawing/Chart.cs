using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using Point = Raman.Core.Point;

namespace Raman.Drawing
{
    [Serializable]
    public class Chart : ISerializable
    {
        private readonly List<Point> _points;

        public List<Point> Points => _points;

        public Chart(List<Point> points)
        {
            _points = points;
        }

        public void Draw(CanvasCoordSystem coordSystem, Graphics graphics)
        {
            if (_points.Count < 2)
            {
                throw new Exception("Chart has less than 2 points.");
            }
            var canvasDrawer = new CanvasDrawer(coordSystem, graphics);
            for (var i = 0; i < _points.Count - 1; i++)
            {
                var point1 = _points[i];
                var point2 = _points[i + 1];
                canvasDrawer.DrawLine(point1, point2);
            }
        }
        
        public override string ToString()
        {
            return $"Points.Count: {_points.Count}";
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            
        }
    }
}