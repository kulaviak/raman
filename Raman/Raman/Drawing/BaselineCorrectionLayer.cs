using System.IO;
using Raman.Controls;
using Point = Raman.Core.Point;

namespace Raman.Drawing;

public class BaselineCorrectionLayer : LayerBase
{
    private readonly CanvasPanel canvasPanel;
        
    private static Color COLOR = Color.Red;

    public List<Point> CorrectionPoints { get; set; } = new List<Point>();
    
    public bool IsBaselineCorrected { get; set; }

    private List<Chart> oldCharts = new List<Chart>();

    public BaselineCorrectionLayer(CanvasCoordSystem coordSystem, CanvasPanel canvasPanel) : base(coordSystem)
    {
        this.canvasPanel = canvasPanel;
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
        if (!IsBaselineCorrected)
        {
            DrawBaselines(graphics);
            DrawMarks(graphics);
        }
    }

    private void DrawBaselines(Graphics graphics)
    {
        if (canvasPanel.Charts.Any() && CorrectionPoints.Count >= 4)
        {
            try
            {
                var start = CorrectionPoints.Min(point => point.X);
                var end = CorrectionPoints.Max(point => point.X);
                var xPositions = GetXPositions(start, end);
                var baselinePoints = new SplineBaselineCalculator().GetBaseline(xPositions, CorrectionPoints);
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
            CorrectionPoints = CorrectionPoints.Where(x => x != point).ToList();
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
        oldCharts = canvasPanel.Charts;
        canvasPanel.Charts = canvasPanel.Charts.Select(x => CorrectBaseline(x)).ToList();
        IsBaselineCorrected = true;
        canvasPanel.Refresh();
    }

    private Chart CorrectBaseline(Chart chart)
    {
        var correctionStart = CorrectionPoints.Min(x => x.X);
        var correctionEnd = CorrectionPoints.Max(x => x.X);
        // get baseline only between correction points
        var xPositions = chart.Points.Where(point => correctionStart <= point.X && point.X <= correctionEnd).Select(point => point.X).ToList();
        var baselinePoints = new SplineBaselineCalculator().GetBaseline(xPositions, CorrectionPoints);
        var newChartPoints = new BaselineCorrector().CorrectChartByBaseline(chart.Points, baselinePoints);
        var ret = new Chart(newChartPoints, chart.Name);
        ret.IsBaselineCorrected = true;
        return ret;
    }

    public void UndoBaselineCorrection()
    {
        canvasPanel.Charts = oldCharts;
        IsBaselineCorrected = false;
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
        try
        {
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
        catch (Exception e)
        {
            FormUtil.ShowAppError($"Spectra export failed. Please check error and try again. Reason: {e.Message}", "Export failed", e);
        }
    }

    private void ExportCorrectedChart(Chart chart, string folderPath)
    {
        var filePath = Path.Combine(folderPath, chart.Name + "_bc.txt");
        new OnePointPerLineFileWriter().WritePoints(chart.Points, filePath);
    }
}