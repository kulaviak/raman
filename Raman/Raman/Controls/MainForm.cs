using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Raman.Controls;
using Raman.Core;
using Raman.Drawing;
using Point = Raman.Core.Point;

namespace Raman
{
    // window zoom made according to Chat GTP query How to implement zoom to area in graphics in windows forms
    public partial class MainForm : Form
    {
        private Form _sidePanel;
        
        public MainForm()
        {
            InitializeComponent();
            MinimumSize = new Size(800, 600);
            LoadDemoSpectrum();
            // LoadDemoSpectra();
            HideSidePanel();
        }

        private void HideSidePanel()
        {
            _sidePanel = null;
            splitContainer.Panel2Collapsed = true;
        }

        private void UpdateSplitter()
        {
            if (_sidePanel != null)
            {
                var borderWidth = 10;
                splitContainer.SplitterDistance = Width - _sidePanel.Width - borderWidth;
            }
        }

        private void ShowSidePanel(Form form)
        {
            form.TopLevel = false;
            _sidePanel = form;
            splitContainer.Panel2Collapsed = false;
            UpdateSplitter();
            form.Dock = DockStyle.Fill;
            splitContainer.Panel2.Controls.Clear();
            splitContainer.Panel2.Controls.Add(form);
            form.Show();
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
            OpenFiles();
        }

        private void OpenFiles()
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
                    OpenFilesInternal(filePaths);
                }
            }
        }

        private void OpenFilesInternal(List<string> filePaths)
        {
            var charts = new List<Chart>();
            var ignoredLines = new List<string>();
            foreach (var filePath in filePaths)
            {
                var fileReader = new OnePointPerLineFileReader(filePath);
                var points = fileReader.TryReadFile();
                charts.Add(new Chart(points));
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
            canvasPanel.SetCharts(charts);
        }
        
        private void LoadDemoSpectrum()
        {
            OpenFilesInternal(new List<string>{"c:/github/kulaviak/raman/data/spectrum.txt"});
        }
        
        private void LoadDemoSpectra()
        {
            var filePaths = new List<string>
            {
                "c:/github/kulaviak/raman/data/spektra/UHK-0-1-5/UHK-0-1-5_Y0_X0.txt",
                "c:/github/kulaviak/raman/data/spektra/UHK-0-1-5/UHK-0-1-5_Y0_X1.txt",
                "c:/github/kulaviak/raman/data/spektra/UHK-0-1-5/UHK-0-1-5_Y0_X3.txt",
                "c:/github/kulaviak/raman/data/spektra/UHK-0-1-5/UHK-0-1-5_Y0_X4.txt",
                "c:/github/kulaviak/raman/data/spektra/UHK-0-1-5/UHK-0-1-5_Y1_X0.txt",
                "c:/github/kulaviak/raman/data/spektra/UHK-0-1-5/UHK-0-1-5_Y1_X1.txt",
                "c:/github/kulaviak/raman/data/spektra/UHK-0-1-5/UHK-0-1-5_Y1_X2.txt",
                "c:/github/kulaviak/raman/data/spektra/UHK-0-1-5/UHK-0-1-5_Y1_X3.txt",
                "c:/github/kulaviak/raman/data/spektra/UHK-0-1-5/UHK-0-1-5_Y1_X4.txt"
            };
            OpenFilesInternal(filePaths);
        }
        
        private void miZoomWindow_Click(object sender, EventArgs e)
        {
            ZoomToWindow();
        }

        private void ZoomToWindow()
        {
            canvasPanel.IsZooming = true;
        }

        private void miZoomToOriginalSize_Click(object sender, EventArgs e)
        {
            canvasPanel.ZoomToOriginalSize();
        }

        private void miRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void miBaselineCorrection_Click(object sender, EventArgs e)
        {
            BaselineCorrection();
        }

        private void BaselineCorrection()
        {
            var form = new BaselineCorrectionForm();
            form.PointsImported += BaselineForm_PointsImported;
            form.PointsExported += BaselineForm_PointsExported;
            form.BaselineCorrectionReseted += BaselineForm_BaselineCorrectionReseted;
            form.Closed += BaselineForm_Closed;
            ShowSidePanel(form);
            canvasPanel.BaselineCorrectionLayer = new BaselineCorrectionLayer(canvasPanel.CoordSystem, canvasPanel);
        }

        private void BaselineForm_Closed(object sender, EventArgs e)
        {
            HideSidePanel();
        }

        private void BaselineForm_BaselineCorrectionReseted(object sender, EventArgs e)
        {
            canvasPanel.BaselineCorrectionLayer.Reset();
        }
        
        private void BaselineForm_PointsExported(object sender, string filePath)
        {
            canvasPanel.BaselineCorrectionLayer.ExportPoints(filePath);
        }
        
        private void BaselineForm_PointsImported(object sender, List<Point> points)
        {
            canvasPanel.BaselineCorrectionLayer.ImportPoint(points);
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void tsbOpenFiles_Click(object sender, EventArgs e)
        {
            OpenFiles();
        }

        private void tsbZoomToWindow_Click(object sender, EventArgs e)
        {
            ZoomToWindow();
        }

        private void tsbZoomToOriginalSize_Click(object sender, EventArgs e)
        {
            canvasPanel.ZoomToOriginalSize();
        }

        private void tsbBaselineCorrection_Click(object sender, EventArgs e)
        {
            BaselineCorrection();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            UpdateSplitter();
        }
    }
}