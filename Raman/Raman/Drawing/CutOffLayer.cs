using Raman.Controls;

namespace Raman.Drawing;

public class CutOffLayer(CanvasCoordSystem coordSystem, CanvasPanel canvasPanel) : LayerBase(coordSystem)
{
    public ValuePoint Start => start;
    
    public ValuePoint End => end;

    private ValuePoint start;
    
    private ValuePoint end;

    private static Color COLOR = Color.Red;

    private Stack<List<Chart>> chartHistory = new Stack<List<Chart>>();
    
    private Stack<ValuePoint> startHistory = new Stack<ValuePoint>();
    
    private Stack<ValuePoint> endHistory = new Stack<ValuePoint>();

    public void CutOff()
    {
        if (start == null)
        {
            MessageUtil.ShowInfo("Start point is not set.", "Information");
            return;
        }
        if (end == null)
        {
            MessageUtil.ShowInfo("End point is not set.", "Information");
            return;
        }
        startHistory.Push(start);
        endHistory.Push(end);
        chartHistory.Push(canvasPanel.Charts);
        canvasPanel.Charts = GetCutOffCharts(canvasPanel.Charts, start.X, end.X);
        start = null;
        end = null;
        Refresh();
    }
    
    public void UndoCutOff()
    {
        if (chartHistory.Count == 0)
        {
            MessageUtil.ShowInfo("There is no undo to do.", "Information");
            return;
        }
        start = startHistory.Pop();
        end = endHistory.Pop();
        canvasPanel.Charts = chartHistory.Pop();
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
        // else if (e.Button == MouseButtons.Middle)
        // {
        //     RemoveClosestPoint(e.Location);
        // }
        else if (e.Button == MouseButtons.Right)
        {
            ShowContextMenu(e.Location);
        }
        Refresh();
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
        if (start == null && end == null)
        {
            MessageUtil.ShowInfo("There are no points to reset.", "Information");
            return;
        }
        start = null;
        end = null;
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

    private void RemoveClosestPoint(Point pos)
    {
        var points = new List<ValuePoint>();
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
        Refresh();
    }

    private void Refresh()
    {
        canvasPanel.Refresh();
    }
    
    private void ShowContextMenu(Point point)
    {
        var contextMenu = new ContextMenuStrip();
        contextMenu.Items.Add("Remove Closest Point", null, (_, _) => RemoveClosestPoint(point));
        contextMenu.Show(canvasPanel, point);
    }
}