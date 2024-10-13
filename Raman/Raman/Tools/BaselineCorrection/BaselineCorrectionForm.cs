using Raman.Controls;
using Raman.File;

namespace Raman.Tools.BaselineCorrection;

public partial class BaselineCorrectionForm : Form
{
    private readonly BaselineCorrectionLayer baselineCorrectionLayer;

    private const string FILE_DIALOG_FILTER = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

    public BaselineCorrectionForm(BaselineCorrectionLayer baselineCorrectionLayer,
        bool areCorrectionPointsAdjusted)
    {
        this.baselineCorrectionLayer = baselineCorrectionLayer;
        InitializeComponent();
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

                var filePath = filePaths.First();
                var points = new SingleSpectrumFileReader(filePath).TryReadFile();
                baselineCorrectionLayer.ImportPoints(points);
            }
        }
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
                var filePath = filePaths.First();
                if (!baselineCorrectionLayer.CorrectionPoints.Any())
                {
                    MessageUtil.ShowUserError("There are no baseline points to export.", "Error");
                    return;
                }
                baselineCorrectionLayer.ExportPoints(filePath);
                MessageUtil.ShowInfo("Points were exported successfully.", "Export finished");
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

    private void btnExportCorrectedSpectra_Click(object sender, EventArgs e)
    {
        try
        {
            baselineCorrectionLayer.ExportCorrectedSpectra();
        }
        catch (Exception ex)
        {
            MessageUtil.ShowAppError("Export corrected spectra failed.", "Error", ex);
        }
    }
    
    private void cbAreCorrectionPointsAdjusted_Click(object sender, EventArgs e)
    {
        // checkbox has Autocheck = false, because I want to be able to set the checked property programmatically and that's why I have to 
        // react on Click event and not on CheckedChanged event
        var isChecked = !((CheckBox) sender).Checked;
        if (baselineCorrectionLayer.CorrectionPoints.Any())
        {
            MessageUtil.ShowUserError(
                "Option Adjust Correction Points can not be changed after some points are already selected. Please remove points and try again.",
                "Error");
            return;
        }
        cbAreCorrectionPointsAdjusted.Checked = isChecked;
        baselineCorrectionLayer.AreCorrectionPointsAdjusted = isChecked;
        AppSettings.AreCorrectionPointsAdjusted = isChecked;
    }

    private void cbAreCorrectedSpectraExportedToSeparateFiles_CheckedChanged(object sender, EventArgs e)
    {
        var isChecked = ((CheckBox) sender).Checked;
        baselineCorrectionLayer.AreCorrectedSpectraExportedToSeparateFiles = isChecked;
        AppSettings.AreCorrectedSpectraExportedToSeparateFiles = isChecked;
    }
}