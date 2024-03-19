using Raman.Drawing;

namespace Raman.Core;

/// <summary>
/// Calculates closest spectrum to the mouse position.
/// </summary>
public class ClosestSpectrumCalculator
{

    public Spectrum GetClosestSpectrum(List<Spectrum> spectra, Point point, CanvasCoordSystem coordSystem, decimal maxAllowedDistance = decimal.MaxValue)
    {
        var minimumDistance = double.MaxValue;
        Spectrum ret = null;
        foreach (var spectrum in spectra)
        {
            var distance = GetDistance(spectrum, point, coordSystem);
            if (distance < minimumDistance && distance < (double) maxAllowedDistance)
            {
                minimumDistance = distance;
                ret = spectrum;
            }
        }
        return ret;
    }

    private double GetDistance(Spectrum spectrum, Point point, CanvasCoordSystem coordSystem)
    {
        var ret = spectrum.Points.Min(spectrumPoint => Util.GetPixelDistance(coordSystem.ToPixelPoint(spectrumPoint), point));
        return ret;
    }
}