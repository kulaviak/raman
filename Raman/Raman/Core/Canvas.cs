using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Raman.Core
{
    /// <summary>
    /// Core class for drawing all the stuff. It puts together objects that should be drawn and the panel on which are drawn. Objects
    /// are drawn by PanelPaint method.
    /// </summary>
    public class Canvas
    {
        private readonly Panel _panel;

        public int X_BORDER = 50;
        
        public int Y_BORDER = 50;

        public int PixelWidth => _panel.Width - 2 * X_BORDER;

        public int PixelHeight => _panel.Height - 2 * Y_BORDER;

        public decimal ValueWidth => MaxX - MinX;

        public decimal ValueHeight => MaxY - MinY;
        
        public decimal MinX { get; set; }
        
        public decimal MaxX { get; set; }
        
        public decimal MinY { get; set; }
        
        public decimal MaxY { get; set; }
        
        public List<Chart> Charts { get; set; } = new List<Chart>();
        
        public Canvas(Panel panel)
        {
            _panel = panel;
            _panel.Paint += PanelPaint;
        }

        public void DrawPoint(Point point, Color color)
        {
            var x = ((point.X - MinX) / ValueWidth) * PixelWidth + MinX;
            var y = ((point.Y - MinY) / ValueHeight) * PixelHeight; //
            // DrawPixelPoint();
        }
        
        private void PanelPaint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            DrawCharts(graphics);
        }

        private void DrawCharts(Graphics graphics)
        {
            foreach (var chart in Charts)
            {
                chart.Draw(graphics);
            }
        }
    }
}