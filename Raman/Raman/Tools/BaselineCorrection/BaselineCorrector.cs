namespace Raman.Tools.BaselineCorrection;

public class BaselineCorrector
{
    /// <summary>
    /// Expects that baseline points are calculated exactly at same places as spectrum points and this is exactly what SplineBaselineCalculator does.
    /// </summary>
    public List<ValuePoint> CorrectSpectrumByBaseline(List<ValuePoint> spectrumPoints, List<ValuePoint> baselinePoints)
    {
        var ret = new List<ValuePoint>();
        foreach (var spectrumPoint in spectrumPoints)
        {
            var baselinePoint = baselinePoints.FirstOrDefault(point => point.X == spectrumPoint.X);
            var correctedPoint = baselinePoint != null ? new ValuePoint(spectrumPoint.X, spectrumPoint.Y - baselinePoint.Y) : spectrumPoint;
            ret.Add(correctedPoint);
        }
        return ret;
    }
}