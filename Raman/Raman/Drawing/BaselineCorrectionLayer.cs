using System.IO;
using Raman.Controls;
using Point = Raman.Core.Point;

namespace Raman.Drawing;

public class BaselineCorrectionLayer : LayerBase
{
    private readonly CanvasPanel canvasPanel;
        
    private static Color COLOR = Color.Red;

    public List<Point> BaselinePoints { get; set; } = new List<Point>();

    private Stack<List<Chart>> undoStack = new Stack<List<Chart>>();

    public BaselineCorrectionLayer(CanvasCoordSystem coordSystem, CanvasPanel canvasPanel) : base(coordSystem)
    {
        this.canvasPanel = canvasPanel;
    }
        
    public override void HandleMouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            var point = CoordSystem.ToValuePoint(e.Location.X, e.Location.Y);
            BaselinePoints.Add(point);
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
        if (!BaselinePoints.Any())
        {
            FormUtil.ShowInfo("There are no points to reset.", "Information");
            return;
        }
        BaselinePoints.Clear();
        Refresh();
    }
        
    public override void Draw(Graphics graphics)
    {
        DrawBaselines(graphics);
        DrawMarks(graphics);
    }

    private void DrawBaselines(Graphics graphics)
    {
        if (canvasPanel.Charts.Any() && BaselinePoints.Count >= 4)
        {
            try
            {
                var start = BaselinePoints.Min(point => point.X);
                var end = BaselinePoints.Max(point => point.X);
                var xPositions = GetXPositions(start, end);
                var baselinePoints = new SplineBaselineCalculator().GetBaseline(xPositions, BaselinePoints);
                new CanvasDrawer(canvasPanel.CoordSystem, graphics).DrawLines(baselinePoints, Pens.Green);
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
            BaselinePoints = BaselinePoints.Where(x => x != point).ToList();
        }
        Refresh();
    }

    private void Refresh()
    {
        canvasPanel.Refresh();
    }

    private void ShowContextMenu(System.Drawing.Point location)
    {
        var contextMenu = new ContextMenuStrip();
        contextMenu.Items.Add("Remove Closest Point", null, (_, _) => RemoveClosestPoint(location));
        contextMenu.Show(canvasPanel, location);
    }
        
    private Point GetPointToRemove(System.Drawing.Point pos)
    {
        var closestPoint = BaselinePoints.MinByOrDefault(x => Util.GetPixelDistance(CoordSystem.ToPixelPoint(x), pos));
        if (closestPoint != null)
        {
            return closestPoint;
        }
        return null;
    }
        
    private void DrawMarks(Graphics graphics)
    {
        foreach (var point in BaselinePoints)
        {
            new Mark(CoordSystem, graphics, COLOR, point).Draw();
        }
    }

    public void ImportPoints(List<Point> points)
    {
        BaselinePoints = points;
        Refresh();
    }

    public void ExportPoints(string filePath)
    {
        if (!BaselinePoints.Any())
        {
            FormUtil.ShowInfo("There are no baseline points to export.", "Information");
            return;
        }
        new OnePointPerLineFileWriter().WritePoints(BaselinePoints, filePath);
    }

    public void CorrectBaseline()
    {
        if (!BaselinePoints.Any())
        {
            FormUtil.ShowInfo("There are no baseline points defined.", "Information");
            return;
        }
        undoStack.Push(canvasPanel.Charts);
        canvasPanel.Charts = canvasPanel.Charts.Select(x => CorrectBaseline(x, BaselinePoints)).ToList();
        BaselinePoints = new List<Point>();
        canvasPanel.Refresh();
    }

    private static Chart CorrectBaseline(Chart chart, List<Point> correctionPoints)
    {
        var correctionStart = correctionPoints.Min(x => x.X);
        var correctionEnd = correctionPoints.Max(x => x.X);
        // get baseline only between correction points
        var xPositions = chart.Points.Where(point => correctionStart <= point.X && point.X <= correctionEnd).Select(point => point.X).ToList();
        var baselinePoints = new SplineBaselineCalculator().GetBaseline(xPositions, correctionPoints);
        var newChartPoints = new BaselineCorrector().CorrectChartByBaseline(chart.Points, baselinePoints);
        var ret = new Chart(newChartPoints, chart.Name);
        ret.IsBaselineCorrected = true;
        return ret;
    }

    public void UndoBaselineCorrection()
    {
        if (undoStack.Count == 0)
        {
            FormUtil.ShowInfo("There is no undo to do.", "Information");
            return;
        }
        canvasPanel.Charts = undoStack.Pop();
        canvasPanel.Refresh();
    }

    public void ExportCorrectedCharts()
    {
        var exportedCharts = canvasPanel.Charts.Where(x => x.IsBaselineCorrected).ToList();
        if (!exportedCharts.Any())
        {
            FormUtil.ShowUserError("There are no corrected spectra.", "No spectra to export");
            return;
        }
        using (var folderBrowserDialog = new FolderBrowserDialog())
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                var selectedPath = folderBrowserDialog.SelectedPath;
                foreach (var chart in exportedCharts)
                {
                    ExportCorrectedChart(chart, selectedPath);                                                
                }
                FormUtil.ShowInfo("Export finished successfully.", "Export finished");
            }
        }
    }

    private void ExportCorrectedChart(Chart chart, string folderPath)
    {
        var filePath = Path.Combine(folderPath, chart.Name + "_bc.txt");
        new OnePointPerLineFileWriter().WritePoints(chart.Points, filePath);
    }
}