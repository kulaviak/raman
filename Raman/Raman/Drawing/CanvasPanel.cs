using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Raman.Drawing
{
    public class CanvasPanel : Panel
    {
        
        private List<Chart> _charts = new List<Chart>();

        public List<Chart> Charts => _charts;
        
        public bool IsZooming { get; set; }
        
        private Point? _zoomStart;
        
        private Rectangle? _zoomRectangle;

        private Bitmap _buffer;

        private Graphics _bufferGraphics;

        private CanvasCoordSystem _coordSystem = null;
        
        public CanvasPanel()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Paint += HandlePaint;
            MouseDown += HandleMouseDown;
            MouseMove += HandleMouseMove;
            MouseUp += HandleMouseUp;
            Resize += HandleResize;
        }

        private void HandlePaint(object sender, PaintEventArgs e)
        {
            if (_buffer == null)
            {
                InitializeBuffer();
            }
            Draw(_bufferGraphics);
            // Copy the content of the off-screen buffer to the form's graphics
            e.Graphics.DrawImage(_buffer, 0, 0);
        }
        
        private void Draw(Graphics graphics)
        {
            graphics.Clear(Color.FromArgb(240, 240, 240));
            DrawCharts(_charts, graphics, Width, Height);
            if (IsZooming && _zoomRectangle != null)
            {
                graphics.DrawRectangle(Pens.Black, _zoomRectangle.Value);
            }
        }
        
        public void DrawCharts(List<Chart> charts, Graphics graphics, int graphicsWidth, int graphicsHeight)
        {
            var allPoints = charts.SelectMany(x => x.Points).ToList();
            var minX = allPoints.Min(point => point.X);
            var maxX = allPoints.Max(point => point.X);
            var minY = allPoints.Min(point => point.Y);
            var maxY = allPoints.Max(point => point.Y);
            var coordSystem = new CanvasCoordSystem(graphicsWidth, graphicsHeight, minX, maxX, minY, maxY);
            foreach (var chart in charts)
            { 
                chart.Draw(coordSystem, graphics);
            }
            new XAxis(coordSystem, graphics).Draw();
            new YAxis(coordSystem, graphics).Draw();
        }       
        
        private void InitializeBuffer()
        {
            _buffer = new Bitmap(Width, Height);
            _bufferGraphics = Graphics.FromImage(_buffer);
        }

        private void HandleResize(object sender, EventArgs e)
        {
            // Recreate the buffer when the form is resized
            if (_buffer != null)
            {
                _buffer.Dispose();
            }
            if (_bufferGraphics != null)
            {
                _bufferGraphics.Dispose();
            }
            InitializeBuffer();
            Refresh();
        }

        private void HandleMouseDown(object sender, MouseEventArgs e)
        {
            if (IsZooming && e.Button == MouseButtons.Left)
            {
                _zoomStart = e.Location;
            }
        }

        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            if (IsZooming && _zoomStart != null)
            {
                var x = Math.Min(_zoomStart.Value.X, e.X);
                var y = Math.Min(_zoomStart.Value.Y, e.Y);
                var width = Math.Abs(_zoomStart.Value.X - e.X);
                var height = Math.Abs(_zoomStart.Value.Y - e.Y);
                _zoomRectangle = new Rectangle(x, y, width, height);
                Refresh(); 
            }
        }

        private void HandleMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && IsZooming)
            {
                IsZooming = false;
                // var zoomedArea = new RectangleF(
                //     _zoomRectangle.X / (float) _mainPanel.Width,
                //     _zoomRectangle.Y / (float) _mainPanel.Height,
                //     _zoomRectangle.Width / (float) _mainPanel.Width,
                //     _zoomRectangle.Height / (float) _mainPanel.Height
                // );
                IsZooming = false;
                _zoomStart = null;
                _zoomRectangle = null;
                // ZoomToArea(zoomedArea);
            }
        }
    }
}