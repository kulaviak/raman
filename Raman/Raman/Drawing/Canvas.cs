using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Raman.Drawing
{
    public class Canvas : Panel
    {
        
        private List<Chart> _charts = new List<Chart>();

        public List<Chart> Charts => _charts;
        
        public bool IsZooming { get; set; }
        
        private Point? _zoomStart;
        
        private Rectangle? _zoomRectangle;

        private Bitmap _buffer;

        private Graphics _bufferGraphics;
        
        public Canvas()
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
            new Drawer().Draw(_charts, graphics, Width, Height);
            if (IsZooming && _zoomRectangle != null)
            {
                graphics.DrawRectangle(Pens.Black, _zoomRectangle.Value);
            }
        }
        
        private void InitializeBuffer()
        {
            _buffer = new Bitmap(ClientSize.Width, ClientSize.Height);
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