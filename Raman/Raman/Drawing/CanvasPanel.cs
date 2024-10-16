using System.ComponentModel;
using Raman.Controls;
using Raman.Tools.BaselineCorrection;
using Raman.Tools.CutOff;
using Raman.Tools.PeakAnalysis;
using Raman.View;

namespace Raman.Drawing;

/// <summary>
/// Canvas panel is responsible for drawing spectra and handling mouse events.
/// It is composed from layers and each layer is responsible for particular functionality. For example BaselineCorrectionLayer is
/// responsible for drawing baseline points and handling mouse events when user is in baseline correction mode.
/// </summary>
public class CanvasPanel : Panel
{
    private List<Spectrum> spectra = new List<Spectrum>();
    
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<Spectrum> Spectra
    {
        get { return spectra; }
        set
        {
            spectra = value;
        }
    }
    
    public List<Spectrum> VisibleSpectra => Spectra.Where(x => x.IsVisible).ToList();

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
            if (CutOffLayer != null)
            {
                CutOffLayer.CoordSystem = value;
            }
            if (MouseZoomLayer != null)
            {
                MouseZoomLayer.CoordSystem = value;
            }
            if (MeasureLayer != null)
            {
                MeasureLayer.CoordSystem = value;
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
    
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public CutOffLayer CutOffLayer { get; set; }
    
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public MouseZoomLayer MouseZoomLayer { get; set; }
    
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public MeasureLayer MeasureLayer { get; set; }
    
    public CanvasPanel()
    {
        InitializeComponent();
        // use double buffering to avoid flickering 
        DoubleBuffered = true;
    }
    
    public void SetCoordSystemToShowAllSpectra()
    {
        CoordSystem = CoordSystemCalculator.GetCoordSystemToShowAllSpectra(spectra, Width, Height);
    }
    
    public void ZoomToSeeAllSpectra()
    {
        CoordSystem = CoordSystemCalculator.GetCoordSystemToShowAllSpectra(spectra, Width, Height);
        DoRefresh();
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
    
    public void SetLayersToDefaultState(AppStatusStrip statusStrip)
    {
        BaselineCorrectionLayer = null;
        PeakAnalysisLayer = null;
        ZoomToWindowLayer = null;
        CutOffLayer = null;
        MeasureLayer = null;
        StatusStripLayer = new StatusStripLayer(CoordSystem, statusStrip, this);
        MouseZoomLayer = new MouseZoomLayer(this);
    }
    
    public void CancelMeasureMode()
    {
        MeasureLayer = null;
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
        MouseWheel += OnMouseWheel;
        Resize += HandleResize;
    }
    
    private void OnMouseWheel(object sender, MouseEventArgs e)
    {
        MouseZoomLayer?.HandleMouseWheel(sender, e);
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
            ClipGraphicsToOnlySpectrumArea(graphics);
            foreach (var spectrum in (IList<Spectrum>) spectra)
            {
                if (spectrum.IsVisible)
                {
                    spectrum.Draw(CoordSystem, graphics);
                }
            }
            graphics.ResetClip();
            DrawXAxis(graphics);
            DrawYAxis(graphics);
            DrawZeroYLevel(graphics);
        }
        ZoomToWindowLayer?.Draw(graphics);
        BaselineCorrectionLayer?.Draw(graphics);
        PeakAnalysisLayer?.Draw(graphics);
        CutOffLayer?.Draw(graphics);
        MeasureLayer?.Draw(graphics);
    }

    private void DrawZeroYLevel(Graphics graphics)
    {
        new ZeroYLevel(CoordSystem, graphics).Draw();
    }

    /// <summary>
    /// Clip only to spectrum area => nothing will be drawn outside of the axes.
    /// </summary>
    private void ClipGraphicsToOnlySpectrumArea(Graphics graphics)
    {
        var x = (int) CoordSystem.LeftBorder;
        var y = (int) CoordSystem.TopBorder;
        var width = (int) CoordSystem.PixelWidth;
        var height = (int) CoordSystem.PixelHeight;
        var spectrumRectangle = new Rectangle(x, y, width, height);
        graphics.SetClip(spectrumRectangle);
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
        UpdateCoordSystemToNewSize();
        DoRefresh();
    }

    private void UpdateCoordSystemToNewSize()
    {
        if (CoordSystem != null)
        {
            CoordSystem = new CanvasCoordSystem(Width, Height, CoordSystem.MinX, CoordSystem.MaxX, CoordSystem.MinY, coordSystem.MaxY);
        }
    }

    private void HandleMouseDown(object sender, MouseEventArgs e)
    {
        ZoomToWindowLayer?.HandleMouseDown(sender, e);
        MeasureLayer?.HandleMouseDown(sender, e);
        var isInZoomMode = ZoomToWindowLayer != null;
        var isInMeasureMode = MeasureLayer != null;
        if (!isInZoomMode && !isInMeasureMode)
        {
            BaselineCorrectionLayer?.HandleMouseDown(sender, e);
            PeakAnalysisLayer?.HandleMouseDown(sender, e);
            CutOffLayer?.HandleMouseDown(sender, e);
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
        MouseZoomLayer?.HandleMouseMove(sender, e);
        MeasureLayer?.HandleMouseMove(sender, e);
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
        MouseZoomLayer?.HandleMouseUp(sender, e);
    }
}