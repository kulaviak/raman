namespace Raman.Core;

public class ExportedPeak(decimal height, decimal leftX, decimal rightX, decimal peakX)
{
    public decimal Height => height;

    public decimal LeftX => leftX;

    public decimal RightX => rightX;

    public decimal PeakX => peakX;

    public override string ToString()
    {
        return $"Height: {Height}, LeftX: {LeftX}, RightX: {RightX}, PeakX: {PeakX}";
    }
}
