using Raman.Controls;

namespace Raman.Drawing;

public class PeakAnalysisLayer : LayerBase
{
    private readonly CanvasPanel canvasPanel;

    private static Pen PEN = Pens.Orange;

    private ValuePoint start;
    
    private ValuePoint currentPoint;

    public List<Peak> Peaks { get; set; } = new List<Peak>();

    private List<Peak> VisiblePeaks => Peaks.Where(peak => peak.Chart.IsVisible).ToList();
    
    public bool IsExported { get; set; }
    
    public bool IsPeakAddedToAllCharts { get; set; }

    public PeakAnalysisLayer(CanvasCoordSystem coordSystem, CanvasPanel canvasPanel) : base(coordSystem)
    {
        this.canvasPanel = canvasPanel;
    }
        
    public override void HandleMouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            if (start == null)
            {
                start = CoordSystem.ToValuePoint(e.Location.X, e.Location.Y);
            }
            else
            {
                var end = CoordSystem.ToValuePoint(e.Location.X, e.Location.Y);
                if (IsPeakAddedToAllCharts)
                {
                    var peaks = GetPeaksForAllCharts(start, end);
                    Peaks.AddRange(peaks);
                }
                else
                {
                    var chart = new ClosestChartCalculator().GetClosestChart(canvasPanel.VisibleCharts, e.Location, canvasPanel.CoordSystem);                    
                    var peak = GetPeakForChart(chart, start, end);
                    Peaks.Add(peak);
                }
                IsExported = false;
                start = null;
            }
            Refresh();
        }
        else if (e.Button == MouseButtons.Middle)
        {
            RemoveClosestPeak(e.Location);
            Refresh();
        }
        else if (e.Button == MouseButtons.Right)
        {
            ShowContextMenu(e.Location);
        }
    }

    private List<Peak> GetPeaksForAllCharts(ValuePoint start, ValuePoint end)
    {
        var ret = new List<Peak>();
        foreach (var chart in canvasPanel.VisibleCharts)
        {
            var peak = GetPeakForChart(chart, start, end);
            ret.Add(peak);
        }
        return ret;
    }

    private Peak GetPeakForChart(Chart chart, ValuePoint userDefinedStart, ValuePoint userDefinedEnd)
    {
        var startPointAtChart = GetPointAtChart(userDefinedStart, chart);
        var endPointAtChart = GetPointAtChart(userDefinedEnd, chart);
        var top = GetTopPoint(startPointAtChart, endPointAtChart, chart);
        // if end is before start, then swap it
        if (endPointAtChart.X < startPointAtChart.X)
        {
            (startPointAtChart, endPointAtChart) = (endPointAtChart, startPointAtChart);
        }
        var peak = new Peak(startPointAtChart, endPointAtChart, top, chart);
        return peak;
    }

    private ValuePoint GetPointAtChart(ValuePoint point, Chart chart)
    {
        var y = chart.GetValue(point.X);
        if (y != null)
        {
            var ret = new ValuePoint(point.X, y.Value);
            return ret;
        }
        // if the x value of the point is out of range the chart, then return first or last point of chart
        else
        {
            if (point.X < chart.Points.First().X)
            {
                return chart.Points.First();
            }
            else
            {
                return chart.Points.Last();
            }
        }
    }

    public override void HandleMouseMove(object sender, MouseEventArgs e)
    {
        currentPoint = CoordSystem.ToValuePoint(e.Location.X, e.Location.Y);
        canvasPanel.Refresh();
    }

    public void Reset()
    {
        if (!Peaks.Any())
        {
            FormUtil.ShowInfo("There are no peak to reset.", "Information");
            return;
        }
        Peaks.Clear();
        Refresh();
    }
        
    public override void Draw(Graphics graphics)
    {
        DrawPeaks(graphics);
        DrawCurrentLine(graphics);
    }

    public override void HandleKeyPress(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape && currentPoint != null)
        {
            start = null;
            Refresh();
        }
    }

    private void DrawCurrentLine(Graphics graphics)
    {
        if (start != null && currentPoint != null)
        {
            new CanvasDrawer(CoordSystem, graphics).DrawLine(start, currentPoint, PEN);
        }
    }

    private void DrawPeaks(Graphics graphics)
    {
        var visiblePeaks = Peaks.Where(x => x.Chart.IsVisible).ToList();
        foreach (var peak in visiblePeaks)
        {
            DrawPeak(peak, graphics);
        }
    }

    private void DrawPeak(Peak peak, Graphics graphics)
    {
        var canvasDrawer = new CanvasDrawer(CoordSystem, graphics);
        canvasDrawer.DrawLine(peak.Base, PEN);
        canvasDrawer.DrawLine(peak.Vertical, PEN);
    }
    
    private ValuePoint GetTopPoint(ValuePoint start, ValuePoint end, Chart chart)
    {
        var ret = chart.Points.Where(point => start.X < point.X && point.X <= end.X).MaxByOrDefault(point => point.Y);
        return ret;
    }

    /// <summary>
    /// Expects, that user clicks on vertical or bottom line of the peak. So it finds the closest peak according to it.
    /// </summary>
    private void RemoveClosestPeak(System.Drawing.Point location)
    {
        var peak = new PeakToRemoveCalculator().GetPeakToRemove(VisiblePeaks, CoordSystem, location);
        if (peak != null)
        {
            Peaks = Peaks.Where(x => x != peak).ToList();
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
        contextMenu.Items.Add("Remove Closest Line", null, (_, _) => RemoveClosestPeak(location));
        contextMenu.Show(canvasPanel, location);
    }

    public void ExportPeaks(string filePath)
    {
        if (!VisiblePeaks.Any())
        {
            FormUtil.ShowInfo("There are no peaks to export.", "Information");
            return;
        }
        new PeakAnalysisExporter().ExportPeaks(filePath, VisiblePeaks);
    }
}