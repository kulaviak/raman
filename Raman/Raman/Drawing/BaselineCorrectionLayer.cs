using Raman.Controls;
using Point = Raman.Core.Point;

namespace Raman.Drawing;

public class BaselineCorrectionLayer : LayerBase
{
    private readonly CanvasPanel _canvasPanel;
        
    private static Color COLOR = Color.Red;

    public List<Point> Points { get; set; } = new List<Point>();
    
    public bool IsBaselineCorrected { get; set; }

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
            Points.Add(point);
            Refresh();
        }
        else if (e.Button == MouseButtons.Middle)
        {
            RemoveClosestPoint(e.Location);
            Refresh();
        }
        else if (e.Button == MouseButtons.Right)
        {
            ShowContextMenu(e.Location);
        }
    }
        
    public void Reset()
    {
        Points.Clear();
        Refresh();
    }
        
    public override void Draw(Graphics graphics)
    {
        DrawBaselines(graphics);
        DrawMarks(graphics);
    }

    private void DrawBaselines(Graphics graphics)
    {
        if (_canvasPanel.Charts.Any() && Points.Count >= 4)
        {
            var chartPoints = _canvasPanel.Charts[0].Points;
            try
            {
                var baselinePoints = new SplineBaselineCalculator().GetBaseline(chartPoints, Points);
                new CanvasDrawer(_canvasPanel.CoordSystem, graphics).DrawLines(baselinePoints, Pens.Green);
            }
            catch (Exception e)
            {
                FormUtil.ShowAppError("Drawing baseline failed.", "Error", e);
            }
        }
    }

    private void RemoveClosestPoint(System.Drawing.Point location)
    {
        var point = GetPointToRemove(location);
        if (point != null)
        {
            Points = Points.Where(x => x != point).ToList();
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
        var closestPoint = Points.MinByOrDefault(x => Util.GetPixelDistance(CoordSystem.ToPixelPoint(x), pos));
        if (closestPoint != null)
        {
            return closestPoint;
        }
        return null;
    }
        
    private void DrawMarks(Graphics graphics)
    {
        foreach (var point in Points)
        {
            new Mark(CoordSystem, graphics, COLOR, point).Draw();
        }
    }

    public void ImportPoints(List<Point> points)
    {
        Points = points;
        Refresh();
    }

    public void ExportPoints(string filePath)
    {
        new OnePointPerLineFileWriter().WritePoints(Points, filePath);
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