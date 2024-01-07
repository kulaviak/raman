using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Raman.Core;
using Point = Raman.Core.Point;

namespace Raman.Controls
{
    public partial class BaselineCorrectionForm : Form
    {
        public event EventHandler<List<Point>> PointsImported;
        
        public event EventHandler<string> PointsExported;
        
        public event EventHandler BaselineCorrectionReseted;
        
        public event EventHandler DoBaselineCorrection;
        
        public event EventHandler UndoBaselineCorrection;
        
        private const string INITIAL_DIRECTORY_PATH = "C:\\tmp";
        
        private const string FILE_DIALOG_FILTER = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
        
        public BaselineCorrectionForm()
        {
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
            PointsImported?.Invoke(this, points);
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
                        PointsExported(this, filePath);
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
            DoBaselineCorrection?.Invoke(this, null);
        }

        private void btnUndoBaselineCorrection_Click(object sender, EventArgs e)
        {
            UndoBaselineCorrection?.Invoke(this, null);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            BaselineCorrectionReseted?.Invoke(this, null);
        }
    }
}
