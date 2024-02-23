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
    
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<Chart> Charts
    {
        get { return charts; }
        set
        {
            charts = value;
            CoordSystem = CoordSystemCalculator.GetCoordSystemToShowAllCharts(charts, Width, Height);
        }
    }

    public List<Chart> VisibleCharts => Charts.Where(x => x.IsVisible).ToList();

    private CanvasCoordSystem coordSystem;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public CanvasCoordSystem CoordSystem
    {
        get => coordSystem;
        set
        {
            coordSystem = value;
            // !!! This must be done for each layer
            if (BaselineCorrectionLayer != null)
            {
                BaselineCorrectionLayer.CoordSystem = value;
            }
            if (ZoomToWindowLayer != null)
            {
                ZoomToWindowLayer.CoordSystem = value;
            }
            if (StatusStripLayer != null)
            {
                StatusStripLayer.CoordSystem = value;
            }
            if (PeakAnalysisLayer != null)
            {
                PeakAnalysisLayer.CoordSystem = value;
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
        // use double buffering to avoid flickering 
        DoubleBuffered = true;
    }
    
    public void ZoomToOriginalSize()
    {
        CoordSystem = CoordSystemCalculator.GetCoordSystemToShowAllCharts(charts, Width, Height);
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
        Draw(e.Graphics);
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
        PeakAnalysisLayer?.Draw(graphics);
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
    
    private void HandleResize(object sender, EventArgs e)
    {
        CoordSystem = CoordSystemCalculator.GetCoordSystemToShowAllCharts(charts, Width, Height);
        DoRefresh();
    }

    private void HandleMouseDown(object sender, MouseEventArgs e)
    {
        ZoomToWindowLayer?.HandleMouseDown(sender, e);
        var isInZoomMode = ZoomToWindowLayer != null;
        if (!isInZoomMode)
        {
            BaselineCorrectionLayer?.HandleMouseDown(sender, e);
            PeakAnalysisLayer?.HandleMouseDown(sender, e);
        }
        Refresh();
    }

    private void HandleMouseMove(object sender, MouseEventArgs e)
    {
        ZoomToWindowLayer?.HandleMouseMove(sender, e);
        var isInZoomMode = ZoomToWindowLayer != null;
        if (!isInZoomMode)
        {
            BaselineCorrectionLayer?.HandleMouseMove(sender, e);
            PeakAnalysisLayer?.HandleMouseMove(sender, e);
        }
        StatusStripLayer?.HandleMouseMove(sender, e);
    }

    private void HandleMouseUp(object sender, MouseEventArgs e)
    {
        ZoomToWindowLayer?.HandleMouseUp(sender, e);
        var isInZoomMode = ZoomToWindowLayer != null;
        if (!isInZoomMode)
        {
            BaselineCorrectionLayer?.HandleMouseUp(sender, e);
            PeakAnalysisLayer?.HandleMouseUp(sender, e);
        }
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

    public void HandleKeyPress(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape && ZoomToWindowLayer != null)
        {
            UnsetZoomToWindowMode();
        }
        ZoomToWindowLayer?.HandleKeyPress(sender, e);
        BaselineCorrectionLayer?.HandleKeyPress(sender, e);
        StatusStripLayer?.HandleKeyPress(sender, e);
        PeakAnalysisLayer?.HandleKeyPress(sender, e);
    }
    
    public void NullAllLayers()
    {
        BaselineCorrectionLayer = null;
        PeakAnalysisLayer = null;
        ZoomToWindowLayer = null;
    }
}