using Raman.Controls;
using Point = Raman.Core.Point;

namespace Raman.Drawing;

public class PeakAnalysisLayer : LayerBase
{
    private readonly CanvasPanel canvasPanel;

    private static Pen PEN = Pens.Orange;

    private Point start;
    
    private Point currentPoint;

    public List<Line> Lines { get; set; } = new List<Line>();
    
    public bool IsExported { get; set; }
    
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
                var newLines = GetLinesForAllCharts(start, end);
                Lines.AddRange(newLines);
                IsExported = false;
                start = null;
            }
            Refresh();
        }
        else if (e.Button == MouseButtons.Middle)
        {
            RemoveClosestLine(e.Location);
            Refresh();
        }
        else if (e.Button == MouseButtons.Right)
        {
            ShowContextMenu(e.Location);
        }
    }

    private List<Line> GetLinesForAllCharts(Point start, Point end)
    {
        var ret = new List<Line>();
        foreach (var chart in canvasPanel.VisibleCharts)
        {
            var line = GetLineForChart(chart, start, end);
            ret.Add(line);
        }
        return ret;
    }

    private Line GetLineForChart(Chart chart, Point userDefinedStart, Point userDefinedEnd)
    {
        var startPointAtChart = GetPointAtChart(userDefinedStart, chart);
        var endPointAtChart = GetPointAtChart(userDefinedEnd, chart);
        var line = new Line(startPointAtChart, endPointAtChart);
        return line;
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
        if (!Lines.Any())
        {
            FormUtil.ShowInfo("There are no correction points to reset.", "Information");
            return;
        }
        Lines.Clear();
        Refresh();
    }
        
    public override void Draw(Graphics graphics)
    {
        DrawLines(graphics);
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

    private void DrawLines(Graphics graphics)
    {
        var canvasDrawer = new CanvasDrawer(CoordSystem, graphics);
        foreach (var line in Lines)
        {
            canvasDrawer.DrawLine(line.Start, line.End, PEN);
            foreach (var chart in canvasPanel.VisibleCharts)
            {
                var peak = GetPeakPoint(line, chart);
                if (peak != null)
                {
                    var intersection = GetIntersectionOfLineAndVertical(line, peak);
                    if (intersection != null)
                    {
                        canvasDrawer.DrawLine(peak, intersection, PEN);
                    }
                }
            }
        }
    }

    // from Chat GPT
    private Point GetIntersectionOfLineAndVertical(Line line, Point peak)
    {
        var x1 = line.Start.X;
        var y1 = line.Start.Y;
        
        var x2 = line.End.X;
        var y2 = line.End.Y;
        
        var m1 = (y2 - y1) / (x2 - x1);
        var b1 = y1 - m1 * x1;
        
        var intersectionX = peak.X;
        var intersectionY = m1 * intersectionX + b1;

        var ret = new Point(intersectionX, intersectionY);
        return ret;
    }

    private Point GetPeakPoint(Line line, Chart chart)
    {
        var ret = chart.Points.Where(point => line.Start.X < point.X && point.X <= line.End.X).MaxByOrDefault(point => point.Y);
        return ret;
    }

    private void RemoveClosestLine(System.Drawing.Point location)
    {
        var line = GetLineToRemove(location);
        if (line != null)
        {
            Lines = Lines.Where(x => x != line).ToList();
        }
        Refresh();
    }

    private Line GetLineToRemove(System.Drawing.Point pos)
    {
        var ret = Lines.MinByOrDefault(line => GetDistanceToLine(line, pos));
        return ret;
    }

    private float GetDistanceToLine(Line line, System.Drawing.Point pos)
    {
        var start = CoordSystem.ToPixelPoint(line.Start);
        var end = CoordSystem.ToPixelPoint(line.End);
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
        contextMenu.Items.Add("Remove Closest Line", null, (_, _) => RemoveClosestLine(location));
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
        foreach (var chart in canvasPanel.VisibleCharts)
        {
            foreach (var line in Lines)
            {
                var peak = GetExportedPeak(line, chart);
                ret.Add(peak);
            }            
        }
        return ret;
    }

    private ExportedPeak GetExportedPeak(Line line, Chart chart)
    {
        var peakPoint = GetPeakPoint(line, chart);
        var intersection = GetIntersectionOfLineAndVertical(line, peakPoint);
        var height = Util.GetDistance(peakPoint, intersection);
        var leftX = line.Start.X;
        var rightX = line.End.X;
        var peakX = peakPoint.X;
        var ret = new ExportedPeak(height, leftX, rightX, peakX);
        return ret;
    }
}