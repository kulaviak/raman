using Microsoft.Extensions.Configuration;

namespace Raman.Core;

public static class AppSettings
{

    private static IConfiguration _configuration;

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

    public static bool AreBaselineEndsExtended
    {
        get => GetBool("AreBaselineEndsExtended") ?? false;
        set => SetBool("AreBaselineEndsExtended", value);
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
    
    /// <summary>
    /// Use '.' It is universal decimal separator
    /// </summary>
    public static string DecimalSeparator => ".";
    
    /// <summary>
    /// Use ',' to comply with CSV definition.
    /// </summary>
    public static string CsvSeparator => ",";

    private static IConfiguration Configuration
    {
        get
        {
            if (_configuration == null)
            {
                var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                _configuration = builder.Build();
            }
            return _configuration;
        }
    }


    public static string Get(string key)
    {
        var ret = Configuration[key];
        return ret;
    }

    public static void Set(string key, string value)
    {
        Configuration[key] = value;
    }

    public static bool? GetBool(string key)
    {
        var str = Configuration[key];
        if (Boolean.TryParse(str, out var ret))
        {
            return ret;
        }
        return null;
    }
    
    public static void SetBool(string key, bool value)
    {
        Configuration[key] = value.ToString();
    }
        
    // public static int? GetInt(string key)
    // {
    //     var str = Configuration[key];
    //     if (Int32.TryParse(str, out var ret))
    //     {
    //         return ret;
    //     }
    //     return null;
    // }
}