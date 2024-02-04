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
        new CanvasDrawer(CoordSystem, graphics).DrawLines(Lines, PEN);
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