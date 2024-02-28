using Raman.Drawing;

namespace Raman;

partial class MainForm
{
  /// <summary>
  /// Required designer variable.
  /// </summary>
  private System.ComponentModel.IContainer components = null;

  /// <summary>
  /// Clean up any resources being used.
  /// </summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && (components != null))
    {
      components.Dispose();
    }
    base.Dispose(disposing);
  }

  #region Windows Form Designer generated code

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
    this.menuStrip = new System.Windows.Forms.MenuStrip();
    this.miFile = new System.Windows.Forms.ToolStripMenuItem();
    this.miOpenSingleSpectrumFiles = new System.Windows.Forms.ToolStripMenuItem();
    this.miOpenMultiSpectrumFiles = new System.Windows.Forms.ToolStripMenuItem();
    this.miExit = new System.Windows.Forms.ToolStripMenuItem();
    this.miView = new System.Windows.Forms.ToolStripMenuItem();
    this.miZoomWindow = new System.Windows.Forms.ToolStripMenuItem();
    this.miZoomToOriginalSize = new System.Windows.Forms.ToolStripMenuItem();
    this.miTools = new System.Windows.Forms.ToolStripMenuItem();
    this.miBaselineCorrection = new System.Windows.Forms.ToolStripMenuItem();
    this.miPeakAnalysis = new System.Windows.Forms.ToolStripMenuItem();
    this.miCutOff = new System.Windows.Forms.ToolStripMenuItem();
    this.miHelp = new System.Windows.Forms.ToolStripMenuItem();
    this.miShowHelp = new System.Windows.Forms.ToolStripMenuItem();
    this.toolStrip = new System.Windows.Forms.ToolStrip();
    this.tsbOpenFiles = new System.Windows.Forms.ToolStripButton();
    this.tsbZoomToWindow = new System.Windows.Forms.ToolStripButton();
    this.tsbZoomToOriginalSize = new System.Windows.Forms.ToolStripButton();
    this.tsbBaselineCorrection = new System.Windows.Forms.ToolStripButton();
    this.tsbPeakAnalysis = new System.Windows.Forms.ToolStripButton();
    this.splitContainer = new System.Windows.Forms.SplitContainer();
    this.statusStrip = new Raman.Controls.AppStatusStrip();
    this.canvasPanel = new Raman.Drawing.CanvasPanel();
    this.tsbCutOff = new System.Windows.Forms.ToolStripButton();
    this.menuStrip.SuspendLayout();
    this.toolStrip.SuspendLayout();
    ((System.ComponentModel.ISupportInitialize) (this.splitContainer)).BeginInit();
    this.splitContainer.Panel1.SuspendLayout();
    this.splitContainer.SuspendLayout();
    this.SuspendLayout();
    // 
    // menuStrip
    // 
    this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.miFile, this.miView, this.miTools, this.miHelp});
    this.menuStrip.Location = new System.Drawing.Point(0, 0);
    this.menuStrip.Name = "menuStrip";
    this.menuStrip.Size = new System.Drawing.Size(800, 24);
    this.menuStrip.TabIndex = 0;
    this.menuStrip.Text = "menuStrip1";
    // 
    // miFile
    // 
    this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.miOpenSingleSpectrumFiles, this.miOpenMultiSpectrumFiles, this.miExit});
    this.miFile.Name = "miFile";
    this.miFile.Size = new System.Drawing.Size(37, 20);
    this.miFile.Text = "File";
    // 
    // miOpenSingleSpectrumFiles
    // 
    this.miOpenSingleSpectrumFiles.Name = "miOpenSingleSpectrumFiles";
    this.miOpenSingleSpectrumFiles.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
    this.miOpenSingleSpectrumFiles.Size = new System.Drawing.Size(261, 22);
    this.miOpenSingleSpectrumFiles.Text = "Open Single Spectrum Files";
    this.miOpenSingleSpectrumFiles.Click += new System.EventHandler(this.miOpenSingleSpectrumFiles_Click);
    // 
    // miOpenMultiSpectrumFiles
    // 
    this.miOpenMultiSpectrumFiles.Name = "miOpenMultiSpectrumFiles";
    this.miOpenMultiSpectrumFiles.Size = new System.Drawing.Size(261, 22);
    this.miOpenMultiSpectrumFiles.Text = "Open Multi Spectrum Files";
    this.miOpenMultiSpectrumFiles.Click += new System.EventHandler(this.miOpenMultiSpectrumFiles_Click);
    // 
    // miExit
    // 
    this.miExit.Name = "miExit";
    this.miExit.Size = new System.Drawing.Size(261, 22);
    this.miExit.Text = "Exit";
    this.miExit.Click += new System.EventHandler(this.miExit_Click);
    // 
    // miView
    // 
    this.miView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.miZoomWindow, this.miZoomToOriginalSize});
    this.miView.Name = "miView";
    this.miView.Size = new System.Drawing.Size(44, 20);
    this.miView.Text = "View";
    // 
    // miZoomWindow
    // 
    this.miZoomWindow.Name = "miZoomWindow";
    this.miZoomWindow.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
    this.miZoomWindow.Size = new System.Drawing.Size(231, 22);
    this.miZoomWindow.Text = "Zoom Window";
    this.miZoomWindow.Click += new System.EventHandler(this.miZoomWindow_Click);
    // 
    // miZoomToOriginalSize
    // 
    this.miZoomToOriginalSize.Name = "miZoomToOriginalSize";
    this.miZoomToOriginalSize.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
    this.miZoomToOriginalSize.Size = new System.Drawing.Size(231, 22);
    this.miZoomToOriginalSize.Text = "Zoom To Original Size";
    this.miZoomToOriginalSize.Click += new System.EventHandler(this.miZoomToOriginalSize_Click);
    // 
    // miTools
    // 
    this.miTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.miBaselineCorrection, this.miPeakAnalysis, this.miCutOff});
    this.miTools.Name = "miTools";
    this.miTools.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
    this.miTools.Size = new System.Drawing.Size(46, 20);
    this.miTools.Text = "Tools";
    // 
    // miBaselineCorrection
    // 
    this.miBaselineCorrection.Name = "miBaselineCorrection";
    this.miBaselineCorrection.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
    this.miBaselineCorrection.Size = new System.Drawing.Size(217, 22);
    this.miBaselineCorrection.Text = "Baseline Correction";
    this.miBaselineCorrection.Click += new System.EventHandler(this.miBaselineCorrection_Click);
    // 
    // miPeakAnalysis
    // 
    this.miPeakAnalysis.Name = "miPeakAnalysis";
    this.miPeakAnalysis.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
    this.miPeakAnalysis.Size = new System.Drawing.Size(217, 22);
    this.miPeakAnalysis.Text = "Peak Analysis";
    this.miPeakAnalysis.Click += new System.EventHandler(this.miPeakAnalysis_Click);
    // 
    // miCutOff
    // 
    this.miCutOff.Name = "miCutOff";
    this.miCutOff.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
    this.miCutOff.Size = new System.Drawing.Size(217, 22);
    this.miCutOff.Text = "Cut Off";
    this.miCutOff.Click += new System.EventHandler(this.miCutOff_Click);
    // 
    // miHelp
    // 
    this.miHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.miShowHelp});
    this.miHelp.Name = "miHelp";
    this.miHelp.Size = new System.Drawing.Size(44, 20);
    this.miHelp.Text = "Help";
    // 
    // miShowHelp
    // 
    this.miShowHelp.Name = "miShowHelp";
    this.miShowHelp.Size = new System.Drawing.Size(99, 22);
    this.miShowHelp.Text = "Help";
    this.miShowHelp.Click += new System.EventHandler(this.miHelp_Click);
    // 
    // toolStrip
    // 
    this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.tsbOpenFiles, this.tsbZoomToWindow, this.tsbZoomToOriginalSize, this.tsbBaselineCorrection, this.tsbPeakAnalysis, this.tsbCutOff});
    this.toolStrip.Location = new System.Drawing.Point(0, 24);
    this.toolStrip.Name = "toolStrip";
    this.toolStrip.Size = new System.Drawing.Size(800, 25);
    this.toolStrip.TabIndex = 1;
    this.toolStrip.Text = "toolStrip2";
    // 
    // tsbOpenFiles
    // 
    this.tsbOpenFiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
    this.tsbOpenFiles.Image = ((System.Drawing.Image) (resources.GetObject("tsbOpenFiles.Image")));
    this.tsbOpenFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
    this.tsbOpenFiles.Name = "tsbOpenFiles";
    this.tsbOpenFiles.Size = new System.Drawing.Size(23, 22);
    this.tsbOpenFiles.Text = "Open Spectrum Files";
    this.tsbOpenFiles.Click += new System.EventHandler(this.tsbOpenFiles_Click);
    // 
    // tsbZoomToWindow
    // 
    this.tsbZoomToWindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
    this.tsbZoomToWindow.Image = ((System.Drawing.Image) (resources.GetObject("tsbZoomToWindow.Image")));
    this.tsbZoomToWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
    this.tsbZoomToWindow.Name = "tsbZoomToWindow";
    this.tsbZoomToWindow.Size = new System.Drawing.Size(23, 22);
    this.tsbZoomToWindow.Text = "Zoom To Window";
    this.tsbZoomToWindow.Click += new System.EventHandler(this.tsbZoomToWindow_Click);
    // 
    // tsbZoomToOriginalSize
    // 
    this.tsbZoomToOriginalSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
    this.tsbZoomToOriginalSize.Image = ((System.Drawing.Image) (resources.GetObject("tsbZoomToOriginalSize.Image")));
    this.tsbZoomToOriginalSize.ImageTransparentColor = System.Drawing.Color.Magenta;
    this.tsbZoomToOriginalSize.Name = "tsbZoomToOriginalSize";
    this.tsbZoomToOriginalSize.Size = new System.Drawing.Size(23, 22);
    this.tsbZoomToOriginalSize.Text = "Zoom To Original Size";
    this.tsbZoomToOriginalSize.Click += new System.EventHandler(this.tsbZoomToOriginalSize_Click);
    // 
    // tsbBaselineCorrection
    // 
    this.tsbBaselineCorrection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
    this.tsbBaselineCorrection.Image = ((System.Drawing.Image) (resources.GetObject("tsbBaselineCorrection.Image")));
    this.tsbBaselineCorrection.ImageTransparentColor = System.Drawing.Color.Magenta;
    this.tsbBaselineCorrection.Name = "tsbBaselineCorrection";
    this.tsbBaselineCorrection.Size = new System.Drawing.Size(23, 22);
    this.tsbBaselineCorrection.Text = "Baseline Correction";
    this.tsbBaselineCorrection.Click += new System.EventHandler(this.tsbBaselineCorrection_Click);
    // 
    // tsbPeakAnalysis
    // 
    this.tsbPeakAnalysis.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
    this.tsbPeakAnalysis.Image = ((System.Drawing.Image) (resources.GetObject("tsbPeakAnalysis.Image")));
    this.tsbPeakAnalysis.ImageTransparentColor = System.Drawing.Color.Magenta;
    this.tsbPeakAnalysis.Name = "tsbPeakAnalysis";
    this.tsbPeakAnalysis.Size = new System.Drawing.Size(23, 22);
    this.tsbPeakAnalysis.Text = "toolStripButton1";
    this.tsbPeakAnalysis.ToolTipText = "Peak Analysis";
    this.tsbPeakAnalysis.Click += new System.EventHandler(this.tsbPeakAnalysis_Click);
    // 
    // splitContainer
    // 
    this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
    this.splitContainer.IsSplitterFixed = true;
    this.splitContainer.Location = new System.Drawing.Point(0, 49);
    this.splitContainer.Name = "splitContainer";
    // 
    // splitContainer.Panel1
    // 
    this.splitContainer.Panel1.Controls.Add(this.statusStrip);
    this.splitContainer.Panel1.Controls.Add(this.canvasPanel);
    this.splitContainer.Size = new System.Drawing.Size(800, 401);
    this.splitContainer.SplitterDistance = 590;
    this.splitContainer.TabIndex = 2;
    // 
    // statusStrip
    // 
    this.statusStrip.Location = new System.Drawing.Point(0, 379);
    this.statusStrip.Name = "statusStrip";
    this.statusStrip.Size = new System.Drawing.Size(590, 22);
    this.statusStrip.TabIndex = 1;
    this.statusStrip.Text = "appStatusStrip1";
    // 
    // canvasPanel
    // 
    this.canvasPanel.Dock = System.Windows.Forms.DockStyle.Fill;
    this.canvasPanel.Location = new System.Drawing.Point(0, 0);
    this.canvasPanel.Name = "canvasPanel";
    this.canvasPanel.Size = new System.Drawing.Size(590, 401);
    this.canvasPanel.TabIndex = 0;
    // 
    // tsbCutOff
    // 
    this.tsbCutOff.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
    this.tsbCutOff.Image = ((System.Drawing.Image) (resources.GetObject("tsbCutOff.Image")));
    this.tsbCutOff.ImageTransparentColor = System.Drawing.Color.Magenta;
    this.tsbCutOff.Name = "tsbCutOff";
    this.tsbCutOff.Size = new System.Drawing.Size(23, 22);
    this.tsbCutOff.Text = "Cut Off";
    // 
    // MainForm
    // 
    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    this.BackColor = System.Drawing.SystemColors.Control;
    this.ClientSize = new System.Drawing.Size(800, 450);
    this.Controls.Add(this.splitContainer);
    this.Controls.Add(this.toolStrip);
    this.Controls.Add(this.menuStrip);
    this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
    this.MainMenuStrip = this.menuStrip;
    this.Name = "MainForm";
    this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
    this.Text = "Raman";
    this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
    this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RamanForm_FormClosing);
    this.Resize += new System.EventHandler(this.MainForm_Resize);
    this.menuStrip.ResumeLayout(false);
    this.menuStrip.PerformLayout();
    this.toolStrip.ResumeLayout(false);
    this.toolStrip.PerformLayout();
    this.splitContainer.Panel1.ResumeLayout(false);
    this.splitContainer.Panel1.PerformLayout();
    ((System.ComponentModel.ISupportInitialize) (this.splitContainer)).EndInit();
    this.splitContainer.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private System.Windows.Forms.ToolStripButton tsbCutOff;

  private System.Windows.Forms.ToolStripMenuItem miCutOff;

  private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
  private System.Windows.Forms.ToolStripMenuItem miHelp;
  private System.Windows.Forms.ToolStripMenuItem miShowHelp;

  private System.Windows.Forms.ToolStripMenuItem miOpenMultiSpectrumFiles;

  private System.Windows.Forms.ToolStripButton tsbPeakAnalysis;

  private System.Windows.Forms.ToolStripMenuItem miPeakAnalysis;

  private System.Windows.Forms.MenuStrip menuStrip;
  private System.Windows.Forms.ToolStripMenuItem miFile;
  private System.Windows.Forms.ToolStripMenuItem miOpenSingleSpectrumFiles;
  private System.Windows.Forms.ToolStripMenuItem miExit;

  #endregion
  private System.Windows.Forms.ToolStrip toolStrip1;
  private System.Windows.Forms.ToolStripButton tsbBaselineCorrection;
  private System.Windows.Forms.ToolStripMenuItem miView;
  private System.Windows.Forms.ToolStripMenuItem miZoomWindow;
  private System.Windows.Forms.ToolStripMenuItem miZoomToOriginalSize;
  private System.Windows.Forms.ToolStripMenuItem miTools;
  private System.Windows.Forms.ToolStripMenuItem miBaselineCorrection;
  private System.Windows.Forms.ToolStrip toolStrip;
  private System.Windows.Forms.SplitContainer splitContainer;
  private Raman.Drawing.CanvasPanel canvasPanel;
  private System.Windows.Forms.ToolStripButton tsbOpenFiles;
  private System.Windows.Forms.ToolStripButton tsbZoomToWindow;
  private System.Windows.Forms.ToolStripButton tsbZoomToOriginalSize;
  private Controls.AppStatusStrip statusStrip;
}