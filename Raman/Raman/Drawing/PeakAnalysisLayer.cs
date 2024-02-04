using Point = Raman.Core.Point;

namespace Raman.Drawing;

public class PeakAnalysisLayer : LayerBase
{
    private readonly CanvasPanel canvasPanel;
        
    private static Color COLOR = Color.Orange;

    public List<Point> Points { get; set; } = new List<Point>();
    
    public PeakAnalysisLayer(CanvasCoordSystem coordSystem, CanvasPanel canvasPanel) : base(coordSystem)
    {
        this.canvasPanel = canvasPanel;
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
            RemoveClosestLine(e.Location);
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
        DrawLines(graphics);
        DrawMarks(graphics);
    }

    private void DrawLines(Graphics graphics)
    {
        // if (_canvasPanel.Charts.Any() && Points.Count >= 4)
        // {
        //     try
        //     {
        //         var start = Points.Min(point => point.X);
        //         var end = Points.Max(point => point.X);
        //         var xPositions = GetXPositions(start, end);
        //         var baselinePoints = new SplineBaselineCalculator().GetBaseline(xPositions, Points);
        //         new CanvasDrawer(_canvasPanel.CoordSystem, graphics).DrawLines(baselinePoints, Pens.Green);
        //     }
        //     catch (Exception e)
        //     {
        //         FormUtil.ShowAppError("Drawing baseline failed.", "Error", e);
        //     }
        // }
    }
    
    private void RemoveClosestLine(System.Drawing.Point location)
    {
        // var point = GetPointToRemove(location);
        // if (point != null)
        // {
        //     Points = Points.Where(x => x != point).ToList();
        // }
        // Refresh();
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
        
    // private Point GetPointToRemove(System.Drawing.Point pos)
    // {
    //     var closestPoint = Points.MinByOrDefault(x => Util.GetPixelDistance(CoordSystem.ToPixelPoint(x), pos));
    //     if (closestPoint != null)
    //     {
    //         return closestPoint;
    //     }
    //     return null;
    // }
        
    private void DrawMarks(Graphics graphics)
    {
        foreach (var point in Points)
        {
            new Mark(CoordSystem, graphics, COLOR, point).Draw();
        }
    }
}