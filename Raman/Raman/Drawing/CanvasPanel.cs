using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Raman.Drawing
{
    public class CanvasPanel : Panel
    {
        
        private ReadOnlyCollection<Chart> _charts = new ReadOnlyCollection<Chart>(new List<Chart>());

        public ReadOnlyCollection<Chart> Charts
        {
            get => _charts;
            set
            {
                _charts = value;
                _coordSystem = GetCoordSystemFromCharts(_charts);
            }
        }

        public bool IsZooming { get; set; }
        
        private Point? _zoomStart;
        
        private Rectangle? _zoomRectangle;

        private Bitmap _buffer;

        private Graphics _bufferGraphics;

        private CanvasCoordSystem _coordSystem;

        public CanvasPanel()
        {
            InitializeComponent();
        }
        
        public void ZoomToOriginalSize()
        {
            _coordSystem = GetCoordSystemFromCharts(_charts);
            DoRefresh();
        }
        
        private void DoRefresh()
        {
            Invalidate();
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
            // copy the content of the off-screen buffer to the form's graphics
            e.Graphics.DrawImage(_buffer, 0, 0);
        }
        
        private void Draw(Graphics graphics)
        {
            graphics.Clear(Color.FromArgb(240, 240, 240));
            if (_coordSystem != null)
            {
                ClipGraphicsToOnlyChartArea(graphics);
                foreach (var chart in (IList<Chart>) _charts)
                { 
                    chart.Draw(_coordSystem, graphics);
                }
                graphics.ResetClip();
                DrawXAxis(graphics);
                DrawYAxis(graphics);
            }
            if (IsZooming && _zoomRectangle != null)
            {
                graphics.DrawRectangle(Pens.Gray, _zoomRectangle.Value);
            }
        }

        /// <summary>
        /// Clip only to chart area => nothing will be drawn outside of the axes.
        /// </summary>
        private void ClipGraphicsToOnlyChartArea(Graphics graphics)
        {
            var x = (int) _coordSystem.Border;
            var y = (int) _coordSystem.Border;
            var width = (int) (Width - 2 * _coordSystem.Border);
            var height = (int) (Height - 2 * _coordSystem.Border);
            var chartRectangle = new Rectangle(x, y, width, height);
            graphics.SetClip(chartRectangle);
        }

        private void DrawYAxis(Graphics graphics)
        {
            new YAxis(_coordSystem, graphics).Draw();
        }

        private void DrawXAxis(Graphics graphics)
        {
            new XAxis(_coordSystem, graphics).Draw();
        }

        private CanvasCoordSystem GetCoordSystemFromCharts(IList<Chart> charts)
        {
            if (charts.Count == 0)
            {
                return null;
            }
            var allPoints = charts.SelectMany(x => x.Points).ToList();
            var minX = allPoints.Min(point => point.X);
            var maxX = allPoints.Max(point => point.X);
            var minY = allPoints.Min(point => point.Y);
            var maxY = allPoints.Max(point => point.Y);
            var coordSystem = new CanvasCoordSystem(Width, Height, minX, maxX, minY, maxY);
            return coordSystem;
        }

        private void InitializeBuffer()
        {
            _buffer = new Bitmap(Width, Height);
            _bufferGraphics = Graphics.FromImage(_buffer);
        }

        private void HandleResize(object sender, EventArgs e)
        {
            _coordSystem = GetCoordSystemFromCharts(_charts);
            // recreate the buffer when the form is resized
            _buffer?.Dispose();
            if (_bufferGraphics != null)
            {
                _bufferGraphics.Dispose();
            }
            InitializeBuffer();
            DoRefresh();
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
                // calculate selection window height to have same aspect ratio as panel => after zoom chart will not change
                // (aspect ratio of chart will not change)
                var height = (int) (width * (Height / (float) Width));
                if (width != 0 && height != 0)
                {
                    _zoomRectangle = new Rectangle(x, y, width, height);
                    DoRefresh(); 
                }
            }
        }

        private void HandleMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && IsZooming && _zoomRectangle != null)
            {
                _coordSystem = GetCoordSystemForZoom(_coordSystem, _zoomRectangle.Value);
                DoRefresh();
                IsZooming = false;
                _zoomStart = null;
                _zoomRectangle = null;
            }
        }

        private CanvasCoordSystem GetCoordSystemForZoom(CanvasCoordSystem oldCoordSystem, Rectangle zoomRectangle)
        {
            var minX = oldCoordSystem.ToValueX(zoomRectangle.X);
            var maxX = oldCoordSystem.ToValueX(zoomRectangle.X + zoomRectangle.Width);
            var minY = oldCoordSystem.ToValueY(zoomRectangle.Y + zoomRectangle.Height);
            var maxY = oldCoordSystem.ToValueY(zoomRectangle.Y);
            var ret = new CanvasCoordSystem(Width, Height, minX, maxX, minY, maxY);
            return ret;
        }
    }
}