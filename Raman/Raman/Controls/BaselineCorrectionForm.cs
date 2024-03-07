using Raman.Drawing;

namespace Raman.Controls;

public partial class BaselineCorrectionForm : Form
{
    private readonly BaselineCorrectionLayer baselineCorrectionLayer;
    
    private const string FILE_DIALOG_FILTER = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
        
    public BaselineCorrectionForm(BaselineCorrectionLayer baselineCorrectionLayer, bool areBaselineEndsExtended,
        bool areCorrectionPointsAdjusted)
    {
        this.baselineCorrectionLayer = baselineCorrectionLayer;
        InitializeComponent();
        cbAreBaselineEndsExtended.Checked = areBaselineEndsExtended;
        cbAreCorrectionPointsAdjusted.Checked = areCorrectionPointsAdjusted;
    }

    private void btnImportPoints_Click(object sender, EventArgs e)
    {
        try
        {
            ImportPoints();
        }
        catch (Exception ex)
        {
            MessageUtil.ShowAppError("Importing baseline points failed.", "Error", ex);
        }
    }

    private void ImportPoints()
    {
        using (var openFileDialog = new OpenFileDialog())
        {
            openFileDialog.Filter = FILE_DIALOG_FILTER; 
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePaths = openFileDialog.FileNames.ToList();
                if (filePaths.Count == 0)
                {
                    MessageUtil.ShowUserError("No file was selected.", "No file selected");
                    return;
                }
                try
                {
                    ImportPointsInternal(filePaths.First());
                }
                catch (Exception ex)
                {
                    MessageUtil.ShowAppError($"Importing baseline correction points failed. Error: {ex.Message}", "Import failed", ex);
                }
            }
        }
    }

    private void ImportPointsInternal(string filePath)
    {
        var points = new SingleSpectrumFileReader(filePath).TryReadFile();
        baselineCorrectionLayer.ImportPoints(points);
    }

    private void btnExportPoints_Click(object sender, EventArgs e)
    {
        try
        {
            ExportPoints();
        }
        catch (Exception ex)
        {
            MessageUtil.ShowAppError("Exporting points failed.", "Error", ex);
        }
    }

    private void ExportPoints()
    {
        using (var saveFileDialog = new SaveFileDialog())
        {
            saveFileDialog.Filter = FILE_DIALOG_FILTER; 
            saveFileDialog.FilterIndex = 1;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePaths = saveFileDialog.FileNames.ToList();
                if (filePaths.Count == 0)
                {
                    MessageUtil.ShowUserError("No file was selected.", "No file selected");
                    return;
                }
                try
                {
                    var filePath = filePaths.First();
                    baselineCorrectionLayer.ExportPoints(filePath);
                    MessageUtil.ShowInfo("Points were exported successfully.", "Export finished");
                }
                catch (Exception ex)
                {
                    MessageUtil.ShowAppError($"Exporting baseline correction points failed. Error: {ex.Message}", "Export failed", ex);
                }
            }
        }
    }

    private void btnDoBaselineCorrection_Click(object sender, EventArgs e)
    {
        try
        {
            baselineCorrectionLayer.CorrectBaseline();
        }
        catch (Exception ex)
        {
            MessageUtil.ShowAppError("Baseline correction failed.", "Error", ex);
        }
    }

    private void btnUndoBaselineCorrection_Click(object sender, EventArgs e)
    {
        baselineCorrectionLayer.UndoBaselineCorrection();
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            baselineCorrectionLayer.Reset();
        }
        catch (Exception ex)
        {
            MessageUtil.ShowAppError("Reset failed.", "Error", ex);
        }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            Close();
        }
        catch (Exception ex)
        {
            MessageUtil.ShowAppError("Closing form failed.", "Error", ex);
        }
    }

    private void BaselineCorrectionForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (baselineCorrectionLayer.CorrectionPoints.Any())
        {
            if (MessageUtil.ShowQuestion("Do you really want to exit Baseline Correction and loose selected correction points?",
                    "Confirmation") == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }

    private void btnExportCorrectedCharts_Click(object sender, EventArgs e)
    {
        try
        {
            baselineCorrectionLayer.ExportCorrectedCharts();
        }
        catch (Exception ex)
        {
            MessageUtil.ShowAppError("Export corrected spectra failed.", "Error", ex);
        }
    }

    private void cbAreBaselineEndExtended_CheckedChanged(object sender, EventArgs e)
    {
        var isChecked = ((CheckBox) sender).Checked;
        baselineCorrectionLayer.AreBaselineEndsExtended = isChecked;
        AppSettings.AreBaselineEndsExtended = isChecked;
    }

    private void cbAreCorrectionPointsAdjusted_CheckedChanged(object sender, EventArgs e)
    {
        var isChecked = ((CheckBox) sender).Checked;
        baselineCorrectionLayer.AreCorrectionPointsAdjusted = isChecked;
        AppSettings.AreCorrectionPointsAdjusted = isChecked;
    }
}