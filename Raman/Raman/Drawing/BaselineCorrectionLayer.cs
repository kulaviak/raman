using Raman.Controls;
using Point = Raman.Core.Point;

namespace Raman.Drawing;

public class BaselineCorrectionLayer : LayerBase
{
    private readonly CanvasPanel _canvasPanel;
        
    private static Color COLOR = Color.Red;

    public List<Point> CorrectionPoints { get; set; } = new List<Point>();
    
    public bool IsBaselineCorrected { get; set; }

    private List<Chart> _oldCharts = new List<Chart>();

    public BaselineCorrectionLayer(CanvasCoordSystem coordSystem, CanvasPanel canvasPanel) : base(coordSystem)
    {
        _canvasPanel = canvasPanel;
    }
        
    public override void HandleMouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            var point = CoordSystem.ToValuePoint(e.Location.X, e.Location.Y);
            CorrectionPoints.Add(point);
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
        CorrectionPoints.Clear();
        Refresh();
    }
        
    public override void Draw(Graphics graphics)
    {
        DrawBaselines(graphics);
        DrawMarks(graphics);
    }

    private void DrawBaselines(Graphics graphics)
    {
        if (_canvasPanel.Charts.Any() && CorrectionPoints.Count >= 4)
        {
            try
            {
                var xPositions = GetXPositions(CorrectionPoints.Min(point => point.X), CorrectionPoints.Max(point => point.X));
                var baselinePoints = new SplineBaselineCalculator().GetBaseline(xPositions, CorrectionPoints);
                new CanvasDrawer(_canvasPanel.CoordSystem, graphics).DrawLines(baselinePoints, Pens.Green);
            }
            catch (Exception e)
            {
                FormUtil.ShowAppError("Drawing baseline failed.", "Error", e);
            }
        }
    }

    private List<decimal> GetXPositions(decimal start, decimal end)
    {
        var diff = (end - start) / 1000;
        var ret = new List<decimal>();
        for (var position = start; position  <= end; position += diff)
        {
            ret.Add(position);    
        }
        return ret;
    }

    private void RemoveClosestPoint(System.Drawing.Point location)
    {
        var point = GetPointToRemove(location);
        if (point != null)
        {
            CorrectionPoints = CorrectionPoints.Where(x => x != point).ToList();
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
        contextMenu.Items.Add("Remove Closest Point", null, (_, _) => RemoveClosestPoint(location));
        contextMenu.Show(_canvasPanel, location);
    }
        
    private Point GetPointToRemove(System.Drawing.Point pos)
    {
        var closestPoint = CorrectionPoints.MinByOrDefault(x => Util.GetPixelDistance(CoordSystem.ToPixelPoint(x), pos));
        if (closestPoint != null)
        {
            return closestPoint;
        }
        return null;
    }
        
    private void DrawMarks(Graphics graphics)
    {
        foreach (var point in CorrectionPoints)
        {
            new Mark(CoordSystem, graphics, COLOR, point).Draw();
        }
    }

    public void ImportPoints(List<Point> points)
    {
        CorrectionPoints = points;
        Refresh();
    }

    public void ExportPoints(string filePath)
    {
        new OnePointPerLineFileWriter().WritePoints(CorrectionPoints, filePath);
    }

    public void CorrectBaseline()
    {
        _oldCharts = _canvasPanel.Charts;
        _canvasPanel.Charts = _canvasPanel.Charts.Select(x => CorrectBaseline(x)).ToList();
        _canvasPanel.Refresh();
    }

    private Chart CorrectBaseline(Chart chart)
    {
        var baselinePoints = new SplineBaselineCalculator().GetBaseline(chart.Points.Select(point => point.X).ToList(), CorrectionPoints);
        var newChartPoints = new BaselineCorrector().CorrectChartByBaseline(chart.Points, baselinePoints);
        var ret = new Chart(newChartPoints);
        return ret;
    }

    public void UndoBaselineCorrection()
    {
        _canvasPanel.Charts = _oldCharts;
        _canvasPanel.Refresh();
    }
}