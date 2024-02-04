using Point = Raman.Core.Point;

namespace Raman.Drawing;

public class Chart
{
    public string Name { get; }

    private readonly List<Point> points;

    public List<Point> Points => points;

    public bool IsBaselineCorrected { get; set; }

    public bool IsVisible { get; set; } = true;

    public Chart(List<Point> points, string name)
    {
        this.points = points;
        Name = name;
    }

    public void Draw(CanvasCoordSystem coordSystem, Graphics graphics)
    {
        new CanvasDrawer(coordSystem, graphics).DrawLines(points, Pens.Blue);
    }

    public override string ToString()
    {
        return $"Points.Count: {points.Count}";
    }
}