using Raman.Drawing;

namespace Raman.Controls;

public class StatusStripLayer(CanvasCoordSystem coordSystem, AppStatusStrip statusStrip, CanvasPanel canvasPanel) : LayerBase(coordSystem)
{

    private const decimal MAX_ALLOWED_DISTANCE_FOR_CLOSEST_SPECTRUM = 100;
    
    public override void HandleMouseMove(object sender, MouseEventArgs e)
    {
        var spectrum = new ClosestSpectrumCalculator().GetClosestSpectrum(canvasPanel.VisibleSpectra, e.Location, canvasPanel.CoordSystem, MAX_ALLOWED_DISTANCE_FOR_CLOSEST_SPECTRUM);
        var positionText = GetPositionText(e.Location);
        var text = spectrum != null ? $"{positionText}     {spectrum.Name}" : positionText;
        statusStrip.ShowText(text);
    }
    
    private string GetPositionText(Point location)
    {
        var position = CoordSystem.ToValuePoint(location.X, location.Y);
        // Showing superscripts on label https://stackoverflow.com/questions/19682459/superscript-label-or-form-name
        var ret = $"X Axis - Wavenumber (cm\u207B\u00B9): {Util.Format(position.X, 0)}  Y Axis - Intensity (cnts): {Util.Format(position.Y, 0)}";
        return ret;
    }
 }