using System.ComponentModel;

namespace Raman.Drawing;

/// <summary>
/// Canvas panel is responsible for drawing charts and handling mouse events.
/// It is composed from layers and each layer is responsible for particular functionality. For example BaselineCorrectionLayer is
/// responsible for drawing baseline points and handling mouse events when user is in baseline correction mode.
/// </summary>
public class CanvasPanel : Panel
{
    private List<Chart> charts = new List<Chart>();

    private const int PIXEL_SPACE = 25;
    
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<Chart> Charts
    {
        get { return charts; }
        set
        {
            charts = value;
            CoordSystem = GetCoordSystemFromCharts(charts);
        }
    }

    private Bitmap buffer;

    private Graphics bufferGraphics;

    private CanvasCoordSystem coordSystem;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public CanvasCoordSystem CoordSystem
    {
        get => coordSystem;
        set
        {
            coordSystem = value;
            if (BaselineCorrectionLayer != null)
            {
                BaselineCorrectionLayer.CoordSystem = value;
            }
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public StatusStripLayer StatusStripLayer { get; set; }
    
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PeakAnalysisLayer PeakAnalysisLayer { get; set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public BaselineCorrectionLayer BaselineCorrectionLayer { get; set; }
    
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ZoomToWindowLayer ZoomToWindowLayer { get; set; }
    
    public CanvasPanel()
    {
        InitializeComponent();
    }

    public void ZoomToOriginalSize()
    {
        CoordSystem = GetCoordSystemFromCharts(charts);
        DoRefresh();
    }

    private void DoRefresh()
    {
        Invalidate();
    }

    private void InitializeComponent()
    {
        Paint += HandlePaint;
        MouseDown += HandleMouseDown;
        MouseMove += HandleMouseMove;
        MouseUp += HandleMouseUp;
        Resize += HandleResize;
    }

    private void HandlePaint(object sender, PaintEventArgs e)
    {
        if (buffer == null)
        {
            InitializeBuffer();
        }

        Draw(bufferGraphics);
        // copy the content of the off-screen buffer to the form's graphics
        e.Graphics.DrawImage(buffer, 0, 0);
    }

    private void Draw(Graphics graphics)
    {
        graphics.Clear(Color.FromArgb(240, 240, 240));
        if (CoordSystem != null)
        {
            ClipGraphicsToOnlyChartArea(graphics);
            foreach (var chart in (IList<Chart>) charts)
            {
                chart.Draw(CoordSystem, graphics);
            }
            graphics.ResetClip();
            DrawXAxis(graphics);
            DrawYAxis(graphics);
        }
        ZoomToWindowLayer?.Draw(graphics);
        BaselineCorrectionLayer?.Draw(graphics);
    }

    /// <summary>
    /// Clip only to chart area => nothing will be drawn outside of the axes.
    /// </summary>
    private void ClipGraphicsToOnlyChartArea(Graphics graphics)
    {
        var x = (int) CoordSystem.LeftBorder;
        var y = (int) CoordSystem.TopBorder;
        var width = (int) CoordSystem.PixelWidth;
        var height = (int) CoordSystem.PixelHeight;
        var chartRectangle = new Rectangle(x, y, width, height);
        graphics.SetClip(chartRectangle);
    }

    private void DrawYAxis(Graphics graphics)
    {
        new YAxis(CoordSystem, graphics).Draw();
    }

    private void DrawXAxis(Graphics graphics)
    {
        new XAxis(CoordSystem, graphics).Draw();
    }

    private CanvasCoordSystem GetCoordSystemFromCharts(IList<Chart> charts)
    {
        if (charts.Count == 0)
        {
            return null;
        }

        var allPoints = charts.SelectMany(x => x.Points).ToList();
        var minX = allPoints.Min(point => point.X);
        var maxX = allPoints.Max(point => point.X);
        var minY = allPoints.Min(point => point.Y);
        var maxY = allPoints.Max(point => point.Y);
        
        // modify displayed range so the chart is not going to axes, but there is a distance to X or Y axis
        // it is needed when baseline correction points are selected, I want to often select points that is beyond chart range
        var spaceX = (maxX - minX) / Width * PIXEL_SPACE;
        var spaceY = (maxY - minY) / Height * PIXEL_SPACE;
        minX -= spaceX;
        maxX += spaceX;
        minY -= spaceY;
        maxY += spaceY;
        
        var coordSystem = new CanvasCoordSystem(Width, Height, minX, maxX, minY, maxY);
        return coordSystem;
    }

    private void InitializeBuffer()
    {
        buffer = new Bitmap(Width, Height);
        bufferGraphics = Graphics.FromImage(buffer);
    }

    private void HandleResize(object sender, EventArgs e)
    {
        CoordSystem = GetCoordSystemFromCharts(charts);
        // recreate the buffer when the form is resized
        buffer?.Dispose();
        if (bufferGraphics != null)
        {
            bufferGraphics.Dispose();
        }

        InitializeBuffer();
        DoRefresh();
    }

    private void HandleMouseDown(object sender, MouseEventArgs e)
    {
        ZoomToWindowLayer?.HandleMouseDown(sender, e);
        BaselineCorrectionLayer?.HandleMouseDown(sender, e);
        Refresh();
    }

    private void HandleMouseMove(object sender, MouseEventArgs e)
    {
        ZoomToWindowLayer?.HandleMouseMove(sender, e);
        BaselineCorrectionLayer?.HandleMouseMove(sender, e);
        StatusStripLayer?.HandleMouseMove(sender, e);
    }

    private void HandleMouseUp(object sender, MouseEventArgs e)
    {
        ZoomToWindowLayer?.HandleMouseUp(sender, e);
        BaselineCorrectionLayer?.HandleMouseUp(sender, e);
    }

    public CanvasCoordSystem GetCoordSystemForZoom(CanvasCoordSystem oldCoordSystem, Rectangle zoomRectangle)
    {
        var minX = oldCoordSystem.ToValueX(zoomRectangle.X);
        var maxX = oldCoordSystem.ToValueX(zoomRectangle.X + zoomRectangle.Width);
        var minY = oldCoordSystem.ToValueY(zoomRectangle.Y + zoomRectangle.Height);
        var maxY = oldCoordSystem.ToValueY(zoomRectangle.Y);
        var ret = new CanvasCoordSystem(Width, Height, minX, maxX, minY, maxY);
        return ret;
    }

    public void SetZoomToWindowMode()
    {
        ZoomToWindowLayer = new ZoomToWindowLayer(coordSystem, this);
    }
    
    public void UnsetZoomToWindowMode()
    {
        ZoomToWindowLayer = null;
        Refresh();
    }
}