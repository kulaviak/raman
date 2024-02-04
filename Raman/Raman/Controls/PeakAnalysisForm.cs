using Raman.Drawing;

namespace Raman.Controls;

public partial class PeakAnalysisForm : Form
{
    private readonly PeakAnalysisLayer peakAnalysisLayer;
    
    private const string INITIAL_DIRECTORY_PATH = "C:\\tmp";
        
    private const string FILE_DIALOG_FILTER = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
        
    public PeakAnalysisForm(PeakAnalysisLayer peakAnalysisLayer)
    {
        this.peakAnalysisLayer = peakAnalysisLayer;
        InitializeComponent();
    }

    private void btnImportPoints_Click(object sender, EventArgs e)
    {
        try
        {
            ImportPoints();
        }
        catch (Exception ex)
        {
            FormUtil.ShowErrorOnUserAction("Importing baseline points failed.", "Error", ex);
        }
    }

    private void ImportPoints()
    {
        // using (var openFileDialog = new OpenFileDialog())
        // {
        //     openFileDialog.InitialDirectory = INITIAL_DIRECTORY_PATH;
        //     openFileDialog.Filter = FILE_DIALOG_FILTER; 
        //     openFileDialog.FilterIndex = 1;
        //     openFileDialog.Multiselect = false;
        //     if (openFileDialog.ShowDialog() == DialogResult.OK)
        //     {
        //         var filePaths = openFileDialog.FileNames.ToList();
        //         if (filePaths.Count == 0)
        //         {
        //             FormUtil.ShowUserError("No file was selected.", "No file selected");
        //             return;
        //         }
        //         try
        //         {
        //             ImportPointsInternal(filePaths.First());
        //         }
        //         catch (Exception ex)
        //         {
        //             FormUtil.ShowAppError($"Importing baseline correction points failed. Error: {ex.Message}", "Import failed", ex);
        //         }
        //     }
        // }
    }

    private void ImportPointsInternal(string filePath)
    {
        // var points = new SingleSpectrumFileReader(filePath).TryReadFile();
        // _peakAnalysisLayer.ImportPoints(points);
    }

    private void btnExportPoints_Click(object sender, EventArgs e)
    {
        // try
        // {
        //     ExportPoints();
        // }
        // catch (Exception ex)
        // {
        //     FormUtil.ShowErrorOnUserAction("Exporting points failed.", "Error", ex);
        // }
    }

    private void ExportPoints()
    {
        // using (var saveFileDialog = new SaveFileDialog())
        // {
        //     saveFileDialog.InitialDirectory = INITIAL_DIRECTORY_PATH;
        //     saveFileDialog.Filter = FILE_DIALOG_FILTER; 
        //     saveFileDialog.FilterIndex = 1;
        //     if (saveFileDialog.ShowDialog() == DialogResult.OK)
        //     {
        //         var filePaths = saveFileDialog.FileNames.ToList();
        //         if (filePaths.Count == 0)
        //         {
        //             FormUtil.ShowUserError("No file was selected.", "No file selected");
        //             return;
        //         }
        //         try
        //         {
        //             var filePath = filePaths.First();
        //             _peakAnalysisLayer.ExportPoints(filePath);
        //             FormUtil.ShowInfo("Points were exported successfully.", "Export finished");
        //         }
        //         catch (Exception ex)
        //         {
        //             FormUtil.ShowAppError($"Exporting baseline correction points failed. Error: {ex.Message}", "Export failed", ex);
        //         }
        //     }
        // }
    }

    private void btnDoBaselineCorrection_Click(object sender, EventArgs e)
    {
        // try
        // {
        //     _peakAnalysisLayer.CorrectBaseline();
        // }
        // catch (Exception ex)
        // {
        //     FormUtil.ShowErrorOnUserAction("Baseline correction failed.", "Error", ex);
        // }
    }

    private void btnUndoBaselineCorrection_Click(object sender, EventArgs e)
    {
        // _peakAnalysisLayer.UndoBaselineCorrection();
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
        // try
        // {
        //     _peakAnalysisLayer.Reset();
        // }
        // catch (Exception ex)
        // {
        //     FormUtil.ShowErrorOnUserAction("Reset failed.", "Error", ex);
        // }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        // try
        // {
        //     Close();
        // }
        // catch (Exception ex)
        // {
        //     FormUtil.ShowErrorOnUserAction("Closing form failed.", "Error", ex);
        // }
    }

    private void BaselineCorrectionForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        // if (_peakAnalysisLayer.CorrectionPoints.Any() && !_peakAnalysisLayer.IsBaselineCorrected)
        // {
        //     if (FormUtil.ShowQuestion("Do you really want to exit Baseline Correction and loose selected correction points?",
        //             "Confirmation") == DialogResult.No)
        //     {
        //         e.Cancel = true;
        //     }
        // }
    }

    private void btnExportCorrectedCharts_Click(object sender, EventArgs e)
    {
        // try
        // {
        //     _peakAnalysisLayer.ExportCorrectedCharts();
        // }
        // catch (Exception ex)
        // {
        //     FormUtil.ShowErrorOnUserAction("Export corrected spectra failed.", "Error", ex);
        // }
    }
}