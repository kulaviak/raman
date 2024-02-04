using Point = Raman.Core.Point;

namespace Raman.Controls;

public class AppStatusStrip : StatusStrip
{
    private ToolStripStatusLabel _lblPosition;

    public AppStatusStrip()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        _lblPosition = new ToolStripStatusLabel();
        Items.AddRange(new ToolStripItem[] {_lblPosition});   
    }

    public void ShowPosition(Point position)
    {
        _lblPosition.Text = $"X: {Util.Format(position.X, 0)}  Y: {Util.Format(position.Y, 0)}";
    }
}