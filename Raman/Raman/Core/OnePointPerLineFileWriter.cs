using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Raman.Core
{
    public class OnePointPerLineFileWriter
    {
        public void WritePoints(List<Point> points, string filePath)
        {
            var lines = PointsToLines(points);
            File.WriteAllLines(filePath, lines);
        }

        private List<string> PointsToLines(List<Point> points)
        {
            var ret = points.Select(x => PointToLine(x)).ToList();
            return ret;
        }

        private string PointToLine(Point point)
        {
            return $"{point.X}\t{point.Y}";
        }
    }
}