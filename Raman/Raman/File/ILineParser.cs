namespace Raman.File;

public interface ILineParser
{
    /// <summary>
    /// Return nullable list of doubles, because we need the information, that some values are missing.
    /// </summary>
    public List<double?> ParseLine(string line);
}