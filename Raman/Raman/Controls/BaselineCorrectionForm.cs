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
        
        public BaselineCorrectionForm()
        {
            InitializeComponent();
        }

        private void btnImportPoints_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\"; 
                openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"; 
                openFileDialog.FilterIndex = 1;
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var filePaths = openFileDialog.FileNames.ToList();
                    if (filePaths.Count == 0)
                    {
                        MessageBox.Show("No file was selected.", "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                    ImportPointsInternal(filePaths.First());
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

        }

        private void btnDoBaselineCorrection_Click(object sender, EventArgs e)
        {

        }

        private void btnUndoBaselineCorrection_Click(object sender, EventArgs e)
        {

        }
    }
}
