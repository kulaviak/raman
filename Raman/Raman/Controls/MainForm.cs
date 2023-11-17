﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Raman.Core;
using Raman.Drawing;
using Point = System.Drawing.Point;

namespace Raman
{
    // window zoom made according to Chat GTP query How to implement zoom to area in graphics in windows forms
    public partial class MainForm : Form
    {
        private List<Chart> _charts = new List<Chart>();
        
        private bool _isZooming;
        
        private Point _zoomStart;
        
        private Rectangle _zoomRectangle;

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
        
        private void _mainPanel_Resize(object sender, EventArgs e)
        {
            Refresh();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            Refresh();
        }

        private void btnOpenFiles_Click(object sender, EventArgs e)
        {
            OpenFiles();
        }

        private void btnZoomWindow_Click(object sender, EventArgs e)
        {
            ZoomWindow();
        }

        private void ZoomWindow()
        {
        }

        private void zoomToOriginalSize_Click(object sender, EventArgs e)
        {

        }

        private void miZoomWindow_Click(object sender, EventArgs e)
        {
            ZoomWindow();
        }

        private void miZoomToOriginalSize_Click(object sender, EventArgs e)
        {

        }

        private void miRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void _mainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isZooming = true;
                _zoomStart = e.Location;
            }
        }

        private void _mainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isZooming)
            {
                var x = Math.Min(_zoomStart.X, e.X);
                var y = Math.Min(_zoomStart.Y, e.Y);
                var width = Math.Abs(_zoomStart.X - e.X);
                var height = Math.Abs(_zoomStart.Y - e.Y);
                _zoomRectangle = new Rectangle(x, y, width, height);
                Refresh(); 
            }
        }

        private void _mainPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _isZooming)
            {
                _isZooming = false;
                var zoomedArea = new RectangleF(
                    _zoomRectangle.X / (float) _mainPanel.Width,
                    _zoomRectangle.Y / (float) _mainPanel.Height,
                    _zoomRectangle.Width / (float) _mainPanel.Width,
                    _zoomRectangle.Height / (float) _mainPanel.Height
                );
                ZoomToArea(zoomedArea);
            }
        }
    }
}