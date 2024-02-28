using Raman.Controls;
using Point = Raman.Core.Point;

namespace Raman.Drawing;

public class CutOffLayer : LayerBase
{
    private readonly CanvasPanel canvasPanel;
    
    private Point start;
    
    private Point end;

    private static Color COLOR = Color.Red;

    private List<Chart> oldCharts = null;
    
    public CutOffLayer(CanvasCoordSystem coordSystem, CanvasPanel canvasPanel) : base(coordSystem)
    {
        this.canvasPanel = canvasPanel;
    }

    public void CutOff()
    {
        if (start == null)
        {
            FormUtil.ShowUserError("Start point is not set.", "Error");
            return;
        }
        if (end == null)
        {
            FormUtil.ShowUserError("End point is not set.", "Error");
            return;
        }
        oldCharts = canvasPanel.Charts;
        canvasPanel.Charts = GetCutOffCharts(canvasPanel.Charts, start.X, end.X);
        Refresh();
    }

    private static List<Chart> GetCutOffCharts(List<Chart> charts, decimal start, decimal end)
    {
        var ret = new List<Chart>();
        foreach (var chart in charts)
        {
            var newChart = chart.IsVisible ? GetCutOffChart(chart, start, end) : chart;
            ret.Add(newChart);
        }
        return ret;
    }

    private static Chart GetCutOffChart(Chart chart, decimal start, decimal end)
    {
        var points = chart.Points.Where(point => start <= point.X && point.X <= end).ToList();
        var ret = new Chart(points, chart.Name);
        return ret;
    }

    public void UndoCutOff()
    {
        if (oldCharts == null)
        {
            FormUtil.ShowUserError("There is no undo to do.", "Error");
            return;
        }
        canvasPanel.Charts = oldCharts;
        oldCharts = null;
        Refresh();
    }
    
    public override void HandleMouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            if (start == null)
            {
                start = CoordSystem.ToValuePoint(e.Location.X, e.Location.Y);
            }
            else if (end == null)
            {
                end = CoordSystem.ToValuePoint(e.Location.X, e.Location.Y);
            }
        }
        else if (e.Button == MouseButtons.Middle)
        {
            RemoveClosestPoint(e.Location);
        }
        Refresh();
    }

    private void RemoveClosestPoint(System.Drawing.Point pos)
    {
        var points = new List<Point>();
        if (start != null)
        {
            points.Add(start);
        }
        if (end != null)
        {
            points.Add(end);
        }
        var closestPoint = points.MinByOrDefault(x => Util.GetPixelDistance(CoordSystem.ToPixelPoint(x), pos));
        if (closestPoint != null)
        {
            if (closestPoint == start)
            {
                start = null;
            }
            else if (closestPoint == end)
            {
                end = null;
            }
        }
    }

    private void Refresh()
    {
        canvasPanel.Refresh();
    }

    public override void Draw(Graphics graphics)
    {
        if (start != null)
        {
            new Mark(CoordSystem, graphics, COLOR, start).Draw();
        }
        if (end != null)
        {
            new Mark(CoordSystem, graphics, COLOR, end).Draw();
        }
    }

    public void ResetPoints()
    {
        start = null;
        end = null;
        Refresh();
    }
}