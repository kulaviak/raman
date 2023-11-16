using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Raman.Core
{
    public class Drawer
    {
        public void Draw(List<Chart> charts, Graphics graphics, int graphicsWidth, int graphicsHeight)
        {
            var allPoints = charts.SelectMany(x => x.Points).ToList();
            var minX = allPoints.Min(point => point.X);
            var maxX = allPoints.Max(point => point.X);
            var minY = allPoints.Min(point => point.Y);
            var maxY = allPoints.Max(point => point.Y);
            var canvas = new Canvas(graphics, graphicsWidth, graphicsHeight, minX, maxX, minY, maxY);
            foreach (var chart in charts)
            {
                chart.Draw(canvas);
            }
            canvas.DrawAxes();
        }       
    }
}