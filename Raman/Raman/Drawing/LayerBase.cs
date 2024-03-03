namespace Raman.Drawing;

public class LayerBase(CanvasCoordSystem coordSystem)
{
    public CanvasCoordSystem CoordSystem { get; set; } = coordSystem;

    public virtual void HandleMouseMove(object sender, MouseEventArgs e) {}

    public virtual void HandleMouseDown(object sender, MouseEventArgs e) {}

    public virtual void HandleMouseUp(object sender, MouseEventArgs e) {}

    public virtual void HandleKeyPress(object sender, KeyEventArgs e) {}
    
    public virtual void Draw(Graphics graphics) {}

    public virtual void HandleMouseWheel(object sender, MouseEventArgs e) {}
}