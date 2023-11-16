using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Raman.Core;

namespace Raman
{
    public partial class MainForm : Form
    {
        private List<Chart> _charts = new List<Chart>();

        public MainForm()
        {
            InitializeComponent();
            LoadDemoSpectrum();
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            Close();            
        }

        private void RamanForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // if (MessageBox.Show("Do you really want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo,
            //         MessageBoxIcon.Question) == DialogResult.No)
            // {
            //     e.Cancel = true;
            // }
        }

        private void miOpenFiles_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\"; 
                openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"; 
                openFileDialog.FilterIndex = 1;
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var filePaths = openFileDialog.FileNames.ToList();
                    if (filePaths.Count == 0)
                    {
                        MessageBox.Show("No files were selected.", "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                    OpenFiles(filePaths);
                }
            }
        }

        private void OpenFiles(List<string> filePaths)
        {
            var ignoredLines = new List<string>();
            foreach (var filePath in filePaths)
            {
                var fileReader = new FileReader(filePath);
                var points = fileReader.TryReadFile();
                _charts.Add(new Chart(points));
                if (points.Count < 2)
                {
                    MessageBox.Show($"File {filePath} has less 2 points. File is ignored.", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    continue;
                }
                if (fileReader.IgnoredLines.Count != 0)
                {
                    ignoredLines.AddRange(fileReader.IgnoredLines);
                }
            }

            if (ignoredLines.Count != 0)
            {
                var maxCount = 10;
                if (ignoredLines.Count > maxCount) ignoredLines = ignoredLines.Take(maxCount).ToList();
                var str = string.Join("\n", ignoredLines);
                MessageBox.Show($"Following lines were ignored (Showing max {maxCount} lines): {str}", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            // to enforce paint event, when form is resized
            Invalidate();
        }

        private void _mainPanel_Paint(object sender, PaintEventArgs e)
        {
            new Drawer().Draw(_charts, e.Graphics, _mainPanel.Width, _mainPanel.Height);
        }

        private void LoadDemoSpectrum()
        {
            var filePath = "c:/github/kulaviak/raman/data/spectrum.txt";
            var fileReader = new FileReader(filePath);
            var points = fileReader.TryReadFile();
            _charts.Add(new Chart(points));
        }
    }
}