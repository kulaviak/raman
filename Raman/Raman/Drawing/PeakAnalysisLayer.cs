using Raman.Controls;
using Point = Raman.Core.Point;

namespace Raman.Drawing;

public class PeakAnalysisLayer : LayerBase
{
    private readonly CanvasPanel canvasPanel;

    private static Pen PEN = Pens.Orange;

    private Point start;
    
    private Point currentPoint;

    public List<Peak> Peaks { get; set; } = new List<Peak>();
    
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
                    var chart = Util.GetClosestChart(canvasPanel.VisibleCharts, end);
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

    private List<Peak> GetPeaksForAllCharts(Point start, Point end)
    {
        var ret = new List<Peak>();
        foreach (var chart in canvasPanel.VisibleCharts)
        {
            var peak = GetPeakForChart(chart, start, end);
            ret.Add(peak);
        }
        return ret;
    }

    private Peak GetPeakForChart(Chart chart, Point userDefinedStart, Point userDefinedEnd)
    {
        var startPointAtChart = GetPointAtChart(userDefinedStart, chart);
        var endPointAtChart = GetPointAtChart(userDefinedEnd, chart);
        var top = GetTopPoint(startPointAtChart, endPointAtChart, chart);
        var peak = new Peak(startPointAtChart, endPointAtChart, top, chart);
        return peak;
    }

    private Point GetPointAtChart(Point point, Chart chart)
    {
        var y = chart.GetValue(point.X);
        if (y != null)
        {
            var ret = new Point(point.X, y.Value);
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
        canvasDrawer.DrawLine(peak.Start, peak.End, PEN);
        var intersection = GetIntersectionOfLineAndVertical(peak.Start, peak.End, peak.Top);
        if (intersection != null)
        {
            canvasDrawer.DrawLine(peak.Top, intersection, PEN);
        }
    }

    // from Chat GPT
    private Point GetIntersectionOfLineAndVertical(Point start, Point end, Point top)
    {
        var x1 = start.X;
        var y1 = start.Y;
        
        var x2 = end.X;
        var y2 = end.Y;
        
        var m1 = (y2 - y1) / (x2 - x1);
        var b1 = y1 - m1 * x1;
        
        var intersectionX = top.X;
        var intersectionY = m1 * intersectionX + b1;

        var ret = new Point(intersectionX, intersectionY);
        return ret;
    }
    
    private Point GetTopPoint(Point start, Point end, Chart chart)
    {
        var ret = chart.Points.Where(point => start.X < point.X && point.X <= end.X).MaxByOrDefault(point => point.Y);
        return ret;
    }

    private void RemoveClosestPeak(System.Drawing.Point location)
    {
        var peak = GetPeakToRemove(location);
        if (peak != null)
        {
            Peaks = Peaks.Where(x => x != peak).ToList();
        }
        Refresh();
    }

    private Peak GetPeakToRemove(System.Drawing.Point pos)
    {
        var ret = Peaks.MinByOrDefault(peak => GetDistanceToPeak(peak, pos));
        return ret;
    }

    private float GetDistanceToPeak(Peak peak, System.Drawing.Point pos)
    {
        var start = CoordSystem.ToPixelPoint(peak.Start);
        var end = CoordSystem.ToPixelPoint(peak.End);
        var ret = Math.Min(Util.GetPixelDistance(pos, start), Util.GetPixelDistance(pos, end));
        return ret;
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
        var peaks = GetPeaksForExport();
        if (!peaks.Any())
        {
            FormUtil.ShowInfo("There are no peaks to export.", "Information");
            return;
        }
        new PeakAnalysisExporter().ExportPeaks(filePath, peaks);
    }

    private List<ExportedPeak> GetPeaksForExport()
    {
        var ret = new List<ExportedPeak>();
        foreach (var peak in Peaks)
        {
            var exportedPeak = GetExportedPeak(peak);
            ret.Add(exportedPeak);
        }            
        return ret;
    }

    private ExportedPeak GetExportedPeak(Peak peak)
    {
        var intersection = GetIntersectionOfLineAndVertical(peak.Start, peak.End, peak.Top);
        var height = Util.GetDistance(peak.Top, intersection);
        var leftX = peak.Start.X;
        var rightX = peak.End.X;
        var peakX = peak.Top.X;
        var ret = new ExportedPeak(height, leftX, rightX, peakX);
        return ret;
    }
}