using System.IO;
using Raman.Controls;

namespace Raman.Drawing;

public class BaselineCorrectionLayer : LayerBase
{
    private readonly CanvasPanel canvasPanel;
        
    private static Color COLOR = Color.Red;

    public List<ValuePoint> CorrectionPoints { get; set; } = new List<ValuePoint>();

    public bool AreBaselineEndsExtended
    {
        get => areBaselineEndsExtended;
        set
        {
            areBaselineEndsExtended = value;
            Refresh();
        }
    }
    
    public bool AreCorrectionPointsAdjusted { get; set; }

    private Stack<List<Chart>> chartHistory = new Stack<List<Chart>>();
    
    private Stack<List<ValuePoint>> correctionPointHistory = new Stack<List<ValuePoint>>();
    
    private bool areBaselineEndsExtended;

    public BaselineCorrectionLayer(CanvasCoordSystem coordSystem, CanvasPanel canvasPanel, bool areBaselineEndsExtended, bool areCorrectionPointsAdjusted) : base(coordSystem)
    {
        this.canvasPanel = canvasPanel;
        AreBaselineEndsExtended = areBaselineEndsExtended;
        AreCorrectionPointsAdjusted = areCorrectionPointsAdjusted;
    }
        
    public override void HandleMouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            var point = CalculateCorrectionPoint(e.Location, canvasPanel.VisibleCharts, AreCorrectionPointsAdjusted);
            CorrectionPoints.Add(point);
            Refresh();
        }
        else if (e.Button == MouseButtons.Middle && Util.IsCtrlKeyPressed())
        {
            RemoveClosestPoint(e.Location);
            Refresh();
        }
        else if (e.Button == MouseButtons.Right)
        {
            ShowContextMenu(e.Location);
        }
    }

    private ValuePoint CalculateCorrectionPoint(Point pos, List<Chart> charts, bool areCorrectionPointsAdjusted)
    {
        if (areCorrectionPointsAdjusted)
        {
            var closestChart = new ClosestChartCalculator().GetClosestChart(charts, pos, CoordSystem); 
            var closestPoints = GetClosestPoints(closestChart.Points, pos);
            var averageY = closestPoints.Average(point => point.Y);
            var ret = new ValuePoint(CoordSystem.ToValueX(pos.X), averageY);
            return ret;
        }
        else
        {
            return CoordSystem.ToValuePoint(pos.X, pos.Y);
        }
    }

    private List<ValuePoint> GetClosestPoints(List<ValuePoint> points, Point pos)
    {
        var ret = new List<ValuePoint>();
        var closestPoint = points.MinByOrDefault(x => Util.GetPixelDistance(CoordSystem.ToPixelPoint(x), pos));
        ret.Add(closestPoint);
        var index = points.IndexOf(closestPoint);
        if (index - 1 >= 0)
        {
            ret.Add(points[index - 1]);
        }
        if (index - 2 >= 0)
        {
            ret.Add(points[index - 2]);
        }
        if (index + 1 < points.Count)
        {
            ret.Add(points[index + 1]);
        }
        if (index + 2 >= points.Count)
        {
            ret.Add(points[index + 2]);
        }
        return ret;
    }

    public void Reset()
    {
        if (!CorrectionPoints.Any())
        {
            MessageUtil.ShowInfo("There are no points to reset.", "Information");
            return;
        }
        CorrectionPoints.Clear();
        Refresh();
    }
        
    public override void Draw(Graphics graphics)
    {
        DrawBaselines(graphics);
        DrawMarks(graphics);
    }
    
    public void ImportPoints(List<ValuePoint> points)
    {
        CorrectionPoints = points;
        Refresh();
    }

    public void ExportPoints(string filePath)
    {
        if (!CorrectionPoints.Any())
        {
            MessageUtil.ShowInfo("There are no baseline points to export.", "Information");
            return;
        }
        new OnePointPerLineFileWriter().WritePoints(CorrectionPoints, filePath);
    }

    public void CorrectBaseline()
    {
        if (!CorrectionPoints.Any())
        {
            MessageUtil.ShowInfo("There are no baseline points defined.", "Information");
            return;
        }
        chartHistory.Push(canvasPanel.Charts);
        correctionPointHistory.Push(CorrectionPoints);
        canvasPanel.Charts = canvasPanel.VisibleCharts.Select(x => CorrectBaseline(x, CorrectionPoints)).ToList();
        CorrectionPoints = new List<ValuePoint>();
        canvasPanel.ZoomToSeeAllCharts();
    }
    
    public void ExportCorrectedCharts()
    {
        var exportedCharts = canvasPanel.Charts.Where(x => x.IsBaselineCorrected).ToList();
        if (!exportedCharts.Any())
        {
            MessageUtil.ShowUserError("There are no corrected spectra.", "No spectra to export");
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
                MessageUtil.ShowInfo("Export finished successfully.", "Export finished");
            }
        }
    }
    
    private void DrawBaselines(Graphics graphics)
    {
        if (canvasPanel.VisibleCharts.Any() && CorrectionPoints.Count >= 4)
        {
            try
            {
                var start = GetBaselineStart();
                var end = GetBaselineEnd();
                var xPositions = GetXPositions(start, end);
                var baselinePoints = new SplineBaselineCalculator().GetBaseline(xPositions, CorrectionPoints);
                new CanvasDrawer(canvasPanel.CoordSystem, graphics).DrawLines(baselinePoints, Pens.Green);
            }
            catch (Exception e)
            {
                MessageUtil.ShowAppError("Drawing baseline failed.", "Error", e);
            }
        }
    }

    private decimal GetBaselineEnd()
    {
        if (AreBaselineEndsExtended)
        {
            var chartPointsMax = canvasPanel.Charts.SelectMany(x => x.Points).Max(point => point.X);
            var correctionPointsMax = CorrectionPoints.Max(point => point.X);
            return Math.Max(chartPointsMax, correctionPointsMax);
        }
        else
        {
            return CorrectionPoints.Max(point => point.X);
        }
    }

    private decimal GetBaselineStart()
    {
        if (AreBaselineEndsExtended)
        {
            var chartPointsMin = canvasPanel.Charts.SelectMany(x => x.Points).Min(point => point.X);
            var correctionPointsMin = CorrectionPoints.Min(point => point.X);
            return Math.Min(chartPointsMin, correctionPointsMin);
        }
        else
        {
            return CorrectionPoints.Min(point => point.X);
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

    private void RemoveClosestPoint(Point location)
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

    private void ShowContextMenu(Point location)
    {
        var contextMenu = new ContextMenuStrip();
        contextMenu.Items.Add("Remove Closest Point", null, (_, _) => RemoveClosestPoint(location));
        contextMenu.Show(canvasPanel, location);
    }
        
    private ValuePoint GetPointToRemove(Point pos)
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
    
    private Chart CorrectBaseline(Chart chart, List<ValuePoint> correctionPoints)
    {
        var baselineStart = GetBaselineStart();
        var baselineEnd = GetBaselineEnd();
        var xPositions = chart.Points.Where(point => baselineStart <= point.X && point.X <= baselineEnd).Select(point => point.X).ToList();
        var baselinePoints = new SplineBaselineCalculator().GetBaseline(xPositions, correctionPoints);
        var newChartPoints = new BaselineCorrector().CorrectChartByBaseline(chart.Points, baselinePoints);
        var ret = new Chart(newChartPoints, chart.Name);
        ret.IsBaselineCorrected = true;
        return ret;
    }

    public void UndoBaselineCorrection()
    {
        if (chartHistory.Count == 0)
        {
            MessageUtil.ShowInfo("There is no undo to do.", "Information");
            return;
        }
        canvasPanel.Charts = chartHistory.Pop();
        CorrectionPoints = correctionPointHistory.Pop();
        canvasPanel.ZoomToSeeAllCharts();
    }
    
    private void ExportCorrectedChart(Chart chart, string folderPath)
    {
        var filePath = Path.Combine(folderPath, chart.Name + "_bc.txt");
        new OnePointPerLineFileWriter().WritePoints(chart.Points, filePath);
    }
}