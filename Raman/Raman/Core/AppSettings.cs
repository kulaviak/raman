using Newtonsoft.Json;

namespace Raman.Core;

public static class AppSettings
{

    private static Dictionary<string, string> _keyValues;

    private static Dictionary<string, string> KeyValues
    {
        get
        {
            if (_keyValues == null)
            {
                try
                {
                    if (System.IO.File.Exists(_fileName))
                    {
                        var json = System.IO.File.ReadAllText(_fileName);
                        _keyValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                    }
                    else
                    {
                        _keyValues = new Dictionary<string, string>();
                    }
                }
                catch (Exception)
                {
                    // ignore
                    _keyValues = new Dictionary<string, string>();
                }
            }
            return _keyValues;
        }
    }

    private static string _fileName = "Raman.json";

    public static string SingleSpectrumOpenFileDirectory
    {
        get => Get("SingleSpectrumOpenFileDirectory");
        set => Set("SingleSpectrumOpenFileDirectory", value);
    }
    
    public static string MultipleSpectrumOpenFileDirectory
    {
        get => Get("MultipleSpectrumOpenFileDirectory");
        set => Set("MultipleSpectrumOpenFileDirectory", value);
    }
    
    public static string PeakAnalysisSaveFileDirectory
    {
        get => Get("PeakAnalysisSaveFileDirectory");
        set => Set("PeakAnalysisSaveFileDirectory", value);
    }
    
    public static bool AreCorrectionPointsAdjusted
    {
        get => GetBool("AreCorrectionPointsAdjusted") ?? false;
        set => SetBool("AreCorrectionPointsAdjusted", value);
    }

    public static bool AreCorrectedSpectraExportedToSeparateFiles
    {
        get => GetBool("AreCorrectedSpectraExportedToSeparateFiles") ?? false;
        set => SetBool("AreCorrectedSpectraExportedToSeparateFiles", value);
    }

    public static string BaselineCorrectionSaveFileDirectory
    {
        get => Get("BaselineCorrectionSaveFileDirectory");
        set => Set("BaselineCorrectionSaveFileDirectory", value);
    }
    
    public static int XDecimalPlaces
    {
        get => GetInt("XDecimalPlaces") ?? 0;
        set => SetInt("XDecimalPlaces", value);
    }
    
    public static int YDecimalPlaces
    {
        get => GetInt("YDecimalPlaces") ?? 2;
        set => SetInt("YDecimalPlaces", value);
    }
    
    /// <summary>
    /// Use '.' It is universal decimal separator
    /// </summary>
    public static string DecimalSeparator => ".";
    
    /// <summary>
    /// Use ',' to comply with CSV definition.
    /// </summary>
    public static string CsvSeparator => ",";

    public static string Get(string key)
    {
        var ret = KeyValues.Get(key);
        return ret;
    }

    public static void Set(string key, string value)
    {
        KeyValues[key] = value;
        Save();
    }

    private static void Save()
    {
        var json = JsonConvert.SerializeObject(KeyValues, Formatting.Indented);
        System.IO.File.WriteAllText(_fileName, json);
    }

    public static bool? GetBool(string key)
    {
        var str = Get(key);
        if (Boolean.TryParse(str, out var ret))
        {
            return ret;
        }
        return null;
    }
    
    public static void SetBool(string key, bool value)
    {
        Set(key, value.ToString());
    }
    
    public static int? GetInt(string key)
    {
        var str = Get(key);
        if (Int32.TryParse(str, out var ret))
        {
            return ret;
        }
        return null;
    }
    
    public static void SetInt(string key, int value)
    {
        Set(key, value.ToString());
    }
}