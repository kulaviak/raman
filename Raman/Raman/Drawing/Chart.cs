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
            new CanvasDrawer(coordSystem, graphics).DrawLines(_points, Pens.Blue);
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