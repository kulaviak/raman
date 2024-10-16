using Raman.Drawing;

namespace Raman.Core;

public class Spectrum
{
    public string Name { get; }

    private readonly List<ValuePoint> points;

    public List<ValuePoint> Points => points;

    public bool IsBaselineCorrected { get; set; }

    public bool IsVisible { get; set; } = true;

    public Spectrum(List<ValuePoint> points, string name)
    {
        this.points = points;
        Name = name;
    }

    public Spectrum(List<ValuePoint> points, string name, bool isBaselineCorrected, bool isVisible)
    {
        this.points = points;
        Name = name;
        IsBaselineCorrected = isBaselineCorrected;
        IsVisible = isVisible;
    }

    public double? GetValue(double x)
    {
        for (var i = 0; i < Points.Count - 1; i++)
        {
            var nextPoint = Points[i + 1];
            var point = points[i];
            if (point.X == x)
            {
                return point.Y;
            }
            if (point.X < x && x <= nextPoint.X)
            {
                var ret = GetInterpolatedValue(point, nextPoint, x);
                return ret;
            }
        }
        return null;
    }
    
    public void Draw(CanvasCoordSystem coordSystem, Graphics graphics)
    {
        new CanvasDrawer(coordSystem, graphics).DrawLines(points, Pens.Blue);
    }

    public override string ToString()
    {
        return $"Name: {Name}, Points.Count: {points.Count}, IsVisible: {IsVisible}, IsBaselineCorrected: {IsBaselineCorrected}";
    }
    
    public Spectrum DeepClone()
    {
        var clonedPoints = Points.Select(point => point.DeepClone()).ToList();
        return new Spectrum(clonedPoints, Name, IsBaselineCorrected, IsVisible);
    }
    
    private static double? GetInterpolatedValue(ValuePoint first, ValuePoint second, double x)
    {
        var diffX = second.X - first.X;
        var diffY = second.Y - first.Y;
        var ret = first.Y + (diffY / diffX) * (x - first.X);
        return ret;
    }
}