using Raman.Drawing;

namespace Raman.Controls;

public partial class BaselineCorrectionForm : Form
{
    private readonly BaselineCorrectionLayer _baselineCorrectionLayer;
    
    private const string INITIAL_DIRECTORY_PATH = "C:\\tmp";
        
    private const string FILE_DIALOG_FILTER = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
        
    public BaselineCorrectionForm(BaselineCorrectionLayer baselineCorrectionLayer)
    {
        _baselineCorrectionLayer = baselineCorrectionLayer;
        InitializeComponent();
    }

    private void btnImportPoints_Click(object sender, EventArgs e)
    {
        using (var openFileDialog = new OpenFileDialog())
        {
            openFileDialog.InitialDirectory = INITIAL_DIRECTORY_PATH;
            openFileDialog.Filter = FILE_DIALOG_FILTER; 
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePaths = openFileDialog.FileNames.ToList();
                if (filePaths.Count == 0)
                {
                    FormUtil.ShowUserError("No file was selected.", "No file selected");
                    return;
                }
                try
                {
                    ImportPointsInternal(filePaths.First());
                }
                catch (Exception ex)
                {
                    FormUtil.ShowAppError($"Importing baseline correction points failed. Error: {ex.Message}", "Import failed", ex);
                }
            }
        }
    }

    private void ImportPointsInternal(string filePath)
    {
        var points = new OnePointPerLineFileReader(filePath).TryReadFile();
        _baselineCorrectionLayer.ImportPoints(points);
    }

    private void btnExportPoints_Click(object sender, EventArgs e)
    {
        using (var saveFileDialog = new SaveFileDialog())
        {
            saveFileDialog.InitialDirectory = INITIAL_DIRECTORY_PATH;
            saveFileDialog.Filter = FILE_DIALOG_FILTER; 
            saveFileDialog.FilterIndex = 1;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePaths = saveFileDialog.FileNames.ToList();
                if (filePaths.Count == 0)
                {
                    FormUtil.ShowUserError("No file was selected.", "No file selected");
                    return;
                }
                try
                {
                    var filePath = filePaths.First();
                    _baselineCorrectionLayer.ExportPoints(filePath);
                    FormUtil.ShowInfo("Points were exported successfully.", "Export finished");
                }
                catch (Exception ex)
                {
                    FormUtil.ShowAppError($"Exporting baseline correction points failed. Error: {ex.Message}", "Export failed", ex);
                }
            }
        }
    }
        
    private void btnDoBaselineCorrection_Click(object sender, EventArgs e)
    {
        _baselineCorrectionLayer.CorrectBaseline();
    }

    private void btnUndoBaselineCorrection_Click(object sender, EventArgs e)
    {
        _baselineCorrectionLayer.UndoBaselineCorrection();
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
        _baselineCorrectionLayer.Reset();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void BaselineCorrectionForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (_baselineCorrectionLayer.CorrectionPoints.Any() && !_baselineCorrectionLayer.IsBaselineCorrected)
        {
            if (FormUtil.ShowQuestion("Do you really want to exit Baseline Correction and loose selected correction points?",
                    "Confirmation") == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}