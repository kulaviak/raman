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
        
        private List<Point> _correctionPoints = new List<Point>();
        
        private List<Chart> _oldCharts;

        public BaselineCorrectionLayer(CanvasCoordSystem coordSystem, CanvasPanel canvasPanel) : base(coordSystem)
        {
            _canvasPanel = canvasPanel;
        }
        
        public override void HandleMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var point = CoordSystem.ToValuePoint(e.Location.X, e.Location.Y);
                _correctionPoints.Add(point);
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
            _correctionPoints.Clear();
            Refresh();
        }
        
        public override void Draw(Graphics graphics)
        {
            DrawMarks(graphics);
            DrawBaselines(graphics);
        }

        private void DrawBaselines(Graphics graphics)
        {
            if (_canvasPanel.Charts.Any() && _correctionPoints.Any())
            {
                var chartPoints = _canvasPanel.Charts[0].Points;
                try
                {
                    var baselinePoints = new PerPartesBaselineCalculator().GetBaseline(chartPoints, _correctionPoints, 3);
                    new CanvasDrawer(_canvasPanel.CoordSystem, graphics).DrawLines(baselinePoints, Pens.GreenYellow);
                }
                catch (Exception e)
                {
                }
            }
        }

        private void RemoveClosestPoint(System.Drawing.Point location)
        {
            var point = GetPointToRemove(location);
            if (point != null)
            {
                _correctionPoints = _correctionPoints.Where(x => x != point).ToList();
            }
            Refresh();
        }

        private void Refresh()
        {
            _canvasPanel.Refresh();
        }

        private void ShowContextMenu(System.Drawing.Point location)
        {
            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Remove Closest Point", null, (sender, e) => RemoveClosestPoint(location));
            contextMenu.Show(_canvasPanel, location);
        }
        
        private Point GetPointToRemove(System.Drawing.Point pos)
        {
            var closestPoint = _correctionPoints.MinByOrDefault(x => Util.GetPixelDistance(CoordSystem.ToPixelPoint(x), pos));
            if (closestPoint != null)
            {
                return closestPoint;
            }
            return null;
        }
        
        private void DrawMarks(Graphics graphics)
        {
            foreach (var point in _correctionPoints)
            {
                new Mark(CoordSystem, graphics, COLOR, point).Draw();
            }
            Refresh();
        }

        public void ImportPoint(List<Point> points)
        {
            _correctionPoints = points;
            Refresh();
        }

        public void ExportPoints(string filePath)
        {
            new OnePointPerLineFileWriter().WritePoints(_correctionPoints, filePath);
        }

        public void CorrectBaseline()
        {
            // _oldCharts = _canvasPanel.Charts;
            // _canvasPanel.Charts = _canvasPanel.Charts.Select(x => CorrectBaseline(x)).ToList();
        }

        private Chart CorrectBaseline(Chart chart)
        {
            return null;
            // var correctedPoints = new PerPartesBaselineCalculator().GetBaseline(chart.Points, _correctionPoints, );
            // var ret = new Chart(correctedPoints);
            // return ret;
        }

        public void UndoBaselineCorrection()
        {
            
        }
    }
}