using Raman.Drawing;

namespace Raman.Controls;

public class StatusStripLayer : LayerBase
{
    private readonly AppStatusStrip statusStrip;
    
    private readonly CanvasPanel canvasPanel;

    public StatusStripLayer(CanvasCoordSystem coordSystem, AppStatusStrip statusStrip, CanvasPanel canvasPanel) : base(coordSystem)
    {
        this.statusStrip = statusStrip;
        this.canvasPanel = canvasPanel;
    }

    private const double MAX_ALLOWED_DISTANCE_FOR_CLOSEST_SPECTRUM = 50;
    
    public override void HandleMouseMove(object sender, MouseEventArgs e)
    {
        var spectrum = new NewClosestSpectrumCalculator().GetClosestSpectrum(canvasPanel.VisibleSpectra, e.Location, CoordSystem, MAX_ALLOWED_DISTANCE_FOR_CLOSEST_SPECTRUM);
        var positionText = GetPositionText(e.Location);
        var text = spectrum != null ? $"{positionText}     {spectrum.Name}" : positionText;
        statusStrip.ShowText(text);
    }
    
    private string GetPositionText(Point location)
    {
        var point = CoordSystem.ToValuePoint(location);
        // Showing superscripts on label https://stackoverflow.com/questions/19682459/superscript-label-or-form-name
        var ret = $"X Axis - Wavenumber (cm\u207B\u00B9): {Util.Format(point.X, 0)}  Y Axis - Intensity (cnts): {Util.Format(point.Y, 0)}";
        return ret;
    }
 }