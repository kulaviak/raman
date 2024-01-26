using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Raman.Core;
using Point = Raman.Core.Point;

namespace Raman.Drawing
{
    public class BaselineCorrectionLayer : LayerBase
    {
        private readonly CanvasPanel _canvasPanel;
        
        private static Color COLOR = Color.Red;
        
        private List<Point> _points = new List<Point>();

        public List<Point> Points => _points;
        
        public BaselineCorrectionLayer(CanvasCoordSystem coordSystem, CanvasPanel canvasPanel) : base(coordSystem)
        {
            _canvasPanel = canvasPanel;
        }
        
        public override void HandleMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var point = CoordSystem.ToValuePoint(e.Location.X, e.Location.Y);
                _points.Add(point);
            }
            else if (e.Button == MouseButtons.Middle)
            {
                RemoveClosestPoint(e.Location);
            }
            else if (e.Button == MouseButtons.Right)
            {
                ShowContextMenu(e.Location);
            }
        }
        
        public void Reset()
        {
            _points.Clear();
            _canvasPanel.Refresh();
        }
        
        public override void Draw(Graphics graphics)
        {
            DrawMarks(graphics);
        }

        private void RemoveClosestPoint(System.Drawing.Point location)
        {
            var point = GetPointToRemove(location);
            if (point != null)
            {
                _points = _points.Where(x => x != point).ToList();
            }
        }

        private void ShowContextMenu(System.Drawing.Point location)
        {
            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Remove Closest Point", null, (sender, e) => RemoveClosestPoint(location));
            contextMenu.Show(_canvasPanel, location);
        }
        
        private Point GetPointToRemove(System.Drawing.Point pos)
        {
            var closestPoint = _points.MinByOrDefault(x => Util.GetPixelDistance(CoordSystem.ToPixelPoint(x), pos));
            if (closestPoint != null)
            {
                return closestPoint;
            }
            return null;
        }
        
        private void DrawMarks(Graphics graphics)
        {
            foreach (var point in _points)
            {
                new Mark(CoordSystem, graphics, COLOR, point).Draw();
            }
        }

        public void ImportPoint(List<Point> points)
        {
            _points = points;
            _canvasPanel.Refresh();
        }

        public void ExportPoints(string filePath)
        {
            new OnePointPerLineFileWriter().WritePoints(_points, filePath);
        }

        public void DoBaselineCorrection()
        {
        }

        public void UndoBaselineCorrection()
        {
        }
    }
}