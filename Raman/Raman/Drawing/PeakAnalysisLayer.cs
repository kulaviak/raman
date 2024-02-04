using Point = Raman.Core.Point;

namespace Raman.Drawing;

public class PeakAnalysisLayer : LayerBase
{
    private readonly CanvasPanel canvasPanel;
        
    private static Color COLOR = Color.Orange;

    private static Pen PEN = Pens.Orange;

    private Point start;
    
    private Point currentPoint;

    public List<Line> Lines { get; set; } = new List<Line>();
    
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
                var line = new Line(start, end);
                Lines.Add(line);
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

    public override void HandleMouseMove(object sender, MouseEventArgs e)
    {
        currentPoint = CoordSystem.ToValuePoint(e.Location.X, e.Location.Y);
        canvasPanel.Refresh();
    }

    public void Reset()
    {
        Lines.Clear();
        Refresh();
    }
        
    public override void Draw(Graphics graphics)
    {
        DrawLines(graphics);
        DrawCurrentLine(graphics);
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
                var peak = GetPeakBetweenPoints(line.Start, line.End, chart);
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

    private Point GetPeakBetweenPoints(Point start, Point end, Chart chart)
    {
        var ret = chart.Points.Where(point => start.X < point.X && point.X <= end.X).MaxByOrDefault(point => point.Y);
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
}